import { LitElement, html } from 'lit';
import { customElement, property } from 'lit/decorators.js';
import styles from './lumina-adjustments.css';

@customElement('lumina-adjustments')
export class LuminaAdjustments extends LitElement {
  static styles = styles;

  @property({ type: Number }) exposure = 0;
  @property({ type: Number }) contrast = 0;
  @property({ type: Number }) highlights = 0;
  @property({ type: Number }) shadows = 0;
  @property({ type: Number }) temperature = 0;
  @property({ type: Number }) tint = 0;
  @property({ type: Number }) saturation = 0;
  @property({ type: Number }) vignette = 0;
  @property({ type: Number }) faceSmoothing = 0;
  @property({ type: Number }) detectedFacesCount = 0;

  private onSliderInput(field: string, e: Event) {
    const input = e.target as HTMLInputElement;
    const value = parseFloat(input.value);
    
    this.dispatchEvent(new CustomEvent('adjustment-changed', {
      detail: { field, value },
      bubbles: true,
      composed: true
    }));
  }

  private resetAll() {
    this.dispatchEvent(new CustomEvent('reset-adjustments', {
      bubbles: true,
      composed: true
    }));
  }

  render() {
    return html`
      <div class="adjustments-header">
        <div class="title">Studio Adjustments</div>
        <button class="btn-reset" title="Reset adjustments" @click=${this.resetAll}>
          <svg class="reset-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M21 12a9 9 0 0 0-9-9 9.75 9.75 0 0 0-6.74 2.74L3 8" />
            <path d="M3 3v5h5" />
            <path d="M3 12a9 9 0 0 0 9 9 9.75 9.75 0 0 0 6.74-2.74L21 16" />
            <path d="M16 16h5v5" />
          </svg>
        </button>
      </div>

      <div class="group-container">
        <!-- Basic Group -->
        <div class="group-title">Basic Adjustments</div>
        
        <!-- Exposure -->
        <div class="slider-item">
          <div class="slider-label-row">
            <span class="slider-name">Exposure</span>
            <span class="slider-value">${this.exposure > 0 ? `+${this.exposure.toFixed(2)}` : this.exposure.toFixed(2)} eV</span>
          </div>
          <input
            type="range"
            min="-3.0"
            max="3.0"
            step="0.05"
            .value=${this.exposure}
            @input=${(e: Event) => this.onSliderInput('exposure', e)}
          />
        </div>

        <!-- Contrast -->
        <div class="slider-item">
          <div class="slider-label-row">
            <span class="slider-name">Contrast</span>
            <span class="slider-value">${this.contrast > 0 ? `+${this.contrast}` : this.contrast}</span>
          </div>
          <input
            type="range"
            min="-50"
            max="50"
            step="1"
            .value=${this.contrast}
            @input=${(e: Event) => this.onSliderInput('contrast', e)}
          />
        </div>

        <!-- Highlights -->
        <div class="slider-item">
          <div class="slider-label-row">
            <span class="slider-name">Highlights</span>
            <span class="slider-value">${this.highlights > 0 ? `+${this.highlights}` : this.highlights}</span>
          </div>
          <input
            type="range"
            min="-100"
            max="100"
            step="2"
            .value=${this.highlights}
            @input=${(e: Event) => this.onSliderInput('highlights', e)}
          />
        </div>

        <!-- Shadows -->
        <div class="slider-item">
          <div class="slider-label-row">
            <span class="slider-name">Shadows</span>
            <span class="slider-value">${this.shadows > 0 ? `+${this.shadows}` : this.shadows}</span>
          </div>
          <input
            type="range"
            min="-100"
            max="100"
            step="2"
            .value=${this.shadows}
            @input=${(e: Event) => this.onSliderInput('shadows', e)}
          />
        </div>

        <!-- Color & Style Group -->
        <div style="border-top: 1px solid rgba(63, 63, 70, 0.4); padding-top: 14px; display: flex; flex-direction: column; gap: 14px;">
          <div class="group-title">Color & Style</div>

          <!-- Temp -->
          <div class="slider-item">
            <div class="slider-label-row">
              <span class="slider-name">Temp (Warmth)</span>
              <span class="slider-value">${this.temperature > 0 ? `+${this.temperature}` : this.temperature}</span>
            </div>
            <input
              type="range"
              class="temp-slider"
              min="-100"
              max="100"
              step="1"
              .value=${this.temperature}
              @input=${(e: Event) => this.onSliderInput('temperature', e)}
            />
          </div>

          <!-- Tint -->
          <div class="slider-item">
            <div class="slider-label-row">
              <span class="slider-name">Tint</span>
              <span class="slider-value">${this.tint > 0 ? `+${this.tint}` : this.tint}</span>
            </div>
            <input
              type="range"
              class="tint-slider"
              min="-100"
              max="100"
              step="1"
              .value=${this.tint}
              @input=${(e: Event) => this.onSliderInput('tint', e)}
            />
          </div>

          <!-- Saturation -->
          <div class="slider-item">
            <div class="slider-label-row">
              <span class="slider-name">Saturation</span>
              <span class="slider-value">${this.saturation > 0 ? `+${this.saturation}` : this.saturation}</span>
            </div>
            <input
              type="range"
              min="-100"
              max="100"
              step="1"
              .value=${this.saturation}
              @input=${(e: Event) => this.onSliderInput('saturation', e)}
            />
          </div>

          <!-- Vignette -->
          <div class="slider-item">
            <div class="slider-label-row">
              <span class="slider-name">Vignette</span>
              <span class="slider-value">${this.vignette}%</span>
            </div>
            <input
              type="range"
              min="-100"
              max="100"
              step="2"
              .value=${this.vignette}
              @input=${(e: Event) => this.onSliderInput('vignette', e)}
            />
          </div>
        </div>

        <!-- AI Face Smoothing -->
        ${this.detectedFacesCount > 0
          ? html`
              <div style="border-top: 1px solid rgba(63, 63, 70, 0.4); padding-top: 14px; display: flex; flex-direction: column; gap: 14px;">
                <div class="group-title">AI Portrait Smoothing</div>
                <div class="slider-item">
                  <div class="slider-label-row">
                    <span class="slider-name">Skin Softening</span>
                    <span class="slider-value">${this.faceSmoothing}%</span>
                  </div>
                  <input
                    type="range"
                    min="0"
                    max="100"
                    step="2"
                    .value=${this.faceSmoothing}
                    @input=${(e: Event) => this.onSliderInput('faceSmoothing', e)}
                  />
                </div>
              </div>
            `
          : ''
        }
      </div>
    `;
  }
}
