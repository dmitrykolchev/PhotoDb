import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import { Image } from "../types";
import thumbnailStyles from "./ThumbnailItem.css";

@customElement("thumbnail-item")
export class ThumbnailItem extends LitElement {
  @property({ type: Object }) image!: Image;
  @property({ type: Boolean }) isSelected: boolean = false;
  @property({ type: Boolean }) isChecked: boolean = false;

  static styles = thumbnailStyles;

  private _onClick() {
    this.dispatchEvent(
      new CustomEvent("image-select", {
        detail: { id: this.image.id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onDblClick() {
    this.dispatchEvent(
      new CustomEvent("image-double-click", {
        detail: { id: this.image.id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onEditClick(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.dispatchEvent(
      new CustomEvent("image-edit", {
        detail: { id: this.image.id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onCheckClick(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.dispatchEvent(
      new CustomEvent("image-check", {
        detail: { id: this.image.id, checked: !this.isChecked },
        bubbles: true,
        composed: true
      })
    );
  }

  render() {
    const img = this.image;
    if (!img) return html``;

    let dotsHtml = html``;
    if (img.flag === "Favorite") {
      dotsHtml = html`<span class="mini-dot bg-green" title="Favorite"></span>`;
    } else if (img.flag === "Process") {
      dotsHtml = html`<span class="mini-dot bg-amber" title="To Process"></span>`;
    } else if (img.flag === "Recycle") {
      dotsHtml = html`<span class="mini-dot bg-red" title="To Recycle Bin"></span>`;
    }

    const thumbUrl = `/api/local-file?filePath=${encodeURIComponent(img.filePath)}&thumb=true`;

    let imageRes = "";
    if (img.width && img.height) {
      imageRes = ` • (${img.width}x${img.height})`;
    }

    const exifParts = [];
    if (img.focalLength) exifParts.push(img.focalLength);
    if (img.aperture) exifParts.push(img.aperture);
    if (img.isoSpeed) {
      const isoStr = String(img.isoSpeed);
      if (isoStr.toLowerCase().includes("iso")) {
        exifParts.push(isoStr);
      } else {
        exifParts.push("ISO " + isoStr);
      }
    }
    if (img.exposureTime) exifParts.push(img.exposureTime);
    const exifText = exifParts.join(" • ") || "-";

    return html`
      <div 
        class="thumb-card ${this.isSelected ? 'selected' : ''}" 
        data-id="${img.id}" 
        id="thumb_${img.id}"
        @click="${this._onClick}"
        @dblclick="${this._onDblClick}"
      >
        <div class="thumb-status-indicators">${dotsHtml}</div>
        <button class="thumb-checkbox-wrap ${this.isChecked ? 'checked' : ''}" title="Select Photo" @click="${this._onCheckClick}">
          <span class="material-symbols-outlined">${this.isChecked ? 'check_box' : 'check_box_outline_blank'}</span>
        </button>
        <button class="thumb-edit-btn" title="Edit Photo" @click="${this._onEditClick}">
          <span class="material-symbols-outlined">edit</span>
        </button>
        <div class="thumb-img-wrap">
          <img src="${thumbUrl}" alt="${img.name || ''}" loading="lazy" decoding="async" referrerPolicy="no-referrer" />
        </div>
        <div class="thumb-card-footer">
          <span class="thumb-filename" title="${img.name || ''}">${img.name || ''}${imageRes}</span>
          <div class="thumb-meta-line">
            <span class="thumb-exif-specs" title="${exifText}" style="flex: 1; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; margin-right: 8px;">${exifText}</span>
            <span class="thumb-star-indicator">
              ${img.rating > 0 ? '★ ' + img.rating : ''}
            </span>
          </div>
        </div>
      </div>
    `;
  }
}
