import { LitElement, html } from "lit";
import { customElement, property, state } from "lit/decorators.js";
import { Image as AppImage } from "../types";
import editorStyles from "./ImageEditorDialog.css";

@customElement("photo-editor-dialog")
export class ImageEditorDialog extends LitElement {
  @property({ type: Object }) selectedImage: AppImage | undefined = undefined;
  @property({ type: Boolean, reflect: true }) open: boolean = false;

  @state() private _activeTab: "adjust" | "crop-resize" = "adjust";
  @state() private _zoom: number = 50; // percentage
  @state() private _isLoading: boolean = false;

  // Parameters
  @state() private _brightness: number = 0;
  @state() private _exposure: number = 0;
  @state() private _contrast: number = 0;
  @state() private _saturation: number = 0;
  @state() private _vibrance: number = 0;
  @state() private _sharpness: number = 0;
  @state() private _rotation: number = 0;
  @state() private _flipH: boolean = false;
  @state() private _flipV: boolean = false;
  @state() private _resizeW: number = 0;
  @state() private _resizeH: number = 0;
  @state() private _lockAspect: boolean = true;

  // Crop percentage parameters (relative to fully rotated/flipped canvas view)
  @state() private _cropX: number = 0; // percentage: 0 to 100
  @state() private _cropY: number = 0; // percentage: 0 to 100
  @state() private _cropW: number = 100; // percentage: 0 to 100
  @state() private _cropH: number = 100; // percentage: 0 to 100
  @state() private _cropAspect: "free" | "1:1" | "3:2" | "4:3" | "16:9" = "free";

  // Drag interaction trackers
  private _activeHandle: string | null = null;
  private _dragStartData: any = null;

  // History state for Undo/Redo
  private _history: any[] = [];
  private _historyIndex: number = -1;

  static styles = editorStyles;

  willUpdate(changedProperties: Map<string | number | symbol, unknown>) {
    if (changedProperties.has("selectedImage") && this.selectedImage) {
      this._resetAll();
    }
    if (changedProperties.has("open")) {
      if (this.open) {
        this.classList.add("open");
        this._resetAll();
      } else {
        this.classList.remove("open");
      }
    }
  }

  private _resetAll() {
    this._brightness = 0;
    this._exposure = 0;
    this._contrast = 0;
    this._saturation = 0;
    this._vibrance = 0;
    this._sharpness = 0;
    this._rotation = 0;
    this._flipH = false;
    this._flipV = false;
    this._zoom = 50;
    this._cropX = 0;
    this._cropY = 0;
    this._cropW = 100;
    this._cropH = 100;
    this._cropAspect = "free";
    if (this.selectedImage) {
      this._resizeW = this.selectedImage.width || 800;
      this._resizeH = this.selectedImage.height || 600;
    }
    this._lockAspect = true;
    this._history = [];
    this._historyIndex = -1;
    this._saveHistoryState();
  }

  private _saveHistoryState() {
    const currentState = {
      brightness: this._brightness,
      exposure: this._exposure,
      contrast: this._contrast,
      saturation: this._saturation,
      vibrance: this._vibrance,
      sharpness: this._sharpness,
      rotation: this._rotation,
      flipH: this._flipH,
      flipV: this._flipV,
      resizeW: this._resizeW,
      resizeH: this._resizeH,
      lockAspect: this._lockAspect,
      cropX: this._cropX,
      cropY: this._cropY,
      cropW: this._cropW,
      cropH: this._cropH,
      cropAspect: this._cropAspect
    };

    // Erase history after current index if we were in middle of undo stack
    this._history = this._history.slice(0, this._historyIndex + 1);
    this._history.push(currentState);
    this._historyIndex = this._history.length - 1;
  }

  private _undo() {
    if (this._historyIndex > 0) {
      this._historyIndex--;
      this._applyHistoryState(this._history[this._historyIndex]);
    }
  }

  private _redo() {
    if (this._historyIndex < this._history.length - 1) {
      this._historyIndex++;
      this._applyHistoryState(this._history[this._historyIndex]);
    }
  }

