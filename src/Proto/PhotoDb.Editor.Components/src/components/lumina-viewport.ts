import { LitElement, html } from 'lit';
import { customElement, property, state, query } from 'lit/decorators.js';
import styles from './lumina-viewport.css';

// Global types and fallbacks
declare global {
    interface Navigator {
        gpu?: {
            requestAdapter(): Promise<any>;
            getPreferredCanvasFormat(): any;
        };
    }
}

type GPUDevice = any;
type GPUCanvasContext = any;
type GPURenderPipeline = any;
type GPUBindGroup = any;
type GPUTexture = any;
type GPUSampler = any;
type GPUBuffer = any;

const GPUTextureUsage = (globalThis as any).GPUTextureUsage || {
    COPY_SRC: 1,
    COPY_DST: 2,
    TEXTURE_BINDING: 4,
    STORAGE_BINDING: 8,
    RENDER_ATTACHMENT: 16,
};

const GPUBufferUsage = (globalThis as any).GPUBufferUsage || {
    MAP_READ: 1,
    MAP_WRITE: 2,
    COPY_SRC: 4,
    COPY_DST: 8,
    INDEX: 16,
    VERTEX: 32,
    UNIFORM: 64,
    STORAGE: 128,
};

@customElement('lumina-viewport')
export class LuminaViewport extends LitElement {
    static styles = styles;

    @property({ type: Number }) width = 0;
    @property({ type: Number }) height = 0;
    @property({ attribute: false }) rChannel: Uint8Array | null = null;
    @property({ attribute: false }) gChannel: Uint8Array | null = null;
    @property({ attribute: false }) bChannel: Uint8Array | null = null;
    @property({ type: String }) tool: 'hand' | 'brush' = 'hand';
    @property({ type: Number }) brushSize = 20;
    @property({ type: Boolean }) showBeforeAfter = false;
    @property({ type: Number }) beforeAfterSplit = 50;
    @property({ attribute: false }) originalRGBA: Uint8Array | null = null;
    @property({ type: Number }) latency = 0;

    @query('#viewport-container') container!: HTMLDivElement;

    get canvas(): HTMLCanvasElement | null {
        return this.renderRoot?.querySelector('.viewport-canvas') as HTMLCanvasElement;
    }

    @state() private scale = 1.0;
    @state() private offsetX = 0;
    @state() private offsetY = 0;
    @state() private renderEngine: 'WebGPU' | 'Error' | 'Initializing' = 'Initializing';
    @state() private webGpuErrorMessage = '';

    private isDragging = false;
    private dragStart = { x: 0, y: 0 };
    private isPainting = false;
    private maskData: Uint8Array | null = null;

    private maskUndoStack: Uint8Array[] = [];
    private maxUndoSteps = 30;
    private isDraggingSplitter = false;

    private webGpuResources = {
        device: null as GPUDevice | null,
        context: null as GPUCanvasContext | null,
        currentCanvas: null as HTMLCanvasElement | null,
        pipeline: null as GPURenderPipeline | null,
        bindGroup: null as GPUBindGroup | null,
        vertexBuffer: null as GPUBuffer | null,
        uniformBuffer: null as GPUBuffer | null,
        sampler: null as GPUSampler | null,
        textures: {
            r: null as GPUTexture | null,
            g: null as GPUTexture | null,
            b: null as GPUTexture | null,
            mask: null as GPUTexture | null,
            original: null as GPUTexture | null,
            dummy: null as GPUTexture | null,
        }
    };

    private resizeObserver: ResizeObserver | null = null;

    connectedCallback() {
        super.connectedCallback();
        this.initWebGpu();
        window.addEventListener('keydown', this.handleKeyDown);
        window.addEventListener('pointerup', this.handleWindowPointerUp);
        window.addEventListener('pointercancel', this.handleWindowPointerUp);
    }

    disconnectedCallback() {
        if (this.resizeObserver) {
            this.resizeObserver.disconnect();
        }
        window.removeEventListener('keydown', this.handleKeyDown);
        window.removeEventListener('pointerup', this.handleWindowPointerUp);
        window.removeEventListener('pointercancel', this.handleWindowPointerUp);
        super.disconnectedCallback();
    }

    private handleWindowPointerUp = (e: PointerEvent) => {
        if (this.isPainting || this.isDragging) {
            this.handlePointerUp(e);
        }
    };

