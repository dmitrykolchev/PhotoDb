import { LitElement, html } from 'lit';
import { customElement, property, state } from 'lit/decorators.js';
import styles from './lumina-navigator.css';

@customElement('lumina-navigator')
export class LuminaNavigator extends LitElement {
  static styles = styles;

  @property({ type: Number }) width = 0;
  @property({ type: Number }) height = 0;
  @property({ type: Number }) scale = 1.0;
  @property({ type: Number }) offsetX = 0;
  @property({ type: Number }) offsetY = 0;
  @property({ type: Number }) containerWidth = 0;
  @property({ type: Number }) containerHeight = 0;
  @property({ attribute: false }) originalRGBA: Uint8Array | null = null;

  @state() private isDragging = false;

  updated(changedProperties: Map<string, any>) {
    if (changedProperties.has('originalRGBA') || changedProperties.has('width') || changedProperties.has('height')) {
      this.updateCanvas();
    }
  }

  private updateCanvas() {
    const canvas = this.renderRoot?.querySelector('#nav-canvas') as HTMLCanvasElement;
    if (!canvas || !this.originalRGBA || this.width === 0 || this.height === 0) return;

    const navW = canvas.width;
    const navH = canvas.height;
    if (navW === 0 || navH === 0) return;

    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    // Create temporary canvas to put the original RGBA pixels
    const tempCanvas = document.createElement('canvas');
    tempCanvas.width = this.width;
    tempCanvas.height = this.height;
    const tempCtx = tempCanvas.getContext('2d');
    if (tempCtx) {
      const imgData = tempCtx.createImageData(this.width, this.height);
      imgData.data.set(this.originalRGBA);
      tempCtx.putImageData(imgData, 0, 0);

      // Scale down onto navigator canvas
      ctx.clearRect(0, 0, navW, navH);
      ctx.drawImage(tempCanvas, 0, 0, navW, navH);
    }
  }

  private handlePointerDown(e: PointerEvent) {
    const container = this.renderRoot?.querySelector('.aspect-container') as HTMLDivElement;
    if (!container) return;
    try {
      container.setPointerCapture(e.pointerId);
    } catch (err) {}
    this.isDragging = true;
    this.updatePanFromEvent(e);
  }

  private handlePointerMove(e: PointerEvent) {
    if (!this.isDragging) return;
    this.updatePanFromEvent(e);
  }

  private handlePointerUp(e: PointerEvent) {
    this.isDragging = false;
    const container = this.renderRoot?.querySelector('.aspect-container') as HTMLDivElement;
    if (container) {
      try {
        container.releasePointerCapture(e.pointerId);
      } catch (err) {}
    }
  }

  private updatePanFromEvent(e: PointerEvent) {
    const container = this.renderRoot?.querySelector('.aspect-container') as HTMLDivElement;
    if (!container || this.width === 0 || this.height === 0) return;

    const rect = container.getBoundingClientRect();
    if (rect.width === 0 || rect.height === 0) return;

    const maxNavW = 216;
    const maxNavH = 120;
    let navW = maxNavW;
    let navH = maxNavH;

    const aspect = this.width / this.height;
    if (aspect > maxNavW / maxNavH) {
      navW = maxNavW;
      navH = maxNavW / aspect;
    } else {
      navH = maxNavH;
      navW = maxNavH * aspect;
    }

    // Standardize client coordinates relative to the inner content (excluding 1px border)
    const px = Math.max(0, Math.min(navW, e.clientX - rect.left - 1));
    const py = Math.max(0, Math.min(navH, e.clientY - rect.top - 1));

    const miniScale = navW / this.width;

    // Calculate bounding box dimensions in the navigator
    const boxW = (this.containerWidth / this.scale) * miniScale;
    const boxH = (this.containerHeight / this.scale) * miniScale;

    // Bounding box top-left corner should be centered at pointer (px, py)
    const boxX = px - boxW / 2;
    const boxY = py - boxH / 2;

    // Map back to viewport offsets directly using the elegant scaling ratio
    const scaleOverMini = this.scale / miniScale;
    let newOffsetX = -boxX * scaleOverMini;
    let newOffsetY = -boxY * scaleOverMini;

    // Clamp the mapped offsets using the exact same generous clamp logic as the main viewport
    const margin = 40;
    const scaledWidth = this.width * this.scale;
    const scaledHeight = this.height * this.scale;

    const minOffsetX = margin - scaledWidth;
    const maxOffsetX = this.containerWidth - margin;
    newOffsetX = Math.max(minOffsetX, Math.min(maxOffsetX, newOffsetX));

    const minOffsetY = margin - scaledHeight;
    const maxOffsetY = this.containerHeight - margin;
    newOffsetY = Math.max(minOffsetY, Math.min(maxOffsetY, newOffsetY));

    this.dispatchEvent(new CustomEvent('navigator-pan', {
      detail: { offsetX: newOffsetX, offsetY: newOffsetY },
      bubbles: true,
      composed: true
    }));
  }

  render() {
    const maxNavW = 216;
    const maxNavH = 120;
    let navW = maxNavW;
    let navH = maxNavH;

    if (this.width > 0 && this.height > 0) {
      const aspect = this.width / this.height;
      if (aspect > maxNavW / maxNavH) {
        navW = maxNavW;
        navH = maxNavW / aspect;
      } else {
        navH = maxNavH;
        navW = maxNavH * aspect;
      }
    }

    // Viewport bounding box calculation
    let boxX = 0;
    let boxY = 0;
    let boxW = navW;
    let boxH = navH;

    if (this.width > 0 && this.height > 0 && this.containerWidth > 0 && this.containerHeight > 0) {
      const miniScale = navW / this.width;

      const imgVisibleXMin = -this.offsetX / this.scale;
      const imgVisibleYMin = -this.offsetY / this.scale;
      const imgVisibleXMax = (-this.offsetX + this.containerWidth) / this.scale;
      const imgVisibleYMax = (-this.offsetY + this.containerHeight) / this.scale;

      boxX = imgVisibleXMin * miniScale;
      boxY = imgVisibleYMin * miniScale;
      boxW = Math.max(4, (imgVisibleXMax - imgVisibleXMin) * miniScale);
      boxH = Math.max(4, (imgVisibleYMax - imgVisibleYMin) * miniScale);
    }

    return html`
      <div class="title">Навигатор</div>
      <div 
        class="aspect-container" 
        style="width: ${navW}px; height: ${navH}px;"
        @pointerdown=${this.handlePointerDown}
        @pointermove=${this.handlePointerMove}
        @pointerup=${this.handlePointerUp}
      >
        <canvas id="nav-canvas" width="${navW}" height="${navH}"></canvas>
        <div class="grid-overlay"></div>
        <div 
          class="bounding-box"
          style="left: ${boxX}px; top: ${boxY}px; width: ${boxW}px; height: ${boxH}px;"
        ></div>
      </div>
    `;
  }
}

declare global {
  interface HTMLElementTagNameMap {
    'lumina-navigator': LuminaNavigator;
  }
}
