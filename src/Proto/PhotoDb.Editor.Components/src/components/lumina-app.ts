import { LitElement, html } from 'lit';
import { customElement, state, query } from 'lit/decorators.js';
import styles from './lumina-app.css';

import './lumina-navigator.ts';
import './lumina-presets.ts';
import './lumina-viewport.ts';
import './lumina-histogram.ts';
import './lumina-neural-engine.ts';
import './lumina-adjustments.ts';
import './lumina-footer.ts';

import { LuminaViewport } from './lumina-viewport.ts';
import { PRESET_CATALOG, PresetItem } from './lumina-presets.ts';

interface AdjustmentParams {
    exposure: number;
    contrast: number;
    highlights: number;
    shadows: number;
    temperature: number;
    tint: number;
    saturation: number;
    vignette: number;
    sharpness: number;
    denoise: number;
    faceSmoothing: number;
    faceRegions?: any[];
}

@customElement('lumina-app')
export class LuminaApp extends LitElement {
    static styles = styles;

    @query('lumina-viewport') viewport!: LuminaViewport;

    // Connection & Pipeline status
    @state() private isWsConnected = false;
    @state() private isProcessing = false;
    @state() private latency = 0;
    @state() private activeCatalog = 'portrait';

    // Image parameters
    @state() private width = 0;
    @state() private height = 0;
    @state() private rChannel: Uint8Array | null = null;
    @state() private gChannel: Uint8Array | null = null;
    @state() private bChannel: Uint8Array | null = null;
    @state() private originalRGBA: Uint8Array | null = null;

    // Sliders
    @state() private exposure = 0;
    @state() private contrast = 0;
    @state() private highlights = 0;
    @state() private shadows = 0;
    @state() private temperature = 0;
    @state() private tint = 0;
    @state() private saturation = 0;
    @state() private vignette = 0;

    // Interaction controls
    @state() private tool: 'hand' | 'brush' = 'hand';
    @state() private brushSize = 20;
    @state() private showBeforeAfter = false;
    @state() private beforeAfterSplit = 50;

    // AI Portrait
    @state() private isAnalyzingFaces = false;
    @state() private detectedFaces: any[] = [];
    @state() private faceSmoothing = 0;
    @state() private aiDescription = '';

    // Histogram
    @state() private histR: Uint8Array | null = null;
    @state() private histG: Uint8Array | null = null;
    @state() private histB: Uint8Array | null = null;

    // Viewport navigation parameters for lumina-navigator
    @state() private viewportScale = 1.0;
    @state() private viewportOffsetX = 0;
    @state() private viewportOffsetY = 0;
    @state() private viewportContainerWidth = 0;
    @state() private viewportContainerHeight = 0;

    private ws: WebSocket | null = null;
    private reconnectTimeout: any = null;
    private binaryUpload: { data: Uint8Array | null; width: number; height: number } = {
        data: null,
        width: 0,
        height: 0
    };

    private isWaitingForAdjust = false;
    private pendingAdjust: Partial<AdjustmentParams> | null = null;

    connectedCallback() {
        super.connectedCallback();
        this.connectWS();
    }

    disconnectedCallback() {
        if (this.reconnectTimeout) {
            clearTimeout(this.reconnectTimeout);
            this.reconnectTimeout = null;
        }
        if (this.ws) {
            this.ws.onopen = null;
            this.ws.onclose = null;
            this.ws.onerror = null;
            this.ws.onmessage = null;
            this.ws.close();
            this.ws = null;
        }
        super.disconnectedCallback();
    }