    private handleKeyDown = (e: KeyboardEvent) => {
        const activeEl = document.activeElement;
        if (activeEl && (activeEl.tagName === 'INPUT' || activeEl.tagName === 'TEXTAREA' || activeEl.getAttribute('contenteditable') === 'true')) {
            return;
        }
        if ((e.ctrlKey || e.metaKey) && e.key === 'z') {
            e.preventDefault();
            this.undoMask();
        }
    };

    firstUpdated() {
        if (this.container) {
            this.resizeObserver = new ResizeObserver(() => {
                this.resetView();
            });
            this.resizeObserver.observe(this.container);
        }
    }

    willUpdate(changedProperties: Map<string, any>) {
        if (changedProperties.has('width') || changedProperties.has('height')) {
            if (this.width > 0 && this.height > 0) {
                this.maskData = new Uint8Array(this.width * this.height);
                this.maskUndoStack = []; // Reset undo stack on image change
                this.cleanupWebGpuTextures();

                // Attempt to calculate centering immediately to avoid visual jump/flicker
                const success = this.calculateInitialView();
                if (!success) {
                    // Fallback starting scale if container is not yet laid out/available
                    this.scale = 1.0;
                    this.offsetX = 0;
                    this.offsetY = 0;
                }
            }
        }
    }

    private calculateInitialView(): boolean {
        const container = this.container || this.renderRoot?.querySelector('#viewport-container') as HTMLDivElement;
        if (container && this.width > 0 && this.height > 0) {
            const containerWidth = container.clientWidth;
            const containerHeight = container.clientHeight;
            if (containerWidth > 0 && containerHeight > 0) {
                const scaleX = Math.max(0.05, (containerWidth - 40) / this.width);
                const scaleY = Math.max(0.05, (containerHeight - 40) / this.height);
                const newScale = Math.max(0.05, Math.min(scaleX, scaleY, 1.0));

                this.scale = newScale;
                this.offsetX = (containerWidth - this.width * newScale) / 2;
                this.offsetY = (containerHeight - this.height * newScale) / 2;
                this.emitViewportChange();
                return true;
            }
        }
        return false;
    }

    public resetViewport() {
        if (this.width > 0 && this.height > 0) {
            this.maskData = new Uint8Array(this.width * this.height);
            this.maskUndoStack = [];
            this.cleanupWebGpuTextures();
            this.resetView();
        }
    }

    public setZoomPreset(preset: string) {
        if (!this.container || this.width === 0 || this.height === 0) return;
        const containerWidth = this.container.clientWidth;
        const containerHeight = this.container.clientHeight;
        if (containerWidth === 0 || containerHeight === 0) return;

        let newScale = 1.0;

        if (preset === 'fit') {
            const scaleX = Math.max(0.05, (containerWidth - 40) / this.width);
            const scaleY = Math.max(0.05, (containerHeight - 40) / this.height);
            newScale = Math.max(0.05, Math.min(scaleX, scaleY, 1.0));
        } else if (preset === 'width') {
            newScale = Math.max(0.05, (containerWidth - 40) / this.width);
        } else if (preset === '100' || preset === '1') {
            newScale = 1.0;
        } else if (preset === '50') {
            newScale = 0.5;
        } else if (preset === '25') {
            newScale = 0.25;
        } else if (preset === '200') {
            newScale = 2.0;
        } else {
            const val = parseFloat(preset);
            if (!isNaN(val)) {
                newScale = val;
            } else {
                return;
            }
        }

        this.scale = newScale;
        this.offsetX = (containerWidth - this.width * newScale) / 2;
        this.offsetY = (containerHeight - this.height * newScale) / 2;
        this.triggerRender();
        this.emitViewportChange();
    }

    private cleanupWebGpuTextures() {
        const texs = this.webGpuResources.textures;
        for (const key of ['r', 'g', 'b', 'mask', 'original', 'dummy'] as const) {
            if (texs[key]) {
                try {
                    texs[key].destroy();
                } catch (e) { }
                texs[key] = null;
            }
        }
        this.webGpuResources.context = null;
        this.webGpuResources.currentCanvas = null;
    }

    updated(changedProperties: Map<string, any>) {
        if (changedProperties.has('width') || changedProperties.has('height')) {
            if (this.width > 0 && this.height > 0) {
                this.resetView();
            }
        }
        // Re-render when rendering params change
        if (this.width > 0 && this.height > 0) {
            this.triggerRender();
        }
    }