  private _applyHistoryState(state: any) {
    this._brightness = state.brightness;
    this._exposure = state.exposure;
    this._contrast = state.contrast;
    this._saturation = state.saturation;
    this._vibrance = state.vibrance;
    this._sharpness = state.sharpness;
    this._rotation = state.rotation;
    this._flipH = state.flipH;
    this._flipV = state.flipV;
    this._resizeW = state.resizeW;
    this._resizeH = state.resizeH;
    this._lockAspect = state.lockAspect;
    this._cropX = state.cropX !== undefined ? state.cropX : 0;
    this._cropY = state.cropY !== undefined ? state.cropY : 0;
    this._cropW = state.cropW !== undefined ? state.cropW : 100;
    this._cropH = state.cropH !== undefined ? state.cropH : 100;
    this._cropAspect = state.cropAspect !== undefined ? state.cropAspect : "free";
  }


  private _onCancel() {
    this.dispatchEvent(new CustomEvent("editor-close", { bubbles: true, composed: true }));
  }

  private _onZoomIn() {
    this._zoom = Math.min(200, this._zoom + 10);
  }

  private _onZoomOut() {
    this._zoom = Math.max(10, this._zoom - 10);
  }

  private _onZoomActual() {
    this._zoom = 100;
  }

  // Sliders
  private _onSliderChange(propName: string, e: Event) {
    const input = e.target as HTMLInputElement;
    const val = parseFloat(input.value);
    (this as any)[propName] = val;
    this._saveHistoryState();
  }

  private _onRotateLeft() {
    this._rotation = (this._rotation - 90) % 360;
    this._saveHistoryState();
  }

  private _onRotateRight() {
    this._rotation = (this._rotation + 90) % 360;
    this._saveHistoryState();
  }

  private _onFlipH() {
    this._flipH = !this._flipH;
    this._saveHistoryState();
  }

  private _onFlipV() {
    this._flipV = !this._flipV;
    this._saveHistoryState();
  }

  private _onResizeWInput(e: Event) {
    const input = e.target as HTMLInputElement;
    const val = parseInt(input.value, 10);
    if (!isNaN(val) && val > 0) {
      this._resizeW = val;
      if (this._lockAspect && this.selectedImage && this.selectedImage.width && this.selectedImage.height) {
        const ratio = this.selectedImage.height / this.selectedImage.width;
        this._resizeH = Math.round(val * ratio);
      }
      this._saveHistoryState();
    }
  }

  private _onResizeHInput(e: Event) {
    const input = e.target as HTMLInputElement;
    const val = parseInt(input.value, 10);
    if (!isNaN(val) && val > 0) {
      this._resizeH = val;
      if (this._lockAspect && this.selectedImage && this.selectedImage.width && this.selectedImage.height) {
        const ratio = this.selectedImage.width / this.selectedImage.height;
        this._resizeW = Math.round(val * ratio);
      }
      this._saveHistoryState();
    }
  }

  private _onToggleLockAspect() {
    this._lockAspect = !this._lockAspect;
    if (this._lockAspect && this.selectedImage && this.selectedImage.width && this.selectedImage.height) {
      const ratio = this.selectedImage.height / this.selectedImage.width;
      this._resizeH = Math.round(this._resizeW * ratio);
    }
    this._saveHistoryState();
  }

