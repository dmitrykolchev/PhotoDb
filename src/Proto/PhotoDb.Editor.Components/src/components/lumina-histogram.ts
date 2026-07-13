import { LitElement, html } from 'lit';
import { customElement, property } from 'lit/decorators.js';
import styles from './lumina-histogram.css';

@customElement('lumina-histogram')
export class LuminaHistogram extends LitElement {
  static styles = styles;

  @property({ attribute: false }) histR: Uint8Array | null = null;
  @property({ attribute: false }) histG: Uint8Array | null = null;
  @property({ attribute: false }) histB: Uint8Array | null = null;

  private getPathData(channel: Uint8Array | null): string {
    if (!channel || channel.length === 0) return "";
    let path = "M 0 100";
    for (let i = 0; i < 256; i++) {
      const x = i;
      // Scale and cap the height values so they stay perfectly inside our SVG viewBox
      const val = channel[i];
      const y = 100 - (val / 255) * 95;
      path += ` L ${x} ${y}`;
    }
    path += " L 256 100 Z";
    return path;
  }

  render() {
    const rPath = this.getPathData(this.histR);
    const gPath = this.getPathData(this.histG);
    const bPath = this.getPathData(this.histB);
    const hasData = rPath && gPath && bPath;

    return html`
      <div class="title">Histogram (FP32 Planar)</div>
      <div class="histogram-container">
        ${hasData
          ? html`
              <svg viewBox="0 0 256 100" preserveAspectRatio="none">
                <g style="mix-blend-mode: screen;">
                  <path d="${rPath}" fill="rgba(239, 68, 68, 0.4)"></path>
                  <path d="${gPath}" fill="rgba(16, 185, 129, 0.4)"></path>
                  <path d="${bPath}" fill="rgba(59, 130, 246, 0.4)"></path>
                </g>
              </svg>
            `
          : html`
              <div class="fallback-text">NO HISTOGRAM SIGNAL</div>
            `
        }
      </div>
    `;
  }
}