    public resetView() {
        if (!this.container || this.width === 0 || this.height === 0) return;
        const containerWidth = this.container.clientWidth;
        const containerHeight = this.container.clientHeight;

        if (containerWidth === 0 || containerHeight === 0) {
            // If the container is not yet laid out (0 size), retry on the next animation frame
            requestAnimationFrame(() => this.resetView());
            return;
        }

        const scaleX = Math.max(0.05, (containerWidth - 40) / this.width);
        const scaleY = Math.max(0.05, (containerHeight - 40) / this.height);
        const newScale = Math.max(0.05, Math.min(scaleX, scaleY, 1.0));

        this.scale = newScale;
        this.offsetX = (containerWidth - this.width * newScale) / 2;
        this.offsetY = (containerHeight - this.height * newScale) / 2;
        this.emitViewportChange();
    }

    public setPan(offsetX: number, offsetY: number) {
        // Clamp the incoming offsets when the navigator pans using the same generous margin
        if (this.container && this.width > 0 && this.height > 0) {
            const containerWidth = this.container.clientWidth;
            const containerHeight = this.container.clientHeight;
            if (containerWidth > 0 && containerHeight > 0) {
                const margin = 40;
                const scaledWidth = this.width * this.scale;
                const scaledHeight = this.height * this.scale;

                const minOffsetX = margin - scaledWidth;
                const maxOffsetX = containerWidth - margin;
                offsetX = Math.max(minOffsetX, Math.min(maxOffsetX, offsetX));

                const minOffsetY = margin - scaledHeight;
                const maxOffsetY = containerHeight - margin;
                offsetY = Math.max(minOffsetY, Math.min(maxOffsetY, offsetY));
            }
        }
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.triggerRender();
        this.emitViewportChange();
    }

    private emitViewportChange() {
        const container = this.container || this.renderRoot?.querySelector('#viewport-container') as HTMLDivElement;
        if (!container) return;
        this.dispatchEvent(new CustomEvent('viewport-changed', {
            detail: {
                scale: this.scale,
                offsetX: this.offsetX,
                offsetY: this.offsetY,
                containerWidth: container.clientWidth,
                containerHeight: container.clientHeight
            },
            bubbles: true,
            composed: true
        }));
    }

    public getMaskData(): Uint8Array {
        return this.maskData || new Uint8Array(0);
    }

    public clearMask() {
        if (this.maskData && this.width > 0 && this.height > 0) {
            // Save current state before clearing
            this.maskUndoStack.push(new Uint8Array(this.maskData));
            if (this.maskUndoStack.length > this.maxUndoSteps) {
                this.maskUndoStack.shift();
            }
            this.maskData.fill(0);
            this.triggerRender();
            this.emitMaskUpdate();
        }
    }

    public canUndo(): boolean {
        return this.maskUndoStack.length > 0;
    }

    public undoMask() {
        if (this.maskUndoStack.length > 0 && this.maskData) {
            const prevMask = this.maskUndoStack.pop();
            if (prevMask) {
                this.maskData.set(prevMask);
                this.triggerRender();
                this.emitMaskUpdate();
            }
        }
    }

    private async initWebGpu() {
        if (!navigator.gpu) {
            console.error("WebGPU is not supported in this browser environment.");
            this.renderEngine = "Error";
            this.webGpuErrorMessage = "Ваш браузер или устройство не поддерживает WebGPU. Для работы фотостудии Lumina AI необходим графический ускоритель (GPU) и совместимый браузер.";
            return;
        }

        try {
            const adapter = await navigator.gpu.requestAdapter();
            if (!adapter) {
                console.error("No appropriate WebGPU adapter found.");
                this.renderEngine = "Error";
                this.webGpuErrorMessage = "Не удалось обнаружить совместимый графический ускоритель (GPU). Работа в данном окружении без видеокарты невозможна.";
                return;
            }
            const device = await adapter.requestDevice();

            device.addEventListener('uncapturederror', (event: any) => {
                console.error("WebGPU uncaptured error:", event.error?.message);
            });

            this.webGpuResources.device = device;
            this.renderEngine = "WebGPU";
            console.log("WebGPU successfully initialized in Lit!");
        } catch (err: any) {
            console.error("Failed to request WebGPU device:", err);
            this.renderEngine = "Error";
            this.webGpuErrorMessage = `Ошибка при инициализации WebGPU: ${err?.message || err}. Работа приложения невозможна без графического ускорителя.`;
        }
    }

    private triggerRender() {
        if (!this.canvas || this.width === 0 || this.height === 0) return;
        try {
            if (this.renderEngine === "WebGPU" && this.webGpuResources.device) {
                this.renderWebGpu();
            }
        } catch (err) {
            console.error("WebGPU rendering failed:", err);
        }
    }