  private _onSave() {
    if (!this.selectedImage) return;
    this._isLoading = true;

    const imgElement = new Image();
    imgElement.crossOrigin = "anonymous";
    imgElement.onload = () => {
      // 1. Create a temporary canvas representing the fully rotated, flipped, and filtered image
      const tempCanvas = document.createElement("canvas");
      
      const baseW = this._resizeW || imgElement.naturalWidth;
      const baseH = this._resizeH || imgElement.naturalHeight;
      
      const angleRad = (this._rotation * Math.PI) / 180;
      const absCos = Math.abs(Math.cos(angleRad));
      const absSin = Math.abs(Math.sin(angleRad));
      const targetW = Math.round(baseW * absCos + baseH * absSin);
      const targetH = Math.round(baseW * absSin + baseH * absCos);
      
      tempCanvas.width = targetW;
      tempCanvas.height = targetH;
      
      const tempCtx = tempCanvas.getContext("2d");
      if (!tempCtx) {
        this._isLoading = false;
        return;
      }
      
      const bFactor = 1 + this._brightness / 100;
      const cFactor = 1 + this._contrast / 100;
      const sFactor = 1 + this._saturation / 100;
      const eFactor = 1 + this._exposure / 100;
      const finalBrightness = bFactor * eFactor;
      
      tempCtx.filter = `brightness(${finalBrightness}) contrast(${cFactor}) saturate(${sFactor})`;
      
      tempCtx.translate(targetW / 2, targetH / 2);
      tempCtx.rotate(angleRad);
      
      const scaleX = this._flipH ? -1 : 1;
      const scaleY = this._flipV ? -1 : 1;
      tempCtx.scale(scaleX, scaleY);
      
      tempCtx.drawImage(imgElement, -baseW / 2, -baseH / 2, baseW, baseH);
      
      // Post-draw custom pixel filters on the temp canvas
      if (this._vibrance !== 0) {
        this._applyVibranceFilter(tempCtx, targetW, targetH, this._vibrance);
      }
      if (this._sharpness > 0) {
        this._applySharpenFilter(tempCtx, targetW, targetH, this._sharpness / 100);
      }
      
      // 2. Create the final cropped canvas based on current crop percentages
      const finalCanvas = document.createElement("canvas");
      const cropLeft = Math.round((this._cropX / 100) * targetW);
      const cropTop = Math.round((this._cropY / 100) * targetH);
      const cropWidth = Math.round((this._cropW / 100) * targetW);
      const cropHeight = Math.round((this._cropH / 100) * targetH);

      finalCanvas.width = cropWidth;
      finalCanvas.height = cropHeight;

      const finalCtx = finalCanvas.getContext("2d");
      if (!finalCtx) {
        this._isLoading = false;
        return;
      }

      finalCtx.drawImage(tempCanvas, cropLeft, cropTop, cropWidth, cropHeight, 0, 0, cropWidth, cropHeight);

      try {
        const base64 = finalCanvas.toDataURL("image/jpeg", 0.95);
        this.dispatchEvent(
          new CustomEvent("editor-save", {
            detail: { imageBase64: base64 },
            bubbles: true,
            composed: true
          })
        );
        this._isLoading = false;
      } catch (err) {
        this._isLoading = false;
        alert("Failed to export canvas image edits.");
      }
    };
    
    imgElement.src = `/api/local-file?filePath=${encodeURIComponent(this.selectedImage.filePath)}`;
  }

  private _applyVibranceFilter(ctx: CanvasRenderingContext2D, w: number, h: number, value: number) {
    const amt = value / 100;
    const imgData = ctx.getImageData(0, 0, w, h);
    const data = imgData.data;
    for (let i = 0; i < data.length; i += 4) {
      const r = data[i];
      const g = data[i+1];
      const b = data[i+2];
      const max = Math.max(r, g, b);
      const min = Math.min(r, g, b);
      const l = (max + min) / 2;
      const s = max === min ? 0 : (l < 128 ? (max - min) / (max + min) : (max - min) / (510 - max - min));
      
      if (s < 1.0) {
        const factor = amt * (1.0 - s) * 1.5;
        data[i] = Math.min(255, Math.max(0, r + (r - l) * factor));
        data[i+1] = Math.min(255, Math.max(0, g + (g - l) * factor));
        data[i+2] = Math.min(255, Math.max(0, b + (b - l) * factor));
      }
    }
    ctx.putImageData(imgData, 0, 0);
  }

