import { LitElement, html } from "lit";
import { customElement, property, state } from "lit/decorators.js";
import { Image, Album } from "../types";
import "./ThumbnailItem";
import imageGridStyles from "./ImageGrid.css";

@customElement("photo-image-grid")
export class ImageGrid extends LitElement {
  @property({ type: Array }) images: Image[] = [];
  @property({ type: Array }) albums: Album[] = [];
  @property({ type: Object }) selectedImage: Image | undefined = undefined;
  @property({ type: String }) activeLibraryName: string = "All Locally Indexed Images";
  @property({ type: String }) activeLibraryPath: string = "";
  @property({ type: String }) sortBy: string = "dateCreated";
  @property({ type: String }) sortOrder: "asc" | "desc" = "desc";
  @property({ type: Number }) limit: number = 48;
  @property({ type: Number }) totalCount: number = 0;
  @property({ type: Number }) currentPage: number = 1;
  @property({ type: Number }) totalPages: number = 1;
  @property({ type: Boolean }) isScanning: boolean = false;
  @property({ type: Boolean }) isVirtualMode: boolean = true;

  @state() private _checkedImageIds: Set<string> = new Set();

  public clearSelection() {
    this._checkedImageIds = new Set();
    this.requestUpdate();
  }

  private _toggleImageCheck(id: string, checked: boolean) {
    const next = new Set(this._checkedImageIds);
    if (checked) {
      next.add(id);
    } else {
      next.delete(id);
    }
    this._checkedImageIds = next;
    this.requestUpdate();
  }

  private _selectAllVisible() {
    const next = new Set(this._checkedImageIds);
    this.images.forEach(img => next.add(img.id));
    this._checkedImageIds = next;
    this.requestUpdate();
  }

  private _deselectAll() {
    this._checkedImageIds = new Set();
    this.requestUpdate();
  }

  private _handleImageCheck(e: any) {
    const { id, checked } = e.detail;
    this._toggleImageCheck(id, checked);
  }

  private async _addBatchToAlbum() {
    const select = this.renderRoot.querySelector("#batch_album_select") as HTMLSelectElement | null;
    const albumId = select?.value;
    if (!albumId) {
      alert("Пожалуйста, выберите альбом");
      return;
    }
    const imageIds = Array.from(this._checkedImageIds);
    if (imageIds.length === 0) return;

    this.dispatchEvent(
      new CustomEvent("batch-add-to-album", {
        detail: { albumId, imageIds },
        bubbles: true,
        composed: true
      })
    );
  }

  static styles = imageGridStyles;

