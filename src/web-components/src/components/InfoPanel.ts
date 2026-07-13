import { LitElement, html } from "lit";
import { customElement, property, state } from "lit/decorators.js";
import { Image as AppImage, Library, Tag, ImageFlag } from "../types";
import infoPanelStyles from "./InfoPanel.css";

@customElement("photo-info-panel")
export class InfoPanel extends LitElement {
  @property({ type: Object }) selectedImage: AppImage | undefined = undefined;
  @property({ type: Array }) libraries: Library[] = [];
  @property({ type: Array }) allTagsList: Tag[] = [];
  @property({ type: Array }) albums: any[] = [];

  @state() private _tagInput: string = "";
  @state() private _showAutocomplete: boolean = false;

  static styles = infoPanelStyles;

  private _onOpenEditorDialog() {
    this.dispatchEvent(
      new CustomEvent("open-image-editor", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onRemoveFromAlbum(albumId: string) {
    this.dispatchEvent(
      new CustomEvent("album-remove", {
        detail: { albumId },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onAddToAlbumSubmit() {
    const select = this.renderRoot.querySelector("#add_to_album_select") as HTMLSelectElement | null;
    const albumId = select?.value;
    if (albumId) {
      this.dispatchEvent(
        new CustomEvent("album-add", {
          detail: { albumId },
          bubbles: true,
          composed: true
        })
      );
    }
  }

  private _onRatingClick(rating: number) {
    this.dispatchEvent(
      new CustomEvent("rating-change", {
        detail: { rating },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onFlagClick(flag: ImageFlag) {
    this.dispatchEvent(
      new CustomEvent("flag-change", {
        detail: { flag },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onRemoveTag(tagName: string) {
    this.dispatchEvent(
      new CustomEvent("tag-remove", {
        detail: { tagName },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onAddTagSubmit(e: Event) {
    e.preventDefault();
    const input = this.renderRoot.querySelector("#tag_append_input") as HTMLInputElement;
    const value = input?.value.trim();
    if (value) {
      this.dispatchEvent(
        new CustomEvent("tag-add", {
          detail: { tagName: value },
          bubbles: true,
          composed: true
        })
      );
      if (input) input.value = "";
      this._tagInput = "";
      this._showAutocomplete = false;
    }
  }

  private _onTagInput(e: Event) {
    const input = e.target as HTMLInputElement;
    this._tagInput = input.value.trim().toLowerCase();
    this._showAutocomplete = this._tagInput.length > 0;
  }

  private _onSelectSuggestion(tagName: string) {
    this.dispatchEvent(
      new CustomEvent("tag-add", {
        detail: { tagName },
        bubbles: true,
        composed: true
      })
    );
    const input = this.renderRoot.querySelector("#tag_append_input") as HTMLInputElement;
    if (input) input.value = "";
    this._tagInput = "";
    this._showAutocomplete = false;
  }

  private _onSaveMemo() {
    const textarea = this.renderRoot.querySelector("#detail_memo_input") as HTMLTextAreaElement;
    if (textarea) {
      this.dispatchEvent(
        new CustomEvent("save-memo", {
          detail: { memo: textarea.value },
          bubbles: true,
          composed: true
        })
      );
    }
  }

  private async _onCopyPath(path: string) {
    try {
      await navigator.clipboard.writeText(path);
      // Also bubble the copy-path event in case anyone is listening
      this.dispatchEvent(
        new CustomEvent("copy-path", {
          detail: { path },
          bubbles: true,
          composed: true
        })
      );
    } catch (err) {
      console.error("Could not copy path: ", err);
    }
  }

  private _onFullscreen() {
    this.dispatchEvent(
      new CustomEvent("fullscreen", {
        bubbles: true,
        composed: true
      })
    );
  }

  private renderEmptyState() {
    return html`
      <div id="metadata_empty_view" class="sidebar-empty-state" style="text-align: center; color: var(--text-dim); display: flex; flex-direction: column; align-items: center; justify-content: center; flex: 1;">
        <span class="material-symbols-outlined font-large-icon" style="font-size: 48px; opacity: 0.5; margin-bottom: 12px;">info</span>
        <p style="font-size: 12px; line-height: 1.5; max-width: 180px;">Select an image thumbnail from the gallery grid to inspect file properties, assign tags, rate photo metadata, or configure status flags.</p>
      </div>
    `;
  }

  private renderDetailsState() {
    const img = this.selectedImage!;
    const streamUrl = `/api/local-file?filePath=${encodeURIComponent(img.filePath)}&thumb=true`;
    const owner = this.libraries.find(l => l.id === img.libraryId);
    const libraryName = owner ? owner.name : img.libraryId;

    const hasAnyExif = img.cameraModel || img.lensModel || img.focalLength || img.aperture || img.isoSpeed || img.exposureTime || img.shootingDate;

    // Filter autocomplete suggestions
    const alreadyHas = img.tags ? img.tags.map(t => t.toLowerCase()) : [];
    const suggestions = this.allTagsList.filter(t => 
      t.name.toLowerCase().includes(this._tagInput) && 
      !alreadyHas.includes(t.name.toLowerCase())
    ).slice(0, 5);

    return html`
      <div id="metadata_selected_view" style="display: flex; flex-direction: column; gap: 6px; flex: 1;">
        <div class="preview-img-box">
          <img id="detail_img" src="${streamUrl}" alt="Active selection" decoding="async" />
          <button id="fullscreen_trigger" class="fullscreen-trigger-btn" title="Open High-Res Lightbox" @click="${this._onFullscreen}">
            <span class="material-symbols-outlined">zoom_in</span>
          </button>
        </div>

        <h2 id="detail_filename" class="detail-title">${img.name || ''}</h2>
        <div class="property-row">
          <span class="prop-key">File Size</span>
          <span id="detail_prop_size" class="prop-val">${this.formatBytes(img.size)}</span>
        </div>

        <div class="property-row">
          <span class="prop-key">Resolution</span>
          <span id="detail_prop_resolution" class="prop-val">${img.width && img.height ? `${img.width} × ${img.height}` : "-"}</span>
        </div>

        <p id="detail_date" class="detail-subtitle">Created: ${this.formatDate(img.created)}</p>

        <!-- Interactive rating picker -->
        <div class="inspector-row">
          <span>Rating</span>
          <div id="detail_rating_stars" class="detail-rating">
            ${[1, 2, 3, 4, 5].map(val => {
              const isFilled = val <= img.rating;
              return html`
                <span 
                  class="star-clickable material-symbols-outlined ${isFilled ? 'filled' : ''}" 
                  @click="${() => this._onRatingClick(val)}"
                >star</span>
              `;
            })}
          </div>
        </div>

        <!-- Status flags selector buttons -->
        <div class="flag-selector-section-label">Flags Status</div>
        <div class="flag-buttons-grid">
          ${["None", "Favorite", "Process", "Recycle"].map(f => {
            const label = f === "None" ? "None" : f === "Favorite" ? "Favorite" : f === "Process" ? "Process" : "Recycle";
            const colorClass = f === "None" ? "bg-dim" : f === "Favorite" ? "bg-green" : f === "Process" ? "bg-amber" : "bg-red";
            const isMatch = img.flag === f;
            return html`
              <button 
                class="flag-action-btn" 
                data-flag="${f}" 
                data-state="${isMatch ? `active-${f}` : 'None'}"
                @click="${() => this._onFlagClick(f as ImageFlag)}"
              >
                <span class="flag-dot ${colorClass}"></span>${label}
              </button>
            `;
          })}
        </div>

        <div class="divider"></div>

        <!-- Image Tag operations -->
        <div class="section-mini-label">Assigned Tags & Memo</div>
        <div id="detail_tags_container" class="assigned-tags-row">
          ${(!img.tags || img.tags.length === 0) ? html`
            <span style="font-size:11px; color:var(--text-dim);">No tags assigned.</span>
          ` : img.tags.map(t => html`
            <span class="tag-badge">
              #${t}
              <button class="tag-remove-btn" @click="${() => this._onRemoveTag(t)}" title="Unassign tag">×</button>
            </span>
          `)}
        </div>

        <!-- New Tag Autocomplete wrapper form -->
        <form 
          id="tag_append_form" 
          class="tag-append-form" 
          autocomplete="off" 
          style="position: relative;"
          @submit="${this._onAddTagSubmit}"
        >
          <input 
            id="tag_append_input" 
            type="text" 
            placeholder="Tag (e.g. Portrait, Retro)..." 
            required 
            @input="${this._onTagInput}"
          />
          <button type="submit" class="btn btn-small">
            <span class="material-symbols-outlined" style="font-size: 14px;">add</span>
          </button>
          
          <div 
            id="tag_autocomplete_box" 
            class="autocomplete-dropdown" 
            style="display: ${this._showAutocomplete && suggestions.length > 0 ? 'block' : 'none'}; position: absolute; left: 0; right: 0; bottom: 32px; z-index: 100;"
          >
            <div class="header">Suggestions</div>
            ${suggestions.map(m => html`
              <button 
                type="button" 
                class="autocomplete-item" 
                @click="${() => this._onSelectSuggestion(m.name)}"
              >
                #${m.name} (${m.imageCount || 0})
              </button>
            `)}
          </div>
        </form>

        <!-- Image Description / Memo section -->
        <div class="memo-container">
          <textarea
            id="detail_memo_input"
            .value="${img.description || ""}"
            placeholder="Add image description or memo notes here..." 
            style="width: calc(100% - 14px); height: 96px; resize: none; background: var(--bg-primary); border: 1px solid var(--border); border-radius: 4px; padding: 4px 6px; font-size: 11px; color: #fff; outline: none; transition: border-color 0.2s;"
          ></textarea>
          <div style="display: flex; justify-content: flex-end; margin-top: 4px;">
            <button 
              id="save_memo_btn" 
              class="btn btn-accent" 
              style="font-size: 10px; height: 20px; padding: 2px 6px; line-height: 1;"
              @click="${this._onSaveMemo}"
            >Save Memo</button>
          </div>
        </div>

        <!-- EXIF Metadata properties divider & section -->
        <div id="exif_section" style="display: ${hasAnyExif ? 'contents' : 'none'};">
          <div class="divider"></div>
          <div class="section-mini-label">EXIF Metadata</div>
          
          <div class="property-row">
            <span class="prop-key">Camera Model</span>
            <span id="detail_exif_camera" class="prop-val">${img.cameraModel || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">Lens Model</span>
            <span id="detail_exif_lens" class="prop-val">${img.lensModel || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">Focal Length</span>
            <span id="detail_exif_focal" class="prop-val">${img.focalLength || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">Aperture</span>
            <span id="detail_exif_aperture" class="prop-val">${img.aperture || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">ISO Speed</span>
            <span id="detail_exif_iso" class="prop-val">${img.isoSpeed || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">Exposure</span>
            <span id="detail_exif_exposure" class="prop-val">${img.exposureTime || "-"}</span>
          </div>
          <div class="property-row">
            <span class="prop-key">Shooting Date</span>
            <span id="detail_exif_date" class="prop-val">${img.shootingDate || "-"}</span>
          </div>
        </div>

        <div class="divider"></div>

        <!-- Albums list and insertion dropdown -->
        <div class="section-mini-label">Albums</div>
        <div class="assigned-albums-row" style="display: flex; flex-wrap: wrap; gap: 4px; margin-bottom: 6px;">
          ${(!img.albums || img.albums.length === 0) ? html`
            <span style="font-size:11px; color:var(--text-dim);">Not in any albums.</span>
          ` : img.albums.map(alb => html`
            <span class="album-badge" style="background: rgba(147, 51, 234, 0.15); border: 1px solid rgba(147, 51, 234, 0.4); color: #d8b4fe; font-size: 11px; padding: 2px 6px; border-radius: 4px; display: inline-flex; align-items: center; gap: 4px;">
              <span class="material-symbols-outlined" style="font-size: 12px; font-variation-settings: 'FILL' 1;">folder_special</span>
              ${alb.name}
              <button class="album-remove-btn" @click="${() => this._onRemoveFromAlbum(alb.id)}" style="background: none; border: none; color: #f472b6; cursor: pointer; padding: 0; font-size: 12px; font-weight: bold; line-height: 1; margin-left: 2px;" title="Remove from album">×</button>
            </span>
          `)}
        </div>

        ${(() => {
          const currentAlbumIds = img.albums ? img.albums.map((a: any) => a.id) : [];
          const availableAlbums = this.albums.filter(a => !currentAlbumIds.includes(a.id));
          if (availableAlbums.length === 0) return html``;
          return html`
            <div style="display: flex; gap: 4px; align-items: center; margin-top: 4px;">
              <select id="add_to_album_select" style="flex: 1; font-size: 11px; background: var(--bg-primary); border: 1px solid var(--border); border-radius: 4px; color: #fff; padding: 2px 4px; height: 24px; outline: none;">
                ${availableAlbums.map(alb => html`
                  <option value="${alb.id}">${alb.name}</option>
                `)}
              </select>
              <button class="btn btn-accent" @click="${this._onAddToAlbumSubmit}" style="height: 24px; font-size: 11px; padding: 0 8px; display: flex; align-items: center; gap: 4px; white-space: nowrap;">
                <span class="material-symbols-outlined" style="font-size: 12px;">add</span> Add to Album
              </button>
            </div>
          `;
        })()}

        <div class="divider"></div>

        <!-- File properties -->
        <div class="section-mini-label">File Properties</div>
        
        <div class="property-row">
          <span class="prop-key">Absolute Path</span>
          <div class="prop-val-row">
            <span id="detail_prop_path" class="prop-val truncate select-text" title="${img.filePath}">${img.filePath}</span>
            <button id="copy_path_btn" title="Copy Path" class="mini-icon-btn" @click="${() => this._onCopyPath(img.filePath)}">
              <span class="material-symbols-outlined">content_copy</span>
            </button>
          </div>
        </div>

        <div class="property-row">
          <span class="prop-key">File Size</span>
          <span id="detail_prop_size" class="prop-val">${this.formatBytes(img.size)}</span>
        </div>

        <div class="property-row">
          <span class="prop-key">Resolution</span>
          <span id="detail_prop_resolution" class="prop-val">${img.width && img.height ? `${img.width} × ${img.height}` : "-"}</span>
        </div>

        <div class="property-row">
          <span class="prop-key">Content Hash</span>
          <span id="detail_prop_hash" class="prop-val font-mono truncate-mid">${img.hash || "Not Scanned"}</span>
        </div>

        <div class="property-row">
          <span class="prop-key">Library Owner</span>
          <span id="detail_prop_library" class="prop-val">${libraryName || ''}</span>
        </div>
      </div>
    `;
  }

  render() {
    return html`
      <style>
        .star-clickable {
          font-variation-settings: 'FILL' 0;
        }

        .star-clickable.filled {
          color: var(--color-amber);
          font-variation-settings: 'FILL' 1;
        }
      </style>
      
      <div class="sidebar-right-inner" style="height: 100%; display: flex; flex-direction: column;">
        ${!this.selectedImage ? this.renderEmptyState() : this.renderDetailsState()}
      </div>
    `;
  }

  // UTILS
  private formatDate(isoString: string): string {
    if (!isoString) return "-";
    try {
      const d = new Date(isoString);
      if (isNaN(d.getTime())) return isoString;
      return d.toLocaleString("en-GB", {
        day: "2-digit",
        month: "short",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit"
      });
    } catch {
      return isoString;
    }
  }

  private formatBytes(bytes: number): string {
    if (!bytes && bytes !== 0) return "-";
    if (bytes === 0) return "0 Bytes";
    const k = 1024;
    const sizes = ["Bytes", "KB", "MB", "GB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + " " + sizes[i];
  }
}