    private connectWS() {
        if (this.reconnectTimeout) {
            clearTimeout(this.reconnectTimeout);
            this.reconnectTimeout = null;
        }
        if (this.ws) {
            this.ws.onopen = null;
            this.ws.onclose = null;
            this.ws.onerror = null;
            this.ws.onmessage = null;
            this.ws.close();
            this.ws = null;
        }

        const protocol = window.location.protocol === "https:" ? "wss:" : "ws:";
        const wsUrl = `${protocol}//${window.location.host}/ws`;

        console.log("Connecting WebSocket to:", wsUrl);
        const ws = new WebSocket(wsUrl);
        this.ws = ws;

        ws.onopen = () => {
            if (this.ws !== ws) return;
            this.isWsConnected = true;
            console.log("WebSocket connected successfully in Lit!");
            // Automatically load the default portrait on connection
            this.loadPreset(PRESET_CATALOG[0]);
        };

        ws.onclose = () => {
            if (this.ws !== ws) return;
            this.isWsConnected = false;
            console.log("WebSocket disconnected. Retrying in 3 seconds...");
            this.reconnectTimeout = setTimeout(() => this.connectWS(), 3000);
        };

        ws.onerror = (err) => {
            if (this.ws !== ws) return;
            console.error("WS error:", err);
        };

        ws.onmessage = (event) => {
            if (this.ws !== ws) return;
            const start = Date.now();
            if (event.data instanceof Blob) {
                // High-performance binary stream decoder
                const fileReader = new FileReader();
                fileReader.onload = (e) => {
                    if (this.ws !== ws) return;
                    const buffer = e.target?.result as ArrayBuffer;
                    if (buffer.byteLength < 12) return;

                    const view = new DataView(buffer);
                    const w = view.getInt32(0, true);
                    const h = view.getInt32(4, true);

                    // Ignore out-of-order late binary payloads for previous images
                    if (this.width > 0 && (w !== this.width || h !== this.height)) {
                        console.log("Ignoring out-of-order late binary payload for old image dimensions:", w, h);
                        this.isWaitingForAdjust = false;
                        this.isProcessing = false;
                        return;
                    }

                    const size = w * h;

                    if (buffer.byteLength >= 12 + size * 3) {
                        this.width = w;
                        this.height = h;

                        this.rChannel = new Uint8Array(buffer, 12, size);
                        this.gChannel = new Uint8Array(buffer, 12 + size, size);
                        this.bChannel = new Uint8Array(buffer, 12 + size * 2, size);

                        const histOffset = 12 + size * 3;
                        if (buffer.byteLength >= histOffset + 256 * 3) {
                            this.histR = new Uint8Array(buffer, histOffset, 256);
                            this.histG = new Uint8Array(buffer, histOffset + 256, 256);
                            this.histB = new Uint8Array(buffer, histOffset + 512, 256);
                        }

                        this.latency = Date.now() - start;
                    }
                    this.isProcessing = false;
                    this.isWaitingForAdjust = false;
                    if (this.pendingAdjust) {
                        const nextAdjust = this.pendingAdjust;
                        this.pendingAdjust = null;
                        this.sendAdjustments(nextAdjust);
                    }
                };
                fileReader.readAsArrayBuffer(event.data);
            } else {
                try {
                    const msg = JSON.parse(event.data);
                    if (msg.type === "upload_ready") {
                        this.uploadBinaryBuffer(ws);
                    } else if (msg.type === "error") {
                        console.error("Server error:", msg.message);
                        this.isProcessing = false;
                    }
                } catch (e) {
                    console.error("JSON parsing error:", e);
                }
            }
        };
    }

    private uploadBinaryBuffer(ws: WebSocket) {
        const { data } = this.binaryUpload;
        if (ws && ws.readyState === WebSocket.OPEN && data) {
            ws.send(data as any);
            console.log("Uploaded raw binary RGBA pixels to backend.");
        }
    }

    private handleImageLoaded(imgElement: HTMLImageElement, w: number, h: number) {
        const canvas = new OffscreenCanvas(w, h);
        const ctx = canvas.getContext("2d");
        if (!ctx) return;

        ctx.drawImage(imgElement, 0, 0, w, h);
        const imgData = ctx.getImageData(0, 0, w, h);
        const rgbaBytes = new Uint8Array(imgData.data.buffer);

        this.originalRGBA = rgbaBytes;
        this.binaryUpload = { data: rgbaBytes, width: w, height: h };

        // Initialize local channels instantly so the user sees the image immediately!
        const size = w * h;
        const r = new Uint8Array(size);
        const g = new Uint8Array(size);
        const b = new Uint8Array(size);
        for (let i = 0; i < size; i++) {
            const offset = i * 4;
            r[i] = rgbaBytes[offset];
            g[i] = rgbaBytes[offset + 1];
            b[i] = rgbaBytes[offset + 2];
        }
        this.rChannel = r;
        this.gChannel = g;
        this.bChannel = b;
        this.width = w;
        this.height = h;

        if (this.ws && this.ws.readyState === WebSocket.OPEN) {
            this.isProcessing = true;
            this.ws.send(JSON.stringify({
                type: "upload_init",
                width: w,
                height: h
            }));
        }
    }