  private _onImageClick(id: string) {
    this.dispatchEvent(
      new CustomEvent("image-select", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onImageDblClick(id: string) {
    this.dispatchEvent(
      new CustomEvent("image-double-click", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onSortChange(e: Event) {
    const select = e.target as HTMLSelectElement;
    this.dispatchEvent(
      new CustomEvent("sort-change", {
        detail: { sortBy: select.value },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onSortOrderToggle() {
    this.dispatchEvent(
      new CustomEvent("sort-order-toggle", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onLimitChange(e: Event) {
    const select = e.target as HTMLSelectElement;
    this.dispatchEvent(
      new CustomEvent("limit-change", {
        detail: { limit: parseInt(select.value, 10) },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onPageChange(page: number) {
    this.dispatchEvent(
      new CustomEvent("page-change", {
        detail: { page },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onGridScroll(e: Event) {
    if (!this.isVirtualMode) return;
    const container = e.target as HTMLDivElement;
    // Check if scrolled near the bottom (e.g. within 120px)
    if (container.scrollHeight - container.scrollTop - container.clientHeight < 120) {
      this.dispatchEvent(
        new CustomEvent("load-more", {
          bubbles: true,
          composed: true
        })
      );
    }
  }

  private _onVirtualModeToggle() {
    this.dispatchEvent(
      new CustomEvent("virtual-mode-toggle", {
        detail: { enabled: !this.isVirtualMode },
        bubbles: true,
        composed: true
      })
    );
  }

  private renderEmptyState() {
    return html`
      <div class="gallery-empty" style="grid-column: 1 / -1; display: flex; flex-direction: column; align-items: center; justify-content: center; padding: 48px; text-align: center; color: var(--text-dim);">
        <span class="material-symbols-outlined large-icon" style="font-size: 48px; opacity: 0.5; margin-bottom: 12px;">photo_library</span>
        <h3>No Indexed Photos Found</h3>
        <p style="font-size: 12px; margin-top: 4px; max-width: 320px; line-height: 1.5;">Try setting up a library, checking your filters config, or click "Scan" on your catalog to index physical assets on host disk.</p>
      </div>
    `;
  }

  private renderImagesList() {
    return this.images.map(img => {
      const isSelected = this.selectedImage && this.selectedImage.id === img.id;
      const isChecked = this._checkedImageIds.has(img.id);
      return html`
        <thumbnail-item 
          .image="${img}" 
          .isSelected="${isSelected}"
          .isChecked="${isChecked}"
        ></thumbnail-item>
      `;
    });
  }

  private renderBatchActionsBar() {
    if (this._checkedImageIds.size === 0) return html``;

    return html`
      <div class="batch-actions-bar">
        <div class="batch-left">
          <span class="batch-count-badge">${this._checkedImageIds.size}</span>
          <span>photos selected</span>
          <button class="batch-btn" style="margin-left: 8px;" @click="${this._deselectAll}">
            <span class="material-symbols-outlined" style="font-size: 14px;">close</span>
            Clear Selection
          </button>
        </div>
        <div class="batch-right">
          <select id="batch_album_select" style="height: 24px; padding: 0 8px; font-size: 11px;">
            <option value="" disabled selected>-- Select Album --</option>
            ${this.albums.map(alb => html`
              <option value="${alb.id}">${alb.name}</option>
            `)}
          </select>
          <button class="batch-btn batch-btn-primary" @click="${this._addBatchToAlbum}">
            <span class="material-symbols-outlined" style="font-size: 14px;">add</span>
            Add to Album
          </button>
        </div>
      </div>
    `;
  }

  private renderPagingStats() {
    if (this.totalCount === 0) return "0 files found";
    if (this.isVirtualMode) {
      return `Continuous Stream: Loaded ${this.images.length} of ${this.totalCount.toLocaleString()} assets`;
    }
    const startIdx = (this.currentPage - 1) * this.limit + 1;
    const endIdx = Math.min(this.currentPage * this.limit, this.totalCount);
    return `Showing ${startIdx} - ${endIdx} of ${this.totalCount.toLocaleString()} assets`;
  }

  private renderPagingButtons() {
    if (this.isVirtualMode) {
      if (this.images.length < this.totalCount) {
        return html`
          <span style="font-size: 11px; color: var(--text-dim); display: flex; align-items: center; gap: 6px;">
            <span class="spinner" style="width: 10px; height: 10px; margin: 0; border-width: 1.5px; border-color: var(--accent) transparent var(--accent) transparent; animation: spin 1s linear infinite;"></span>
            Scroll down to stream more...
          </span>
        `;
      } else {
        return html`
          <span style="font-size: 11px; color: #10b981; font-weight: 500; display: flex; align-items: center; gap: 4px;">
            <span class="material-symbols-outlined" style="font-size: 14px;">check_circle</span> All images loaded
          </span>
        `;
      }
    }

    if (this.totalCount === 0) return html``;

    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.currentPage - 2);
    let endPage = Math.min(this.totalPages, startPage + maxPagesToShow - 1);
    if (endPage - startPage + 1 < maxPagesToShow) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }

    const pages = [];
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    return html`
      <button 
        class="paging-btn" 
        id="page_prev_btn" 
        ?disabled="${this.currentPage === 1}"
        @click="${() => this._onPageChange(this.currentPage - 1)}"
      >
        <span class="material-symbols-outlined" style="font-size:12px;">chevron_left</span>
      </button>

      ${pages.map(i => {
        const isCurrent = i === this.currentPage;
        return html`
          <button 
            class="paging-btn page-num-btn" 
            data-page="${i}" 
            style="${isCurrent ? 'border-color:var(--accent); color:#FFF; background:rgba(59,130,246,0.15);' : ''}"
            @click="${() => this._onPageChange(i)}"
          >
            ${i}
          </button>
        `;
      })}

      <button 
        class="paging-btn" 
        id="page_next_btn" 
        ?disabled="${this.currentPage === this.totalPages}"
        @click="${() => this._onPageChange(this.currentPage + 1)}"
      >
        <span class="material-symbols-outlined" style="font-size:12px;">chevron_right</span>
      </button>
    `;
  }

  render() {
    return html`
      <div class="content-header" style="height: 48px; border-bottom: 1px solid var(--border); display: flex; align-items: center; justify-content: space-between; padding: 0 16px; flex-shrink: 0; background: var(--bg-secondary);">
        <div class="view-title-block">
          <span class="material-symbols-outlined icon">photo_library</span>
          <span id="active_library_title">${this.activeLibraryName}</span>
          <span id="active_library_path" class="sub-path">${this.activeLibraryPath}</span>
        </div>
        <div class="view-controls-block">
          <!-- Batch Select Button -->
          <button 
            id="batch_mode_btn" 
            class="order-toggle-btn ${this._checkedImageIds.size > 0 ? 'active' : ''}" 
            @click="${this._selectAllVisible}"
            title="Выделить все изображения на текущей странице"
            style="width: 114px; gap: 4px; display: inline-flex; align-items: center; justify-content: center;"
          >
            <span class="material-symbols-outlined" style="font-size: 14px;">checklist</span>
            <span>Select All</span>
          </button>

          <!-- Sort config selector -->
          <div class="sort-selector-wrap">
            <span class="control-label">Sort:</span>
            <select id="sort_by_select" @change="${this._onSortChange}">
              <option value="dateCreated" ?selected="${this.sortBy === "dateCreated"}">Date Created</option>
              <option value="fileName" ?selected="${this.sortBy === "fileName"}">File Name</option>
              <option value="rating" ?selected="${this.sortBy === "rating"}">Rating Progress</option>
              <option value="size" ?selected="${this.sortBy === "size"}">File Size</option>
            </select>
            <button 
              id="sort_order_toggle" 
              title="Toggle sorting order direction" 
              class="order-toggle-btn"
              @click="${this._onSortOrderToggle}"
            >
              ${this.sortOrder.toUpperCase()}
            </button>
          </div>

          <!-- Grid Items Limit selector -->
          <div class="limit-selector-wrap">
            <span class="control-label">Show:</span>
            <select id="grid_limit_select" @change="${this._onLimitChange}">
              <option value="12" ?selected="${this.limit === 12}">12</option>
              <option value="24" ?selected="${this.limit === 24}">24</option>
              <option value="48" ?selected="${this.limit === 48}">48</option>
              <option value="96" ?selected="${this.limit === 96}">96</option>
            </select>
          </div>

          <!-- Virtual Mode Toggle Button -->
          <button 
            id="virtual_mode_btn" 
            class="order-toggle-btn ${this.isVirtualMode ? 'active' : ''}" 
            @click="${this._onVirtualModeToggle}"
            title="Toggle between Paginated and Continuous Infinite Scroll Stream"
          >
            <span class="material-symbols-outlined icon-mode ${this.isVirtualMode ? 'active' : ''}">${this.isVirtualMode ? 'view_stream' : 'grid_view'}</span>
            <span class="text-mode ${this.isVirtualMode ? 'active' : ''}">${this.isVirtualMode ? 'STREAM: ON' : 'STREAM: OFF'}</span>
          </button>
        </div>
      </div>

      <!-- Batch Actions Bar -->
      ${this.renderBatchActionsBar()}

      <!-- Scanning progress bar block -->
      <div id="scanning_indicator" class="scan-status-alert" style="display: ${this.isScanning ? 'flex' : 'none'};">
        <div class="spinner"></div>
        <div class="scan-text">
          <strong>SCANNING LOCAL DIRECTORY ON DISK...</strong>
          <span>Reading bytes, mapping dimensions and calculating file hashes.</span>
        </div>
      </div>

      <!-- Scrollable images container -->
      <div id="gallery_grid_main" class="gallery-grid" @scroll="${this._onGridScroll}" @image-check="${this._handleImageCheck}">
        ${this.images.length === 0 ? this.renderEmptyState() : this.renderImagesList()}
      </div>

      <!-- Inline paging control bar -->
      <div class="pagination-footer">
        <div id="pagination_stats">${this.renderPagingStats()}</div>
        <div class="paging-buttons" id="paging_buttons_wrapper">
          ${this.renderPagingButtons()}
        </div>
      </div>
    `;
  }
}