    private renderWebGpu() {
        const canvas = this.canvas;
        const { device } = this.webGpuResources;
        if (!device || !canvas) return;

        // Check if canvas element changed
        if (this.webGpuResources.currentCanvas !== canvas) {
            this.webGpuResources.context = null;
            this.webGpuResources.currentCanvas = canvas;
        }

        if (canvas.width !== this.width || canvas.height !== this.height) {
            canvas.width = this.width;
            canvas.height = this.height;
            this.webGpuResources.context = null;
        }

        let context = this.webGpuResources.context;
        if (!context) {
            context = canvas.getContext("webgpu") as GPUCanvasContext;
            if (!context) {
                throw new Error("Failed to get webgpu canvas context");
            }
            const format = navigator.gpu ? navigator.gpu.getPreferredCanvasFormat() : "bgra8unorm";
            context.configure({
                device,
                format,
                alphaMode: "premultiplied",
            });
            this.webGpuResources.context = context;
        }

        if (!this.rChannel || !this.gChannel || !this.bChannel) return;
        if (this.rChannel.length !== this.width * this.height ||
            this.gChannel.length !== this.width * this.height ||
            this.bChannel.length !== this.width * this.height) {
            console.warn("Channel size mismatch in renderWebGpu");
            return;
        }

        // 1. Create and upload vertex buffer
        let vertexBuffer = this.webGpuResources.vertexBuffer;
        if (!vertexBuffer) {
            const vertices = new Float32Array([
                // X,    Y,   U,   V
                -1.0, -1.0, 0.0, 1.0,  // Bottom-Left
                1.0, -1.0, 1.0, 1.0,  // Bottom-Right
                -1.0, 1.0, 0.0, 0.0,  // Top-Left
                1.0, 1.0, 1.0, 0.0   // Top-Right
            ]);
            vertexBuffer = device.createBuffer({
                size: vertices.byteLength,
                usage: GPUBufferUsage.VERTEX | GPUBufferUsage.COPY_DST,
                mappedAtCreation: true
            });
            new Float32Array(vertexBuffer.getMappedRange()).set(vertices);
            vertexBuffer.unmap();
            this.webGpuResources.vertexBuffer = vertexBuffer;
        }

        // 2. Upload planar R, G, B, and Mask channels
        const updateOrCreatePlaneTexture = (data: Uint8Array, currentTex: GPUTexture | null, label: string): GPUTexture => {
            let texture = currentTex;
            if (!texture) {
                texture = device.createTexture({
                    size: [this.width, this.height],
                    format: "r8unorm",
                    usage: GPUTextureUsage.TEXTURE_BINDING | GPUTextureUsage.COPY_DST,
                    label,
                });
            }

            const bytesPerPixel = 1;
            const unpaddedBytesPerRow = this.width * bytesPerPixel;
            const paddedBytesPerRow = Math.ceil(unpaddedBytesPerRow / 256) * 256;
            const paddedData = new Uint8Array(paddedBytesPerRow * this.height);
            for (let y = 0; y < this.height; y++) {
                paddedData.set(
                    data.subarray(y * this.width, (y + 1) * this.width),
                    y * paddedBytesPerRow
                );
            }

            device.queue.writeTexture(
                { texture },
                paddedData,
                { bytesPerRow: paddedBytesPerRow },
                [this.width, this.height]
            );
            return texture;
        };

        const rTex = updateOrCreatePlaneTexture(this.rChannel, this.webGpuResources.textures.r, "R Channel");
        this.webGpuResources.textures.r = rTex;

        const gTex = updateOrCreatePlaneTexture(this.gChannel, this.webGpuResources.textures.g, "G Channel");
        this.webGpuResources.textures.g = gTex;

        const bTex = updateOrCreatePlaneTexture(this.bChannel, this.webGpuResources.textures.b, "B Channel");
        this.webGpuResources.textures.b = bTex;

        const mask = this.maskData || new Uint8Array(this.width * this.height);
        const maskTex = updateOrCreatePlaneTexture(mask, this.webGpuResources.textures.mask, "Mask Channel");
        this.webGpuResources.textures.mask = maskTex;

        // 3. Upload original RGBA texture
        let origTex = this.webGpuResources.textures.original;
        if (this.originalRGBA && !origTex) {
            origTex = device.createTexture({
                size: [this.width, this.height],
                format: "rgba8unorm",
                usage: GPUTextureUsage.TEXTURE_BINDING | GPUTextureUsage.COPY_DST,
                label: "Original RGBA",
            });

            const bytesPerPixel = 4;
            const unpaddedBytesPerRow = this.width * bytesPerPixel;
            const paddedBytesPerRow = Math.ceil(unpaddedBytesPerRow / 256) * 256;
            const paddedData = new Uint8Array(paddedBytesPerRow * this.height);
            for (let y = 0; y < this.height; y++) {
                paddedData.set(
                    this.originalRGBA.subarray(y * this.width * 4, (y + 1) * this.width * 4),
                    y * paddedBytesPerRow
                );
            }

            device.queue.writeTexture(
                { texture: origTex },
                paddedData,
                { bytesPerRow: paddedBytesPerRow },
                [this.width, this.height]
            );
            this.webGpuResources.textures.original = origTex;
        }

        // Create transient dummy 1x1 texture if origTex is not available
        let dummyTex = this.webGpuResources.textures.dummy;
        if (!origTex && !dummyTex) {
            dummyTex = device.createTexture({
                size: [1, 1],
                format: "rgba8unorm",
                usage: GPUTextureUsage.TEXTURE_BINDING | GPUTextureUsage.COPY_DST,
                label: "Dummy Texture",
            });
            device.queue.writeTexture(
                { texture: dummyTex },
                new Uint8Array([0, 0, 0, 0]),
                { bytesPerRow: 256 },
                [1, 1]
            );
            this.webGpuResources.textures.dummy = dummyTex;
        }

        const finalOrigTex = origTex || dummyTex!;

        // 4. Create Shader Module with WGSL
        let pipeline = this.webGpuResources.pipeline;
        if (!pipeline) {
            const shaderModule = device.createShaderModule({
                code: `
          struct VertexOutput {
            @builtin(position) position : vec4<f32>,
            @location(0) uv : vec2<f32>,
          }

          @vertex
          fn vs_main(@location(0) pos: vec2<f32>, @location(1) uv: vec2<f32>) -> VertexOutput {
            var output: VertexOutput;
            output.position = vec4<f32>(pos, 0.0, 1.0);
            output.uv = uv;
            return output;
          }

          @group(0) @binding(0) var s : sampler;
          @group(0) @binding(1) var r_tex : texture_2d<f32>;
          @group(0) @binding(2) var g_tex : texture_2d<f32>;
          @group(0) @binding(3) var b_tex : texture_2d<f32>;
          @group(0) @binding(4) var mask_tex : texture_2d<f32>;
          
          struct Uniforms {
            split_ratio : f32,
            show_split : f32,
          }
          @group(0) @binding(5) var<uniform> uniforms : Uniforms;
          @group(0) @binding(6) var orig_tex : texture_2d<f32>;

          @fragment
          fn fs_main(@location(0) uv : vec2<f32>) -> @location(0) vec4<f32> {
            let r = textureSample(r_tex, s, uv).r;
            let g = textureSample(g_tex, s, uv).r;
            let b = textureSample(b_tex, s, uv).r;
            let mask = textureSample(mask_tex, s, uv).r;

            var final_color = vec4<f32>(r, g, b, 1.0);

            if (mask > 0.1) {
              final_color = mix(final_color, vec4<f32>(0.9, 0.2, 0.2, 1.0), 0.35);
            }

            if (uniforms.show_split > 0.5) {
              if (uv.x < uniforms.split_ratio) {
                let orig = textureSampleLevel(orig_tex, s, uv, 0.0);
                return orig;
              } else if (abs(uv.x - uniforms.split_ratio) < 0.002) {
                return vec4<f32>(1.0, 1.0, 1.0, 1.0);
              }
            }

            return final_color;
          }
        `,
            });

            const format = navigator.gpu ? navigator.gpu.getPreferredCanvasFormat() : "bgra8unorm";

            // 7. Setup Pipeline
            pipeline = device.createRenderPipeline({
                layout: "auto",
                vertex: {
                    module: shaderModule,
                    entryPoint: "vs_main",
                    buffers: [{
                        arrayStride: 16,
                        attributes: [
                            { shaderLocation: 0, offset: 0, format: "float32x2" },
                            { shaderLocation: 1, offset: 8, format: "float32x2" }
                        ]
                    }]
                },
                fragment: {
                    module: shaderModule,
                    entryPoint: "fs_main",
                    targets: [{ format }],
                },
                primitive: {
                    topology: "triangle-strip",
                    cullMode: "none",
                },
            });
            this.webGpuResources.pipeline = pipeline;
        }

        // 5. Setup uniform buffer for split view parameters
        let uniformBuffer = this.webGpuResources.uniformBuffer;
        if (!uniformBuffer) {
            uniformBuffer = device.createBuffer({
                size: 16, // aligned to 16 bytes
                usage: GPUBufferUsage.UNIFORM | GPUBufferUsage.COPY_DST,
            });
            this.webGpuResources.uniformBuffer = uniformBuffer;
        }
        const uniformData = new Float32Array([
            this.beforeAfterSplit / 100,
            this.showBeforeAfter ? 1.0 : 0.0,
            0.0,
            0.0,
        ]);
        device.queue.writeBuffer(uniformBuffer, 0, uniformData);

        // 6. Create Sampler (Linear filtering with clamp-to-edge)
        let sampler = this.webGpuResources.sampler;
        if (!sampler) {
            sampler = device.createSampler({
                magFilter: "linear",
                minFilter: "linear",
                addressModeU: "clamp-to-edge",
                addressModeV: "clamp-to-edge",
            });
            this.webGpuResources.sampler = sampler;
        }

        // 8. Bind Resources to BindGroup
        const bindGroup = device.createBindGroup({
            layout: pipeline.getBindGroupLayout(0),
            entries: [
                { binding: 0, resource: sampler },
                { binding: 1, resource: rTex.createView() },
                { binding: 2, resource: gTex.createView() },
                { binding: 3, resource: bTex.createView() },
                { binding: 4, resource: maskTex.createView() },
                { binding: 5, resource: { buffer: uniformBuffer } },
                { binding: 6, resource: finalOrigTex.createView() },
            ],
        });

        // 9. Encode and submit render commands
        const commandEncoder = device.createCommandEncoder();
        const currentTex = context.getCurrentTexture();
        const textureView = currentTex.createView();

        const renderPass = commandEncoder.beginRenderPass({
            colorAttachments: [{
                view: textureView,
                clearValue: { r: 0.0, g: 0.0, b: 0.0, a: 0.0 },
                loadOp: "clear",
                storeOp: "store",
            }],
        });

        renderPass.setPipeline(pipeline);
        renderPass.setVertexBuffer(0, vertexBuffer);
        renderPass.setBindGroup(0, bindGroup);
        renderPass.draw(4);
        renderPass.end();

        device.queue.submit([commandEncoder.finish()]);
    }