  private _applySharpenFilter(ctx: CanvasRenderingContext2D, w: number, h: number, mix: number) {
    const x = mix;
    const weights = [
       0, -x,  0,
      -x, 1 + 4*x, -x,
       0, -x,  0
    ];
    const imgData = ctx.getImageData(0, 0, w, h);
    const side = 3;
    const halfSide = 1;
    const src = imgData.data;
    const sw = w;
    const sh = h;
    const output = ctx.createImageData(sw, sh);
    const dst = output.data;
    
    for (let y = 0; y < sh; y++) {
      for (let xCoord = 0; xCoord < sw; xCoord++) {
        const dstOff = (y * sw + xCoord) * 4;
        let r = 0, g = 0, b = 0;
        for (let cy = 0; cy < side; cy++) {
          for (let cx = 0; cx < side; cx++) {
            const scy = y + cy - halfSide;
            const scx = xCoord + cx - halfSide;
            if (scy >= 0 && scy < sh && scx >= 0 && scx < sw) {
              const srcOff = (scy * sw + scx) * 4;
              const wt = weights[cy * side + cx];
              r += src[srcOff] * wt;
              g += src[srcOff + 1] * wt;
              b += src[srcOff + 2] * wt;
            }
          }
        }
        dst[dstOff] = Math.min(255, Math.max(0, r));
        dst[dstOff + 1] = Math.min(255, Math.max(0, g));
        dst[dstOff + 2] = Math.min(255, Math.max(0, b));
        dst[dstOff + 3] = src[dstOff + 3];
      }
    }
    ctx.putImageData(output, 0, 0);
  }