    private resetEditorToDefaults() {
        this.exposure = 0;
        this.contrast = 0;
        this.highlights = 0;
        this.shadows = 0;
        this.temperature = 0;
        this.tint = 0;
        this.saturation = 0;
        this.vignette = 0;

        this.tool = 'hand';
        this.brushSize = 20;
        this.showBeforeAfter = false;
        this.beforeAfterSplit = 50;

        this.detectedFaces = [];
        this.faceSmoothing = 0;
        this.aiDescription = '';

        // Reset adjustment queue states
        this.isWaitingForAdjust = false;
        this.pendingAdjust = null;

        if (this.viewport) {
            this.viewport.resetViewport();
        }
    }

    private loadPreset(preset: PresetItem) {
        this.resetEditorToDefaults();
        this.activeCatalog = preset.id;

        // Reset image dimensions and channels to eliminate lingering old image & flickering
        this.width = 0;
        this.height = 0;
        this.originalRGBA = null;
        this.rChannel = null;
        this.gChannel = null;
        this.bChannel = null;
        this.histR = null;
        this.histG = null;
        this.histB = null;
        this.isProcessing = true;

        const img = new Image();
        img.crossOrigin = "anonymous";
        img.onload = () => {
            let w = img.naturalWidth;
            let h = img.naturalHeight;
            const maxDim = 16000;

            if (w > maxDim || h > maxDim) {
                if (w > h) {
                    h = Math.round((h * maxDim) / w);
                    w = maxDim;
                } else {
                    w = Math.round((w * maxDim) / h);
                    h = maxDim;
                }
            }

            try {
                this.handleImageLoaded(img, w, h);
            } catch (err) {
                console.warn("Drawing Unsplash image failed (likely CORS). Falling back to procedural frame:", err);
                this.createProceduralTestFrame();
            }
        };

        img.onerror = () => {
            console.warn("Unsplash blocked. Designing rich procedural dusk frame...");
            this.createProceduralTestFrame();
        };

        img.src = preset.url;
    }

    private createProceduralTestFrame() {
        const w = 600;
        const h = 600;
        const canvas = document.createElement("canvas");
        canvas.width = w;
        canvas.height = h;
        const ctx = canvas.getContext("2d");
        if (!ctx) return;

        const gradient = ctx.createLinearGradient(0, 0, 0, h);
        gradient.addColorStop(0, "#1e3a8a");
        gradient.addColorStop(0.5, "#db2777");
        gradient.addColorStop(1, "#f59e0b");
        ctx.fillStyle = gradient;
        ctx.fillRect(0, 0, w, h);

        ctx.fillStyle = "#ffedd5";
        ctx.beginPath();
        ctx.arc(w / 2, h / 2 - 50, 100, 0, Math.PI * 2);
        ctx.fill();

        ctx.fillStyle = "#1e1b4b";
        ctx.beginPath();
        ctx.arc(w / 2, h / 2 - 80, 110, Math.PI, Math.PI * 2);
        ctx.fill();

        ctx.fillStyle = "#1e293b";
        ctx.beginPath();
        ctx.arc(w / 2 - 35, h / 2 - 60, 12, 0, Math.PI * 2);
        ctx.arc(w / 2 + 35, h / 2 - 60, 12, 0, Math.PI * 2);
        ctx.fill();

        ctx.fillStyle = "#f43f5e";
        ctx.globalAlpha = 0.45;
        ctx.beginPath();
        ctx.arc(w / 2 - 55, h / 2 - 30, 20, 0, Math.PI * 2);
        ctx.arc(w / 2 + 55, h / 2 - 30, 20, 0, Math.PI * 2);
        ctx.fill();
        ctx.globalAlpha = 1.0;

        ctx.strokeStyle = "#e11d48";
        ctx.lineWidth = 6;
        ctx.lineCap = "round";
        ctx.beginPath();
        ctx.arc(w / 2, h / 2 - 20, 30, 0, Math.PI);
        ctx.stroke();

        const imgData = ctx.getImageData(0, 0, w, h);
        const rgbaBytes = new Uint8Array(imgData.data.buffer);
        this.originalRGBA = rgbaBytes;

        // Initialize local channels instantly so the user sees the image immediately!
        const size = w * h;
        const r = new Uint8Array(size);
        const g = new Uint8Array(size);
        const b = new Uint8Array(size);
        for (let i = 0; i < size; i++) {
            const offset = i * 4;
            r[i] = rgbaBytes[offset];
            g[i] = rgbaBytes[offset + 1];
            b[i] = rgbaBytes[offset + 2];
        }
        this.rChannel = r;
        this.gChannel = g;
        this.bChannel = b;
        this.width = w;
        this.height = h;

        this.binaryUpload = { data: rgbaBytes, width: w, height: h };
        if (this.ws && this.ws.readyState === WebSocket.OPEN) {
            this.ws.send(JSON.stringify({ type: "upload_init", width: w, height: h }));
        }
    }