    private handleWheel(e: WheelEvent) {
        e.preventDefault();
        if (this.width === 0 || this.height === 0) return;

        const zoomIntensity = 0.12;
        const rect = this.container.getBoundingClientRect();
        if (!rect) return;

        const mouseX = e.clientX - rect.left;
        const mouseY = e.clientY - rect.top;

        const imageX = (mouseX - this.offsetX) / this.scale;
        const imageY = (mouseY - this.offsetY) / this.scale;

        let newScale = this.scale;
        if (e.deltaY < 0) {
            newScale = Math.min(10.0, this.scale + this.scale * zoomIntensity);
        } else {
            newScale = Math.max(0.1, this.scale - this.scale * zoomIntensity);
        }

        let newOffsetX = mouseX - imageX * newScale;
        let newOffsetY = mouseY - imageY * newScale;

        // Clamp zoom offsets with a generous margin so the image can be zoomed/panned anywhere
        const margin = 40;
        const scaledWidth = this.width * newScale;
        const scaledHeight = this.height * newScale;

        const minOffsetX = margin - scaledWidth;
        const maxOffsetX = rect.width - margin;
        newOffsetX = Math.max(minOffsetX, Math.min(maxOffsetX, newOffsetX));

        const minOffsetY = margin - scaledHeight;
        const maxOffsetY = rect.height - margin;
        newOffsetY = Math.max(minOffsetY, Math.min(maxOffsetY, newOffsetY));

        this.scale = newScale;
        this.offsetX = newOffsetX;
        this.offsetY = newOffsetY;
        this.emitViewportChange();
    }