  render() {
    if (!this.selectedImage) return html``;

    const streamUrl = `/api/local-file?filePath=${encodeURIComponent(this.selectedImage.filePath)}`;

    return html`
      <!-- Header -->
      <div class="editor-header">
        <div class="header-left">
          <button class="back-btn" title="Back" @click="${this._onCancel}">
            <span class="material-symbols-outlined">arrow_back</span>
          </button>
          <span class="filename-title">${this.selectedImage.name || "Untitled Photo"}</span>
        </div>

        <div class="mode-toolbar">
          <button class="mode-btn ${this._activeTab === "adjust" ? "active" : ""}" @click="${() => this._activeTab = "adjust"}">
            <span class="material-symbols-outlined">tune</span> Adjust
          </button>
          <button class="mode-btn ${this._activeTab === "crop-resize" ? "active" : ""}" @click="${() => this._activeTab = "crop-resize"}">
            <span class="material-symbols-outlined">crop_rotate</span> Crop & Resize
          </button>
        </div>

        <div class="header-right">
          <div class="save-options-group">
            <button class="save-btn" @click="${this._onSave}">
              <span class="material-symbols-outlined">check</span> Save options
            </button>
            <button class="save-dropdown-btn" title="Save options dropdown">
              <span class="material-symbols-outlined">keyboard_arrow_down</span>
            </button>
          </div>
          <button class="cancel-btn" @click="${this._onCancel}">Cancel</button>
        </div>
      </div>

      <!-- Sub-Toolbar (Zoom, Undo, Redo, Reset) -->
      <div class="editor-subbar">
        <button class="subbar-btn" title="Zoom In" @click="${this._onZoomIn}">
          <span class="material-symbols-outlined">zoom_in</span>
        </button>
        <button class="subbar-btn" title="Zoom Out" @click="${this._onZoomOut}">
          <span class="material-symbols-outlined">zoom_out</span>
        </button>
        <button class="subbar-btn" title="Zoom 100%" @click="${this._onZoomActual}">
          <span class="material-symbols-outlined">filter_1</span>
        </button>
        <span class="zoom-text">${this._zoom}%</span>

        <div class="subbar-divider"></div>

        <button class="subbar-btn" title="Reset all changes" @click="${this._resetAll}">
          <span class="material-symbols-outlined">restart_alt</span>
        </button>
        <button class="subbar-btn" title="Undo" ?disabled="${this._historyIndex <= 0}" @click="${this._undo}">
          <span class="material-symbols-outlined">undo</span>
        </button>
        <button class="subbar-btn" title="Redo" ?disabled="${this._historyIndex >= this._history.length - 1}" @click="${this._redo}">
          <span class="material-symbols-outlined">redo</span>
        </button>
      </div>

      <!-- Main body split into viewport and sidebar -->
      <div class="editor-body">
        <div class="viewport-container">
          <div class="canvas-wrapper" style="width: ${this._zoom}%; transition: width 0.1s ease-out;">
            <img 
              class="preview-img" 
              src="${streamUrl}" 
              alt="Editor Preview"
              style="
                filter: brightness(${(1 + this._brightness / 100) * (1 + this._exposure / 100)}) contrast(${1 + this._contrast / 100}) saturate(${1 + this._saturation / 100});
                transform: rotate(${this._rotation}deg) scaleX(${this._flipH ? -1 : 1}) scaleY(${this._flipV ? -1 : 1});
              "
            />
            
            ${this._activeTab === "crop-resize" ? html`
              <div 
                class="crop-overlay-box" 
                style="left: ${this._cropX}%; top: ${this._cropY}%; width: ${this._cropW}%; height: ${this._cropH}%;"
                @mousedown="${this._onCropDragStart}"
              >
                <!-- Grid lines -->
                <div class="crop-grid-line h1"></div>
                <div class="crop-grid-line h2"></div>
                <div class="crop-grid-line v1"></div>
                <div class="crop-grid-line v2"></div>

                <!-- Corners -->
                <div class="crop-corner nw" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("nw", e)}"></div>
                <div class="crop-corner ne" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("ne", e)}"></div>
                <div class="crop-corner sw" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("sw", e)}"></div>
                <div class="crop-corner se" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("se", e)}"></div>

                <!-- Sides -->
                <div class="crop-side n" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("n", e)}"></div>
                <div class="crop-side s" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("s", e)}"></div>
                <div class="crop-side w" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("w", e)}"></div>
                <div class="crop-side e" @mousedown="${(e: MouseEvent) => this._onHandleDragStart("e", e)}"></div>
              </div>
            ` : ""}
          </div>

          ${this._activeTab === "crop-resize" ? html`
            <!-- Interactive bottom rotation wheel -->
            <div class="rotation-wheel-container">
              <span class="wheel-value">${this._rotation}°</span>
              <div class="wheel-slider-row">
                <span class="material-symbols-outlined" style="font-size: 14px; color: var(--text-dim);">rotate_left</span>
                <input 
                  type="range" 
                  class="wheel-slider" 
                  min="-180" 
                  max="180" 
                  step="1" 
                  .value="${this._rotation}" 
                  @input="${(e: Event) => this._onSliderChange("_rotation", e)}" 
                />
                <span class="material-symbols-outlined" style="font-size: 14px; color: var(--text-dim);">rotate_right</span>
              </div>
            </div>
          ` : ""}
        </div>

        <!-- Sidebar Panel with settings controls -->
        <div class="editor-sidebar">
          ${this._activeTab === "adjust" ? html`
            <!-- ADJUSTMENTS PANEL -->
            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">light_mode</span> Light
              </div>
              <div class="sliders-grid">
                <div class="control-group">
                  <div class="control-label-row">
                    <span>Brightness</span>
                    <span class="val-label">${this._brightness > 0 ? "+" : ""}${this._brightness}%</span>
                  </div>
                  <input type="range" class="control-slider" min="-100" max="100" step="1" .value="${this._brightness}" @input="${(e: Event) => this._onSliderChange("_brightness", e)}" />
                </div>

                <div class="control-group">
                  <div class="control-label-row">
                    <span>Exposure</span>
                    <span class="val-label">${this._exposure > 0 ? "+" : ""}${this._exposure}%</span>
                  </div>
                  <input type="range" class="control-slider" min="-100" max="100" step="1" .value="${this._exposure}" @input="${(e: Event) => this._onSliderChange("_exposure", e)}" />
                </div>

                <div class="control-group">
                  <div class="control-label-row">
                    <span>Contrast</span>
                    <span class="val-label">${this._contrast > 0 ? "+" : ""}${this._contrast}%</span>
                  </div>
                  <input type="range" class="control-slider" min="-100" max="100" step="1" .value="${this._contrast}" @input="${(e: Event) => this._onSliderChange("_contrast", e)}" />
                </div>
              </div>
            </div>

            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">palette</span> Color
              </div>
              <div class="sliders-grid">
                <div class="control-group">
                  <div class="control-label-row">
                    <span>Saturation</span>
                    <span class="val-label">${this._saturation > 0 ? "+" : ""}${this._saturation}%</span>
                  </div>
                  <input type="range" class="control-slider" min="-100" max="100" step="1" .value="${this._saturation}" @input="${(e: Event) => this._onSliderChange("_saturation", e)}" />
                </div>

                <div class="control-group">
                  <div class="control-label-row">
                    <span>Vibrance</span>
                    <span class="val-label">${this._vibrance > 0 ? "+" : ""}${this._vibrance}%</span>
                  </div>
                  <input type="range" class="control-slider" min="-100" max="100" step="1" .value="${this._vibrance}" @input="${(e: Event) => this._onSliderChange("_vibrance", e)}" />
                </div>
              </div>
            </div>

            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">filter_vintage</span> Details
              </div>
              <div class="sliders-grid">
                <div class="control-group">
                  <div class="control-label-row">
                    <span>Sharpness</span>
                    <span class="val-label">${this._sharpness}%</span>
                  </div>
                  <input type="range" class="control-slider" min="0" max="100" step="1" .value="${this._sharpness}" @input="${(e: Event) => this._onSliderChange("_sharpness", e)}" />
                </div>
              </div>
            </div>
          ` : html`
            <!-- CROP & RESIZE / TRANSFORMS PANEL -->
            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">transform</span> Rotate & Flip
              </div>
              <div class="transform-grid">
                <button class="transform-btn" title="Rotate -90°" @click="${this._onRotateLeft}">
                  <span class="material-symbols-outlined">rotate_left</span> Left
                </button>
                <button class="transform-btn" title="Rotate +90°" @click="${this._onRotateRight}">
                  <span class="material-symbols-outlined">rotate_right</span> Right
                </button>
                <button class="transform-btn" title="Flip Horizontal" @click="${this._onFlipH}">
                  <span class="material-symbols-outlined">flip</span> Flip Horiz
                </button>
                <button class="transform-btn" title="Flip Vertical" @click="${this._onFlipV}">
                  <span class="material-symbols-outlined">flip</span> Flip Vert
                </button>
              </div>
            </div>

            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">crop</span> Crop Presets
              </div>
              <div class="aspect-presets-grid">
                <button class="aspect-preset-btn ${this._cropAspect === "free" ? "active" : ""}" @click="${() => this._onChangeCropAspect("free")}">Free</button>
                <button class="aspect-preset-btn ${this._cropAspect === "1:1" ? "active" : ""}" @click="${() => this._onChangeCropAspect("1:1")}">1:1 Square</button>
                <button class="aspect-preset-btn ${this._cropAspect === "4:3" ? "active" : ""}" @click="${() => this._onChangeCropAspect("4:3")}">4:3 Standard</button>
                <button class="aspect-preset-btn ${this._cropAspect === "3:2" ? "active" : ""}" @click="${() => this._onChangeCropAspect("3:2")}">3:2 Photo</button>
                <button class="aspect-preset-btn ${this._cropAspect === "16:9" ? "active" : ""}" @click="${() => this._onChangeCropAspect("16:9")}">16:9 Screen</button>
              </div>
            </div>

            <div class="sidebar-section">
              <div class="section-title">
                <span class="material-symbols-outlined">aspect_ratio</span> Dimensions
              </div>
              <div class="resize-inputs-row">
                <div class="resize-field">
                  <span>Width (px)</span>
                  <div class="stepper-input-container">
                    <input type="number" min="1" .value="${this._resizeW}" @input="${this._onResizeWInput}" />
                    <div class="stepper-buttons">
                      <button class="stepper-btn up" @click="${() => this._incrementResizeW(1)}">
                        <span class="material-symbols-outlined">keyboard_arrow_up</span>
                      </button>
                      <button class="stepper-btn down" @click="${() => this._incrementResizeW(-1)}">
                        <span class="material-symbols-outlined">keyboard_arrow_down</span>
                      </button>
                    </div>
                  </div>
                </div>
                <button class="aspect-lock-btn ${this._lockAspect ? "locked" : ""}" title="Lock Aspect Ratio" @click="${this._onToggleLockAspect}">
                  <span class="material-symbols-outlined">${this._lockAspect ? "lock" : "lock_open"}</span>
                </button>
                <div class="resize-field">
                  <span>Height (px)</span>
                  <div class="stepper-input-container">
                    <input type="number" min="1" .value="${this._resizeH}" @input="${this._onResizeHInput}" />
                    <div class="stepper-buttons">
                      <button class="stepper-btn up" @click="${() => this._incrementResizeH(1)}">
                        <span class="material-symbols-outlined">keyboard_arrow_up</span>
                      </button>
                      <button class="stepper-btn down" @click="${() => this._incrementResizeH(-1)}">
                        <span class="material-symbols-outlined">keyboard_arrow_down</span>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          `}
        </div>
      </div>

      ${this._isLoading ? html`
        <div class="editor-loading">
          <div class="spinner"></div>
          <span style="font-size: 13px; font-weight: 500; color: #fff;">Rendering Edited Photo...</span>
        </div>
      ` : ""}
    `;
  }

  // Crop Drag Event Handlers
  private _onDragMoveBound = (e: MouseEvent) => this._onDragMove(e);
  private _onDragEndBound = () => this._onDragEnd();

  private _onCropDragStart(e: MouseEvent) {
    if ((e.target as HTMLElement).classList.contains("crop-corner") || (e.target as HTMLElement).classList.contains("crop-side")) {
      return;
    }
    e.preventDefault();
    e.stopPropagation();

    this._activeHandle = "move";
    this._dragStartData = {
      startX: e.clientX,
      startY: e.clientY,
      startCropX: this._cropX,
      startCropY: this._cropY,
      startCropW: this._cropW,
      startCropH: this._cropH,
      containerRect: this.shadowRoot?.querySelector(".canvas-wrapper")?.getBoundingClientRect()
    };

    window.addEventListener("mousemove", this._onDragMoveBound);
    window.addEventListener("mouseup", this._onDragEndBound);
  }

  private _onHandleDragStart(handle: string, e: MouseEvent) {
    e.preventDefault();
    e.stopPropagation();

    this._activeHandle = handle;
    this._dragStartData = {
      startX: e.clientX,
      startY: e.clientY,
      startCropX: this._cropX,
      startCropY: this._cropY,
      startCropW: this._cropW,
      startCropH: this._cropH,
      containerRect: this.shadowRoot?.querySelector(".canvas-wrapper")?.getBoundingClientRect()
    };

    window.addEventListener("mousemove", this._onDragMoveBound);
    window.addEventListener("mouseup", this._onDragEndBound);
  }

  private _onDragMove(e: MouseEvent) {
    if (!this._activeHandle || !this._dragStartData) return;

    const { startX, startY, startCropX, startCropY, startCropW, startCropH, containerRect } = this._dragStartData;
    if (!containerRect) return;

    const deltaX = ((e.clientX - startX) / containerRect.width) * 100;
    const deltaY = ((e.clientY - startY) / containerRect.height) * 100;

    let newX = startCropX;
    let newY = startCropY;
    let newW = startCropW;
    let newH = startCropH;

    if (this._activeHandle === "move") {
      newX = Math.max(0, Math.min(100 - startCropW, startCropX + deltaX));
      newY = Math.max(0, Math.min(100 - startCropH, startCropY + deltaY));
    } else {
      if (this._activeHandle.includes("w")) {
        const possibleW = startCropW - deltaX;
        const possibleX = startCropX + deltaX;
        if (possibleX >= 0 && possibleW >= 5) {
          newX = possibleX;
          newW = possibleW;
        }
      }
      if (this._activeHandle.includes("e")) {
        newW = Math.max(5, Math.min(100 - startCropX, startCropW + deltaX));
      }
      if (this._activeHandle.includes("n")) {
        const possibleH = startCropH - deltaY;
        const possibleY = startCropY + deltaY;
        if (possibleY >= 0 && possibleH >= 5) {
          newY = possibleY;
          newH = possibleH;
        }
      }
      if (this._activeHandle.includes("s")) {
        newH = Math.max(5, Math.min(100 - startCropY, startCropH + deltaY));
      }

      if (this._cropAspect !== "free") {
        let targetRatio = 1;
        if (this._cropAspect === "1:1") targetRatio = 1;
        else if (this._cropAspect === "4:3") targetRatio = 4 / 3;
        else if (this._cropAspect === "3:2") targetRatio = 3 / 2;
        else if (this._cropAspect === "16:9") targetRatio = 16 / 9;

        const containerRatio = containerRect.width / containerRect.height;
        const percentRatio = targetRatio / containerRatio;

        if (this._activeHandle === "e" || this._activeHandle === "w" || this._activeHandle === "ne" || this._activeHandle === "nw" || this._activeHandle === "se" || this._activeHandle === "sw") {
          newH = newW / percentRatio;
          if (newY + newH > 100) {
            newH = 100 - newY;
            newW = newH * percentRatio;
          }
        } else {
          newW = newH * percentRatio;
          if (newX + newW > 100) {
            newW = 100 - newX;
            newH = newW / percentRatio;
          }
        }
      }
    }

    this._cropX = newX;
    this._cropY = newY;
    this._cropW = newW;
    this._cropH = newH;
  }

  private _onDragEnd() {
    window.removeEventListener("mousemove", this._onDragMoveBound);
    window.removeEventListener("mouseup", this._onDragEndBound);
    this._activeHandle = null;
    this._dragStartData = null;
    this._saveHistoryState();
  }

  private _onChangeCropAspect(aspect: "free" | "1:1" | "3:2" | "4:3" | "16:9") {
    this._cropAspect = aspect;
    if (aspect === "free") {
      this._saveHistoryState();
      return;
    }

    let targetRatio = 1;
    if (aspect === "1:1") targetRatio = 1;
    else if (aspect === "3:2") targetRatio = 3 / 2;
    else if (aspect === "4:3") targetRatio = 4 / 3;
    else if (aspect === "16:9") targetRatio = 16 / 9;

    const containerRect = this.shadowRoot?.querySelector(".canvas-wrapper")?.getBoundingClientRect();
    if (!containerRect) {
      this._saveHistoryState();
      return;
    }

    const containerRatio = containerRect.width / containerRect.height;
    const percentRatio = targetRatio / containerRatio;

    if (percentRatio > 1) {
      this._cropW = 100;
      this._cropH = 100 / percentRatio;
      this._cropX = 0;
      this._cropY = (100 - this._cropH) / 2;
    } else {
      this._cropH = 100;
      this._cropW = 100 * percentRatio;
      this._cropY = 0;
      this._cropX = (100 - this._cropW) / 2;
    }

    this._saveHistoryState();
  }

  private _incrementResizeW(amount: number) {
    const newVal = Math.max(1, this._resizeW + amount);
    this._resizeW = newVal;
    if (this._lockAspect && this.selectedImage && this.selectedImage.width && this.selectedImage.height) {
      const ratio = this.selectedImage.height / this.selectedImage.width;
      this._resizeH = Math.round(newVal * ratio);
    }
    this._saveHistoryState();
  }

  private _incrementResizeH(amount: number) {
    const newVal = Math.max(1, this._resizeH + amount);
    this._resizeH = newVal;
    if (this._lockAspect && this.selectedImage && this.selectedImage.width && this.selectedImage.height) {
      const ratio = this.selectedImage.width / this.selectedImage.height;
      this._resizeW = Math.round(newVal * ratio);
    }
    this._saveHistoryState();
  }
}
