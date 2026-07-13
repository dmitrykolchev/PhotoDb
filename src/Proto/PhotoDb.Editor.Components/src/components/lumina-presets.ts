import { LitElement, html } from 'lit';
import { customElement, property } from 'lit/decorators.js';
import styles from './lumina-presets.css';

export interface PresetItem {
  id: string;
  name: string;
  url: string;
  desc: string;
  category: string;
}

export const PRESET_CATALOG: PresetItem[] = [
  {
    id: "portrait",
    name: "Classic Studio Portrait",
    url: "https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=800&q=80",
    desc: "Perfect for testing AI Face Detection and skin softening shaders.",
    category: "Portrait"
  },
  {
    id: "landscape",
    name: "Mountain Sunset",
    url: "https://images.unsplash.com/photo-1475924156734-496f6cac6ec1?auto=format&fit=crop&w=800&q=80",
    desc: "Great for recovering highlights, shadow details, and white balance.",
    category: "Landscape"
  },
  {
    id: "architecture",
    name: "Urban Minimalist",
    url: "https://images.unsplash.com/photo-1513694203232-719a280e022f?auto=format&fit=crop&w=800&q=80",
    desc: "Ideal for testing Content-Aware Fill object removal on straight lines.",
    category: "Minimalist"
  }
];

@customElement('lumina-presets')
export class LuminaPresets extends LitElement {
  static styles = styles;

  @property({ type: String }) activeCatalog = 'portrait';
  @property({ type: Boolean }) hasFaces = false;

  private selectPreset(item: PresetItem) {
    this.dispatchEvent(new CustomEvent('preset-selected', {
      detail: { preset: item },
      bubbles: true,
      composed: true
    }));
  }

  private triggerFileInput() {
    const input = this.shadowRoot?.querySelector('.hidden-input') as HTMLInputElement;
    if (input) {
      input.click();
    }
  }

  private handleFileChange(e: Event) {
    const target = e.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
      this.dispatchEvent(new CustomEvent('file-selected', {
        detail: { file },
        bubbles: true,
        composed: true
      }));
    }
  }

  render() {
    return html`
      <div>
        <div class="section-title" style="margin-bottom: 8px;">Import Media</div>
        <div class="upload-area" @click=${this.triggerFileInput}>
          <svg class="upload-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4" />
            <polyline points="17 8 12 3 7 8" />
            <line x1="12" y1="3" x2="12" y2="15" />
          </svg>
          <div class="upload-text">Choose File</div>
          <div class="upload-sub">JPEG, PNG, WEBP</div>
          <input type="file" class="hidden-input" accept="image/*" @change=${this.handleFileChange} />
        </div>
      </div>

      <div class="presets-container">
        <div class="presets-header">
          <div class="section-title">Presets Catalog</div>
          <svg style="width: 14px; height: 14px; color: #52525b;" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <rect x="3" y="3" width="7" height="9" />
            <rect x="14" y="3" width="7" height="5" />
            <rect x="14" y="12" width="7" height="9" />
            <rect x="3" y="16" width="7" height="5" />
          </svg>
        </div>
        
        <div class="presets-list">
          ${PRESET_CATALOG.map((item) => html`
            <button
              class="preset-card ${this.activeCatalog === item.id ? 'active' : ''}"
              @click=${() => this.selectPreset(item)}
            >
              <div class="preset-top">
                <span class="preset-name">${item.name}</span>
                <span class="preset-category">${item.category}</span>
              </div>
              <div class="preset-desc">${item.desc}</div>
            </button>
          `)}
        </div>
      </div>

      <div class="history-section">
        <div class="section-title">History</div>
        <div class="history-list">
          <div class="history-item">Import RAW</div>
          <div class="history-item">Color Temp Adjust</div>
          <div class="history-item ${this.hasFaces ? 'active' : ''}">AI Portrait Enhancer</div>
          <div class="history-item" style="color: #3f3f46;">... Pending WebGPU Sync</div>
        </div>
      </div>
    `;
  }
}