    private sendAdjustments(overrides?: Partial<AdjustmentParams>) {
        if (this.ws && this.ws.readyState === WebSocket.OPEN && this.width > 0) {
            if (this.isWaitingForAdjust) {
                // Merge pending adjustments while waiting
                this.pendingAdjust = { ...this.pendingAdjust, ...overrides };
                return;
            }

            const params = {
                exposure: this.exposure,
                contrast: this.contrast,
                highlights: this.highlights,
                shadows: this.shadows,
                temperature: this.temperature,
                tint: this.tint,
                saturation: this.saturation,
                vignette: this.vignette,
                sharpness: 0,
                denoise: 0,
                faceSmoothing: this.faceSmoothing,
                ...overrides
            };

            this.isWaitingForAdjust = true;
            this.ws.send(JSON.stringify({
                type: "adjust",
                params
            }));
        }
    }

    private handleSliderChange(field: string, val: number) {
        (this as any)[field] = val;
        this.sendAdjustments({ [field]: val });
    }

    private resetAllSliders() {
        this.exposure = 0;
        this.contrast = 0;
        this.highlights = 0;
        this.shadows = 0;
        this.temperature = 0;
        this.tint = 0;
        this.saturation = 0;
        this.vignette = 0;
        this.faceSmoothing = 0;
        this.aiDescription = '';

        this.sendAdjustments({
            exposure: 0,
            contrast: 0,
            highlights: 0,
            shadows: 0,
            temperature: 0,
            tint: 0,
            saturation: 0,
            vignette: 0,
            faceSmoothing: 0,
            faceRegions: []
        });
    }