    private handlePointerDown(e: PointerEvent) {
        if (this.width === 0 || this.height === 0) return;

        const rect = this.canvas?.getBoundingClientRect();
        if (!rect) return;

        const clickX = Math.floor(((e.clientX - rect.left) / rect.width) * this.width);
        const clickY = Math.floor(((e.clientY - rect.top) / rect.height) * this.height);

        if (this.tool === "brush") {
            if (clickX >= 0 && clickX < this.width && clickY >= 0 && clickY < this.height) {
                this.isPainting = true;
                if (this.maskData) {
                    this.maskUndoStack.push(new Uint8Array(this.maskData));
                    if (this.maskUndoStack.length > this.maxUndoSteps) {
                        this.maskUndoStack.shift();
                    }
                }
                try {
                    this.container.setPointerCapture(e.pointerId);
                } catch (err) { }
                this.paintMask(clickX, clickY, e.shiftKey ? 0 : 255);
            }
        } else {
            this.isDragging = true;
            this.dragStart = { x: e.clientX - this.offsetX, y: e.clientY - this.offsetY };
            try {
                this.container.setPointerCapture(e.pointerId);
            } catch (err) { }
        }
    }

    private handlePointerMove(e: PointerEvent) {
        if (this.width === 0 || this.height === 0) return;

        const rect = this.canvas?.getBoundingClientRect();
        if (!rect) return;

        const clickX = Math.floor(((e.clientX - rect.left) / rect.width) * this.width);
        const clickY = Math.floor(((e.clientY - rect.top) / rect.height) * this.height);

        if (this.tool === "brush" && this.isPainting) {
            if ((e.buttons & 1) === 0) {
                this.isPainting = false;
                try {
                    this.container.releasePointerCapture(e.pointerId);
                } catch (err) { }
                this.emitMaskUpdate();
                return;
            }
            this.paintMask(clickX, clickY, e.shiftKey ? 0 : 255);
        } else if (this.isDragging) {
            if ((e.buttons & 1) === 0) {
                this.isDragging = false;
                try {
                    this.container.releasePointerCapture(e.pointerId);
                } catch (err) { }
                return;
            }
            let newOffsetX = e.clientX - this.dragStart.x;
            let newOffsetY = e.clientY - this.dragStart.y;

            const containerWidth = this.container.clientWidth;
            const containerHeight = this.container.clientHeight;

            // Clamp dragging offsets with a generous margin so the image can be panned anywhere,
            // even if it is smaller than the container, as long as it's not dragged entirely off-screen
            const margin = 40;
            const scaledWidth = this.width * this.scale;
            const scaledHeight = this.height * this.scale;

            const minOffsetX = margin - scaledWidth;
            const maxOffsetX = containerWidth - margin;
            newOffsetX = Math.max(minOffsetX, Math.min(maxOffsetX, newOffsetX));

            const minOffsetY = margin - scaledHeight;
            const maxOffsetY = containerHeight - margin;
            newOffsetY = Math.max(minOffsetY, Math.min(maxOffsetY, newOffsetY));

            this.offsetX = newOffsetX;
            this.offsetY = newOffsetY;
            this.emitViewportChange();
        }
    }

