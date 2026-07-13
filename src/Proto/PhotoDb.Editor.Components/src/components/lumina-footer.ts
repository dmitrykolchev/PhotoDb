import { LitElement, html } from 'lit';
import { customElement, property } from 'lit/decorators.js';
import styles from './lumina-footer.css';

@customElement('lumina-footer')
export class LuminaFooter extends LitElement {
  static styles = styles;

  @property({ type: Number }) width = 0;
  @property({ type: Number }) height = 0;
  @property({ type: Number }) latency = 0;

  render() {
    return html`
      <div class="footer-container">
        <div class="left-side">
          <span>Backend: .NET 10 (SIMD AVX-512)</span>
          <span>Render: WebGPU (Direct Mapping)</span>
        </div>
        <div class="right-side">
          <div class="indicator ind-blue">
            <span class="dot dot-blue"></span>
            LATENCY: ${this.latency > 0 ? `${this.latency.toFixed(1)}MS` : "N/A"}
          </div>
          <div class="indicator ind-amber">
            <span class="dot dot-amber"></span>
            MEMORY: 1.2GB FP32
          </div>
          <span>
            ${this.width > 0 ? `${this.width}x${this.height}` : "0x0"} | REC709
          </span>
        </div>
      </div>
    `;
  }
}