    private async triggerAiAutoColor() {
        if (!this.originalRGBA || this.width === 0) return;
        this.isProcessing = true;

        try {
            const canvas = document.createElement("canvas");
            canvas.width = this.width;
            canvas.height = this.height;
            const ctx = canvas.getContext("2d");
            if (!ctx) return;

            const imgData = ctx.createImageData(this.width, this.height);
            imgData.data.set(this.originalRGBA);
            ctx.putImageData(imgData, 0, 0);
            const base64 = canvas.toDataURL("image/jpeg", 0.85);

            const res = await fetch("/api/ai/auto-color", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ image: base64, mimeType: "image/jpeg" })
            });

            const data = await res.json();
            if (data.error) throw new Error(data.error);

            this.exposure = data.exposure;
            this.contrast = data.contrast;
            this.highlights = data.highlights;
            this.shadows = data.shadows;
            this.temperature = data.temperature;
            this.tint = data.tint;
            this.saturation = data.saturation;
            this.vignette = data.vignette;
            this.aiDescription = data.description;

            this.sendAdjustments({
                exposure: data.exposure,
                contrast: data.contrast,
                highlights: data.highlights,
                shadows: data.shadows,
                temperature: data.temperature,
                tint: data.tint,
                saturation: data.saturation,
                vignette: data.vignette
            });
        } catch (err: any) {
            console.error("AI Auto Color failed:", err);
            alert(`AI Auto Color Error: ${err.message || "Failed to process request"}`);
            this.isProcessing = false;
        }
    }

    private async triggerAiFaceDetection() {
        if (!this.originalRGBA || this.width === 0) return;
        this.isAnalyzingFaces = true;

        try {
            const canvas = document.createElement("canvas");
            canvas.width = this.width;
            canvas.height = this.height;
            const ctx = canvas.getContext("2d");
            if (!ctx) return;

            const imgData = ctx.createImageData(this.width, this.height);
            imgData.data.set(this.originalRGBA);
            ctx.putImageData(imgData, 0, 0);
            const base64 = canvas.toDataURL("image/jpeg", 0.85);

            const res = await fetch("/api/ai/face-detect", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ image: base64, mimeType: "image/jpeg" })
            });

            const data = await res.json();
            if (data.error) throw new Error(data.error);

            this.detectedFaces = data.faces;

            if (data.faces.length > 0) {
                this.faceSmoothing = 45;

                const mappedRegions = data.faces.map((f: any) => ({
                    ymin: f.ymin,
                    xmin: f.xmin,
                    ymax: f.ymax,
                    xmax: f.xmax,
                    skinBox: f.regions.skin
                }));

                if (this.ws && this.ws.readyState === WebSocket.OPEN) {
                    this.ws.send(JSON.stringify({
                        type: "set_face_regions",
                        regions: mappedRegions
                    }));
                }
            } else {
                alert("No human faces detected in this image. Try selecting the Portrait preset catalog.");
            }
        } catch (err: any) {
            console.error("AI Face Detection failed:", err);
            alert(`AI Portrait Error: ${err.message || "Failed to analyze face"}`);
        } finally {
            this.isAnalyzingFaces = false;
        }
    }

    private triggerInpaintRemoval() {
        if (!this.viewport || this.width === 0) return;

        const mask = this.viewport.getMaskData();
        let paintedCount = 0;
        for (let i = 0; i < mask.length; i++) {
            if (mask[i] > 0) paintedCount++;
        }

        if (paintedCount === 0) {
            alert("Please select the Brush Tool first and paint over an object on the canvas to remove it.");
            return;
        }

        this.isProcessing = true;

        // Efficient base64 conversion
        const binaryString = String.fromCharCode.apply(null, Array.from(mask));
        const base64Mask = btoa(binaryString);

        if (this.ws && this.ws.readyState === WebSocket.OPEN) {
            this.ws.send(JSON.stringify({
                type: "inpaint",
                maskBase64: base64Mask
            }));

            this.viewport.clearMask();
        }
    }

    private handleFileSelected(e: CustomEvent) {
        const file = e.detail.file as File;
        this.resetEditorToDefaults();

        // Reset image dimensions and channels to eliminate lingering old image & flickering
        this.width = 0;
        this.height = 0;
        this.originalRGBA = null;
        this.rChannel = null;
        this.gChannel = null;
        this.bChannel = null;
        this.histR = null;
        this.histG = null;
        this.histB = null;
        this.isProcessing = true;

        const reader = new FileReader();
        reader.onload = (event) => {
            const img = new Image();
            img.onload = () => {
                let w = img.naturalWidth;
                let h = img.naturalHeight;
                const maxDim = 16000;

                if (w > maxDim || h > maxDim) {
                    if (w > h) {
                        h = Math.round((h * maxDim) / w);
                        w = maxDim;
                    } else {
                        w = Math.round((w * maxDim) / h);
                        h = maxDim;
                    }
                }

                this.handleImageLoaded(img, w, h);
            };
            img.src = event.target?.result as string;
        };
        reader.readAsDataURL(file);
    }

    private handlePresetSelected(e: CustomEvent) {
        this.loadPreset(e.detail.preset);
    }

    private handleAdjustmentChanged(e: CustomEvent) {
        const { field, value } = e.detail;
        this.handleSliderChange(field, value);
    }

    private handleBrushSizeInput(e: Event) {
        const input = e.target as HTMLInputElement;
        this.brushSize = parseInt(input.value);
    }

    private handleSplitInput(e: Event) {
        const input = e.target as HTMLInputElement;
        this.beforeAfterSplit = parseInt(input.value);
    }

    private handleSplitUpdated(e: CustomEvent) {
        this.beforeAfterSplit = e.detail.split;
    }

    private handleSplitToggle(e: Event) {
        const input = e.target as HTMLInputElement;
        this.showBeforeAfter = input.checked;
    }

    private handleViewportChanged(e: CustomEvent) {
        this.viewportScale = e.detail.scale;
        this.viewportOffsetX = e.detail.offsetX;
        this.viewportOffsetY = e.detail.offsetY;
        this.viewportContainerWidth = e.detail.containerWidth;
        this.viewportContainerHeight = e.detail.containerHeight;
    }

    private handleNavigatorPan(e: CustomEvent) {
        if (this.viewport) {
            this.viewport.setPan(e.detail.offsetX, e.detail.offsetY);
            this.viewportOffsetX = e.detail.offsetX;
            this.viewportOffsetY = e.detail.offsetY;
        }
    }

    private isCustomZoom(): boolean {
        const presets = [0.25, 0.5, 1.0, 2.0];
        const containerWidth = this.viewportContainerWidth;
        const containerHeight = this.viewportContainerHeight;
        if (containerWidth > 0 && containerHeight > 0 && this.width > 0 && this.height > 0) {
            const scaleX = Math.max(0.05, (containerWidth - 40) / this.width);
            const scaleY = Math.max(0.05, (containerHeight - 40) / this.height);
            const fitScale = Math.max(0.05, Math.min(scaleX, scaleY, 1.0));
            const widthScale = Math.max(0.05, (containerWidth - 40) / this.width);

            const tolerance = 0.01;
            if (Math.abs(this.viewportScale - fitScale) < tolerance) return false;
            if (Math.abs(this.viewportScale - widthScale) < tolerance) return false;
        }

        const tolerance = 0.001;
        return !presets.some(p => Math.abs(this.viewportScale - p) < tolerance);
    }

    private getCurrentZoomPresetValue(): string {
        const containerWidth = this.viewportContainerWidth;
        const containerHeight = this.viewportContainerHeight;
        if (containerWidth > 0 && containerHeight > 0 && this.width > 0 && this.height > 0) {
            const scaleX = Math.max(0.05, (containerWidth - 40) / this.width);
            const scaleY = Math.max(0.05, (containerHeight - 40) / this.height);
            const fitScale = Math.max(0.05, Math.min(scaleX, scaleY, 1.0));
            const widthScale = Math.max(0.05, (containerWidth - 40) / this.width);

            const tolerance = 0.01;
            if (Math.abs(this.viewportScale - fitScale) < tolerance) return 'fit';
            if (Math.abs(this.viewportScale - widthScale) < tolerance) return 'width';
        }

        const presets: { [key: string]: number } = {
            '25': 0.25,
            '50': 0.5,
            '100': 1.0,
            '200': 2.0
        };

        const tolerance = 0.001;
        for (const [key, val] of Object.entries(presets)) {
            if (Math.abs(this.viewportScale - val) < tolerance) {
                return key;
            }
        }

        return 'custom';
    }

    private handleZoomPresetChange(e: Event) {
        const select = e.target as HTMLSelectElement;
        const val = select.value;
        if (val === 'custom') return;
        if (this.viewport) {
            this.viewport.setZoomPreset(val);
        }
    }

    render() {
        const hasImage = this.width > 0 && this.height > 0;

        return html`
      <!-- Header -->
      <header>
        <div class="logo-section">
          <div class="logo-brand">
            <span class="logo-badge">Lr</span>
            <span class="logo-text">Lumina AI <span class="logo-version">Studio</span></span>
          </div>
          <nav>
            <span class="active">Develop</span>
            <span>Library</span>
            <span>Print</span>
            <span>Map</span>
          </nav>
        </div>

        <div class="status-indicator">
          ${this.isProcessing
                ? html`
                <div class="pipeline-status">
                  <svg class="spin-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
                    <line x1="12" y1="2" x2="12" y2="6" />
                    <line x1="12" y1="18" x2="12" y2="22" />
                    <line x1="4.93" y1="4.93" x2="7.76" y2="7.76" />
                    <line x1="16.24" y1="16.24" x2="19.07" y2="19.07" />
                    <line x1="2" y1="12" x2="6" y2="12" />
                    <line x1="18" y1="12" x2="22" y2="12" />
                    <line x1="4.93" y1="19.07" x2="7.76" y2="16.24" />
                    <line x1="16.24" y1="7.76" x2="19.07" y2="4.93" />
                  </svg>
                  <span>Processing...</span>
                </div>
              `
                : ''
            }

          <div class="ws-status ${this.isWsConnected ? '' : 'disconnected'}">
            <span class="ws-dot ${this.isWsConnected ? 'pulse' : ''}"></span>
            <span>${this.isWsConnected ? 'CONNECTED' : 'DISCONNECTED'}</span>
          </div>
        </div>
      </header>

      <!-- Main Studio Panel -->
      <div class="main-dashboard">
        <!-- Left Sidebar Rail -->
        <aside class="left-rail custom-scrollbar">
          <lumina-navigator
            .width=${this.width}
            .height=${this.height}
            .scale=${this.viewportScale}
            .offsetX=${this.viewportOffsetX}
            .offsetY=${this.viewportOffsetY}
            .containerWidth=${this.viewportContainerWidth}
            .containerHeight=${this.viewportContainerHeight}
            .originalRGBA=${this.originalRGBA}
            @navigator-pan=${this.handleNavigatorPan}
          ></lumina-navigator>

          <lumina-presets
            .activeCatalog=${this.activeCatalog}
            .hasFaces=${this.detectedFaces.length > 0}
            @file-selected=${this.handleFileSelected}
            @preset-selected=${this.handlePresetSelected}
          ></lumina-presets>

          <div class="planar-core-info">
            <div class="planar-title">Planar Core Details</div>
            <div class="planar-row">
              <span>Channel Plan:</span>
              <span class="planar-val">R + G + B</span>
            </div>
            <div class="planar-row">
              <span>Bit Depth:</span>
              <span class="planar-val">32-bit FP</span>
            </div>
            <div class="planar-row">
              <span>CUDA Accel:</span>
              <span class="planar-val-green">ACTIVE</span>
            </div>
          </div>
        </aside>

        <!-- Central Workspace -->
        <div class="center-viewport">
          <!-- Workspace Toolbar Controls -->
          <div class="viewport-toolbar">
            <div class="tool-group">
              <button
                class="tool-btn ${this.tool === 'hand' ? 'active' : ''}"
                title="Pan & Zoom Tool"
                @click=${() => { this.tool = 'hand'; }}
              >
                <svg class="tool-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <polyline points="5 9 2 12 5 15" />
                  <polyline points="9 5 12 2 15 5" />
                  <polyline points="15 19 12 22 9 19" />
                  <polyline points="19 9 22 12 19 15" />
                  <line x1="2" y1="12" x2="22" y2="12" />
                  <line x1="12" y1="2" x2="12" y2="22" />
                </svg>
              </button>
              
              <button
                class="tool-btn ${this.tool === 'brush' ? 'active' : ''}"
                title="Brush Mask Tool"
                ?disabled=${!hasImage}
                @click=${() => { this.tool = 'brush'; }}
              >
                <svg class="tool-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="m12 3-1.912 5.813a2 2 0 0 1-1.275 1.275L3 12l5.813 1.912a2 2 0 0 1 1.275 1.275L12 21l1.912-5.813a2 2 0 0 1 1.275-1.275L21 12l-5.813-1.912a2 2 0 0 1-1.275-1.275L12 3Z"/>
                </svg>
              </button>
            </div>

            <!-- Brush configurations -->
            <div class="brush-configs" style="transition: opacity 0.2s; ${this.tool === 'brush' ? 'opacity: 1; pointer-events: auto;' : 'opacity: 0; pointer-events: none; position: absolute; left: -9999px;'}">
              <label>Brush Size:</label>
              <input
                type="range"
                class="brush-slider"
                min="5"
                max="80"
                step="1"
                .value=${this.brushSize}
                @input=${this.handleBrushSizeInput}
                ?disabled=${this.tool !== 'brush'}
              />
              <span style="min-width: 32px; display: inline-block;">${this.brushSize}px</span>

              <button class="clear-btn" @click=${() => this.viewport.clearMask()} ?disabled=${this.tool !== 'brush'}>
                <svg style="width: 12px; height: 12px;" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M3 6h18" />
                  <path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6" />
                  <path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2" />
                </svg>
                Clear Paint
              </button>

              <button
                class="clear-btn"
                @click=${() => this.viewport.undoMask()}
                ?disabled=${this.tool !== 'brush' || !this.viewport?.canUndo?.()}
                title="Undo last brush stroke (Ctrl+Z)"
              >
                <svg style="width: 12px; height: 12px;" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M3 7v6h6" />
                  <path d="M21 17a9 9 0 0 0-9-9 9 9 0 0 0-6 2.3L3 13" />
                </svg>
                Undo
              </button>
            </div>

            <!-- Split view controls -->
            <div class="split-configs">
              <label class="split-label">
                <input
                  type="checkbox"
                  class="split-checkbox"
                  .checked=${this.showBeforeAfter}
                  @change=${this.handleSplitToggle}
                  ?disabled=${!hasImage}
                />
                Before / After
              </label>
            </div>

            <!-- Zoom controls -->
            <div class="zoom-configs">
              <span class="zoom-label">Масштаб:</span>
              <select
                id="zoom-select"
                class="zoom-select"
                .value=${this.getCurrentZoomPresetValue()}
                @change=${this.handleZoomPresetChange}
                ?disabled=${!hasImage}
              >
                <option value="fit">Вписать полностью</option>
                <option value="width">По ширине экрана</option>
                <option value="25">25%</option>
                <option value="50">50%</option>
                <option value="100">1:1 (100%)</option>
                <option value="200">200%</option>
                ${this.isCustomZoom() ? html`<option value="custom" disabled selected>${Math.round(this.viewportScale * 100)}%</option>` : ''}
              </select>
            </div>
          </div>

          <!-- Main Viewport Canvas -->
          <lumina-viewport
            .width=${this.width}
            .height=${this.height}
            .rChannel=${this.rChannel}
            .gChannel=${this.gChannel}
            .bChannel=${this.bChannel}
            .tool=${this.tool}
            .brushSize=${this.brushSize}
            .showBeforeAfter=${this.showBeforeAfter}
            .beforeAfterSplit=${this.beforeAfterSplit}
            .originalRGBA=${this.originalRGBA}
            .latency=${this.latency}
            @split-updated=${this.handleSplitUpdated}
            @mask-updated=${() => this.requestUpdate()}
            @viewport-changed=${this.handleViewportChanged}
          ></lumina-viewport>
        </div>

        <!-- Right Sidebar Rail -->
        <aside class="right-rail custom-scrollbar">
          <lumina-histogram
            .histR=${this.histR}
            .histG=${this.histG}
            .histB=${this.histB}
          ></lumina-histogram>

          <lumina-neural-engine
            .isProcessing=${this.isProcessing}
            .isAnalyzingFaces=${this.isAnalyzingFaces}
            .aiDescription=${this.aiDescription}
            .detectedFaces=${this.detectedFaces}
            .tool=${this.tool}
            .hasImage=${hasImage}
            @trigger-auto-color=${this.triggerAiAutoColor}
            @trigger-face-detect=${this.triggerAiFaceDetection}
            @trigger-inpaint=${this.triggerInpaintRemoval}
          ></lumina-neural-engine>

          <lumina-adjustments
            .exposure=${this.exposure}
            .contrast=${this.contrast}
            .highlights=${this.highlights}
            .shadows=${this.shadows}
            .temperature=${this.temperature}
            .tint=${this.tint}
            .saturation=${this.saturation}
            .vignette=${this.vignette}
            .faceSmoothing=${this.faceSmoothing}
            .detectedFacesCount=${this.detectedFaces.length}
            @adjustment-changed=${this.handleAdjustmentChanged}
            @reset-adjustments=${this.resetAllSliders}
          ></lumina-adjustments>
        </aside>
      </div>

      <!-- Technical Footer status line -->
      <lumina-footer
        .width=${this.width}
        .height=${this.height}
        .latency=${this.latency}
      ></lumina-footer>
    `;
    }
}