    private handlePointerUp(e: PointerEvent) {
        if (this.isDragging) {
            this.isDragging = false;
            try {
                this.container.releasePointerCapture(e.pointerId);
            } catch (err) { }
        }
        if (this.isPainting) {
            this.isPainting = false;
            try {
                this.container.releasePointerCapture(e.pointerId);
            } catch (err) { }
            this.emitMaskUpdate();
        }
    }

    private emitMaskUpdate() {
        this.dispatchEvent(new CustomEvent('mask-updated', {
            detail: { mask: this.maskData },
            bubbles: true,
            composed: true
        }));
    }

    private paintMask(cx: number, cy: number, value: number) {
        if (!this.maskData || this.width === 0 || this.height === 0) return;
        const r = this.brushSize;
        const mask = this.maskData;

        for (let dy = -r; dy <= r; dy++) {
            const y = cy + dy;
            if (y < 0 || y >= this.height) continue;
            const yOffset = y * this.width;

            for (let dx = -r; dx <= r; dx++) {
                const x = cx + dx;
                if (x < 0 || x >= this.width) continue;

                if (dx * dx + dy * dy <= r * r) {
                    mask[yOffset + x] = value;
                }
            }
        }

        this.triggerRender();
    }

    private handleSplitterDown(e: PointerEvent) {
        e.stopPropagation();
        this.isDraggingSplitter = true;
        const el = e.currentTarget as HTMLElement;
        el.setPointerCapture(e.pointerId);
    }

    private handleSplitterMove(e: PointerEvent) {
        if (!this.isDraggingSplitter) return;
        e.stopPropagation();
        const rect = this.canvas?.getBoundingClientRect();
        if (!rect) return;
        const percentX = ((e.clientX - rect.left) / rect.width) * 100;
        this.beforeAfterSplit = Math.max(0, Math.min(100, percentX));

        this.dispatchEvent(new CustomEvent('split-updated', {
            detail: { split: this.beforeAfterSplit },
            bubbles: true,
            composed: true
        }));
        this.triggerRender();
    }

