import { LitElement, html } from 'lit';
import { customElement, property } from 'lit/decorators.js';
import styles from './lumina-neural-engine.css';

@customElement('lumina-neural-engine')
export class LuminaNeuralEngine extends LitElement {
  static styles = styles;

  @property({ type: Boolean }) isProcessing = false;
  @property({ type: Boolean }) isAnalyzingFaces = false;
  @property({ type: String }) aiDescription = '';
  @property({ attribute: false }) detectedFaces: any[] = [];
  @property({ type: String }) tool: 'hand' | 'brush' = 'hand';
  @property({ type: Boolean }) hasImage = false;

  private triggerAutoColor() {
    this.dispatchEvent(new CustomEvent('trigger-auto-color', {
      bubbles: true,
      composed: true
    }));
  }

  private triggerFaceDetect() {
    this.dispatchEvent(new CustomEvent('trigger-face-detect', {
      bubbles: true,
      composed: true
    }));
  }

  private triggerInpaint() {
    this.dispatchEvent(new CustomEvent('trigger-inpaint', {
      bubbles: true,
      composed: true
    }));
  }

  render() {
    return html`
      <div class="title">AI Neural Engine</div>
      
      <div class="button-grid">
        <button
          @click=${this.triggerAutoColor}
          ?disabled=${!this.hasImage || this.isProcessing}
        >
          <svg class="btn-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="m12 3-1.912 5.813a2 2 0 0 1-1.275 1.275L3 12l5.813 1.912a2 2 0 0 1 1.275 1.275L12 21l1.912-5.813a2 2 0 0 1 1.275-1.275L21 12l-5.813-1.912a2 2 0 0 1-1.275-1.275L12 3Z"/>
          </svg>
          Auto Tone Correction
        </button>

        <button
          @click=${this.triggerFaceDetect}
          ?disabled=${!this.hasImage || this.isAnalyzingFaces}
        >
          <svg class="btn-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2" />
            <circle cx="12" cy="7" r="4" />
          </svg>
          AI Face Detection
        </button>
      </div>

      ${this.aiDescription
        ? html`
            <div class="ai-note">
              <div class="ai-note-header">
                <svg class="ai-note-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
                  <path d="m12 3-1.912 5.813a2 2 0 0 1-1.275 1.275L3 12l5.813 1.912a2 2 0 0 1 1.275 1.275L12 21l1.912-5.813a2 2 0 0 1 1.275-1.275L21 12l-5.813-1.912a2 2 0 0 1-1.275-1.275L12 3Z"/>
                </svg>
                Neural Assist Note:
              </div>
              ${this.aiDescription}
            </div>
          `
        : ''
      }

      ${this.tool === 'brush' && this.hasImage
        ? html`
            <div class="inpaint-container">
              <div class="inpaint-header">
                <svg style="width: 14px; height: 14px;" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <circle cx="6" cy="6" r="3" />
                  <path d="M8.12 8.12 12 12" />
                  <circle cx="20" cy="4" r="1" />
                  <circle cx="14" cy="10" r="1" />
                  <circle cx="4" cy="20" r="1" />
                  <circle cx="14" cy="20" r="1" />
                  <circle cx="9.5" cy="15.5" r="1" />
                  <circle cx="18" cy="16" r="3" />
                  <path d="m18 13-1.912-5.813a2 2 0 0 0-1.275-1.275L9 4" />
                  <path d="m18 19-1.912 5.813a2 2 0 0 1-1.275 1.275L9 28" />
                </svg>
                <h4>AI Object Remover (Inpaint)</h4>
              </div>
              <p class="inpaint-desc">
                Paint red brush strokes over any unwanted object, then click below to run Content-Aware Fill.
              </p>
              <button
                class="btn-remove"
                @click=${this.triggerInpaint}
                ?disabled=${this.isProcessing}
              >
                REMOVE SELECTED OBJECT
              </button>
            </div>
          `
        : ''
      }

      ${this.detectedFaces.length > 0
        ? html`
            <div class="faces-container">
              <div class="title">AI Portraits Detected (${this.detectedFaces.length})</div>
              ${this.detectedFaces.map((face, idx) => html`
                <div class="face-card">
                  <div class="face-header">
                    <span>Face #${idx + 1} (${face.genderEstimate}, ${face.ageEstimate})</span>
                    <span class="face-badge">Active</span>
                  </div>
                  <p class="face-desc">${face.recommendation}</p>
                </div>
              `)}
            </div>
          `
        : ''
      }
    `;
  }
}
