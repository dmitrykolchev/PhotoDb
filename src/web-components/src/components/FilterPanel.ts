import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import { Tag, ImageFlag } from "../types";
import filterStyles from "./FilterPanel.css";

@customElement("photo-filter-panel")
export class FilterPanel extends LitElement {
  @property({ type: Number }) ratingFilter: number = 0;
  @property({ type: Array }) activeFlags: ImageFlag[] = [];
  @property({ type: Array }) activeTags: string[] = [];
  @property({ type: Array }) allTagsList: Tag[] = [];

  static styles = filterStyles;

  private _onFlagClick(flag: ImageFlag) {
    this.dispatchEvent(
      new CustomEvent("flag-toggle", {
        detail: { flag },
        bubbles: true,
        composed: true
      })
    );
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

  private _onClearRating() {
    this.dispatchEvent(
      new CustomEvent("rating-clear", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onTagClick(tagName: string) {
    this.dispatchEvent(
      new CustomEvent("tag-toggle", {
        detail: { tagName },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onClearAll() {
    this.dispatchEvent(
      new CustomEvent("clear-all", {
        bubbles: true,
        composed: true
      })
    );
  }

  render() {
    const isFavoriteActive = this.activeFlags.includes("Favorite");
    const isToProcessActive = this.activeFlags.includes("Process");
    const isToRecycleActive = this.activeFlags.includes("Recycle");

    return html`
      <div class="section-label">Flag Filters</div>
      <div class="nav-list flex-col" id="flag_filters_list">
        <div 
          class="flag-filter-item ${isFavoriteActive ? 'active' : ''}" 
          @click="${() => this._onFlagClick("Favorite")}"
        >
          <span class="flag-dot bg-green"></span>
          <span class="flag-text">Favorites</span>
          <span class="material-symbols-outlined check-icon">check</span>
        </div>
        <div 
          class="flag-filter-item ${isToProcessActive ? 'active' : ''}" 
          @click="${() => this._onFlagClick("Process")}"
        >
          <span class="flag-dot bg-amber"></span>
          <span class="flag-text">To Process</span>
          <span class="material-symbols-outlined check-icon">check</span>
        </div>
        <div 
          class="flag-filter-item ${isToRecycleActive ? 'active' : ''}" 
          @click="${() => this._onFlagClick("Recycle")}"
        >
          <span class="flag-dot bg-red"></span>
          <span class="flag-text">To Recycle Bin</span>
          <span class="material-symbols-outlined check-icon">check</span>
        </div>
      </div>

      <div class="section-label">Rating filter</div>
      <div class="rating-filter-wrapper">
        <div class="rating-filter-stars" id="rating_filter_stars">
          ${[1, 2, 3, 4, 5].map(starVal => {
            const isFilled = starVal <= this.ratingFilter;
            return html`
              <span 
                class="star-clickable material-symbols-outlined ${isFilled ? 'filled' : ''}" 
                @click="${() => this._onRatingClick(starVal)}"
              >star</span>
            `;
          })}
        </div>
        <button id="clear_rating_btn" class="clear-ref-btn" @click="${this._onClearRating}">clear</button>
      </div>

      <div class="section-label">Popular Tags</div>
      <div id="sidebar_tags_cloud" class="tags-cloud">
        ${this.allTagsList.length === 0 ? html`
          <span style="font-size:11px; color:var(--text-dim); padding:4px 0; display:block;">No tags registered yet.</span>
        ` : this.allTagsList.map(tag => {
          const isActive = this.activeTags.includes(tag.name);
          return html`
            <span 
              class="tag-pill ${isActive ? 'active' : ''}" 
              @click="${() => this._onTagClick(tag.name)}"
            >
              #${tag.name}
              <span class="count">${tag.imageCount || 0}</span>
            </span>
          `;
        })}
      </div>

      <div class="left-sidebar-footer" style="padding: 12px 8px;">
        <button id="clear_all_filters_btn" class="btn btn-tertiary w-full" @click="${this._onClearAll}">
          <span class="material-symbols-outlined">filter_alt_off</span>
          Clear All Filters
        </button>
      </div>
    `;
  }
}