    private handleSplitterUp(e: PointerEvent) {
        if (this.isDraggingSplitter) {
            this.isDraggingSplitter = false;
            const el = e.currentTarget as HTMLElement;
            try {
                el.releasePointerCapture(e.pointerId);
            } catch (err) { }
        }
    }

    render() {
        const hasImage = this.width > 0 && this.height > 0;
        const cursorStyle = this.tool === "brush" ? "crosshair" : (this.isDragging ? "grabbing" : "grab");

        return html`
      <div
        id="viewport-container"
        class="viewport-container"
        @wheel=${this.handleWheel}
        @pointerdown=${this.handlePointerDown}
        @pointermove=${this.handlePointerMove}
        @pointerup=${this.handlePointerUp}
        @pointercancel=${this.handlePointerUp}
        @lostpointercapture=${this.handlePointerUp}
        style="cursor: ${cursorStyle};"
      >
        ${this.renderEngine === 'Error'
                ? html`
              <div class="webgpu-error-container">
                <svg class="error-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
                <div class="error-title">Аппаратное ускорение WebGPU недоступно</div>
                <div class="error-message">
                  ${this.webGpuErrorMessage || "Для работы фотостудии Lumina AI необходим графический ускоритель (GPU) с поддержкой технологии WebGPU."}
                </div>
                <div class="error-hint">Пожалуйста, убедитесь, что в настройках вашего браузера включено аппаратное ускорение и установлены актуальные драйверы видеокарты.</div>
              </div>
            `
                : html`
              ${hasImage
                        ? html`
                    <canvas
                      id="viewport-canvas-webgpu"
                      class="viewport-canvas"
                      style="position: absolute; left: 0; top: 0; transform-origin: top-left; transform: translate(${this.offsetX}px, ${this.offsetY}px) scale(${this.scale}); width: ${this.width}px; height: ${this.height}px;"
                    ></canvas>

                    ${this.showBeforeAfter
                                ? html`
                          <div
                            class="splitter-overlay"
                            style="transform: translate(${this.offsetX}px, ${this.offsetY}px) scale(${this.scale}); width: ${this.width}px; height: ${this.height}px;"
                          >
                            <div
                              class="splitter-bar"
                              style="left: ${this.beforeAfterSplit}%;"
                              @pointerdown=${this.handleSplitterDown}
                              @pointermove=${this.handleSplitterMove}
                              @pointerup=${this.handleSplitterUp}
                            >
                              <div class="splitter-handle">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
                                  <path d="M8 7l-5 5 5 5M16 7l5 5-5 5" />
                                </svg>
                              </div>
                            </div>
                          </div>
                        `
                                : ''
                            }
                  `
                        : html`
                    <div class="placeholder">
                      <svg class="placeholder-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5">
                        <path d="M2 12C2 6.47715 6.47715 2 12 2C17.5228 2 22 6.47715 22 12C22 17.5228 17.5228 22 12 22C6.47715 22 2 17.5228 2 12Z" />
                        <path d="M12 8V12L14 14" stroke-linecap="round" stroke-linejoin="round" />
                      </svg>
                      <div class="placeholder-text">Загрузите или выберите изображение из каталога, чтобы начать редактирование</div>
                    </div>
                  `
                    }

              ${hasImage
                        ? html`
                    <div class="technical-panel">
                      <div class="badge-item">
                        <span class="indicator-dot dot-green"></span>
                        ENGINE: <span class="tech-value">WebGPU</span>
                      </div>
                      <div>
                        SIZE: <span class="tech-value">${this.width}×${this.height}</span>
                      </div>
                      ${this.latency > 0
                                ? html`
                            <div>
                              LATENCY: <span class="tech-value">${this.latency.toFixed(1)}ms</span>
                            </div>
                          `
                                : ''
                            }
                    </div>
                  `
                        : ''
                    }

              ${this.tool === 'brush' && hasImage
                        ? html`
                    <div class="brush-badge">
                      <svg class="brush-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="m12 3-1.912 5.813a2 2 0 0 1-1.275 1.275L3 12l5.813 1.912a2 2 0 0 1 1.275 1.275L12 21l1.912-5.813a2 2 0 0 1 1.275-1.275L21 12l-5.813-1.912a2 2 0 0 1-1.275-1.275L12 3Z"/>
                      </svg>
                      <span>Режим кисти: <strong class="brush-highlight">Рисование маски</strong> (Удерживайте Shift для стирания)</span>
                    </div>
                  `
                        : ''
                    }
            `
            }
      </div>
    `;
    }
}
