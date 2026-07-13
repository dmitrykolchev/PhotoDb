import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import { Image } from "../types";
import lightboxStyles from "./Lightbox.css";

@customElement("photo-lightbox")
export class Lightbox extends LitElement {
  @property({ type: Boolean }) active = false;
  @property({ type: Object }) image: Image | undefined = undefined;

  static styles = lightboxStyles;

  connectedCallback() {
    super.connectedCallback();
    window.addEventListener("keydown", this._onGlobalKeyDown);
  }

  disconnectedCallback() {
    super.disconnectedCallback();
    window.removeEventListener("keydown", this._onGlobalKeyDown);
  }

  private _onGlobalKeyDown = (e: KeyboardEvent) => {
    if (!this.active) return;
    if (e.key === "ArrowLeft") {
      this._onPrev(e);
    } else if (e.key === "ArrowRight") {
      this._onNext(e);
    } else if (e.key === "Escape") {
      this._onClose(e);
    }
  };

  private _onClose(e: Event) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("close", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onPrev(e: Event) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("prev", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onNext(e: Event) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("next", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onOverlayClick(e: MouseEvent) {
    const target = e.target as HTMLElement;
    if (target.classList.contains("lightbox-overlay") || target.classList.contains("lightbox-container")) {
      this._onClose(e);
    }
  }

  render() {
    if (!this.active || !this.image) return html``;

    const streamUrl = `/api/local-file?filePath=${encodeURIComponent(this.image.filePath)}`;

    return html`
      <div class="lightbox-overlay" @click="${this._onOverlayClick}">
        <button class="lightbox-close-btn" @click="${this._onClose}" title="Close Fullscreen (Esc)">
          <span class="material-symbols-outlined">close</span>
        </button>
        
        <button class="lightbox-arrow lightbox-arrow-left" @click="${this._onPrev}" title="Previous image (ArrowLeft)">
          <span class="material-symbols-outlined">chevron_left</span>
        </button>
        
        <div class="lightbox-container">
          <div class="lightbox-img-wrapper">
            <img src="${streamUrl}" alt="${this.image.name}" decoding="async" />
            <div class="lightbox-caption">
              <span class="caption-title">${this.image.name}</span>
              <span class="caption-meta">${this.image.filePath}</span>
            </div>
          </div>
        </div>

        <button class="lightbox-arrow lightbox-arrow-right" @click="${this._onNext}" title="Next image (ArrowRight)">
          <span class="material-symbols-outlined">chevron_right</span>
        </button>
      </div>
    `;
  }
}
