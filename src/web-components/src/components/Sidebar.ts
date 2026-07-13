import { LitElement, html } from "lit";
import { customElement, property, state } from "lit/decorators.js";
import { Library } from "../types";
import sidebarStyles from "./Sidebar.css";

@customElement("photo-sidebar")
export class Sidebar extends LitElement {
  @property({ type: Array }) libraries: Library[] = [];
  @property({ type: String }) activeLibraryId: string | undefined = undefined;
  @property({ type: String }) editingLibraryId: string | undefined = undefined;
  @property({ type: String }) openOpsLibraryId: string | undefined = undefined;

  @property({ type: Array }) albums: any[] = [];
  @property({ type: String }) activeAlbumId: string | undefined = undefined;
  @property({ type: String }) editingAlbumId: string | undefined = undefined;
  @property({ type: String }) openOpsAlbumId: string | undefined = undefined;

  @state() private _isCreatingAlbum = false;
  @state() private _newAlbumName = "";

  static styles = sidebarStyles;

  connectedCallback() {
    super.connectedCallback();
    document.addEventListener("click", this._onDocumentClick);
  }

  disconnectedCallback() {
    super.disconnectedCallback();
    document.removeEventListener("click", this._onDocumentClick);
  }

  private _onDocumentClick = (e: Event) => {
    const target = e.target as HTMLElement;
    if (!this.contains(target)) {
      this.openOpsLibraryId = undefined;
      this.openOpsAlbumId = undefined;
    }
  };

  private _onToggleOpsAlbum(e: Event, id: string) {
    e.stopPropagation();
    this.openOpsAlbumId = this.openOpsAlbumId === id ? undefined : id;
  }

  private _onAlbumClick(e: Event, id: string) {
    const target = e.target as HTMLElement;
    if (this.editingAlbumId === id) {
      return;
    }
    if (target.closest(".nav-item-ops") || target.closest(".nav-op-btn") || target.closest(".name-edit-input") || target.closest(".more-btn")) {
      return;
    }
    this.dispatchEvent(
      new CustomEvent("album-select", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onAlbumCreateClick() {
    this._isCreatingAlbum = true;
    this._newAlbumName = "";
    this.updateComplete.then(() => {
      const input = this.shadowRoot?.querySelector(".create-album-input") as HTMLInputElement | null;
      if (input) {
        input.focus();
      }
    });
  }

  private _onNewAlbumKeyDown(e: KeyboardEvent) {
    if (e.key === "Enter") {
      e.stopPropagation();
      this._submitNewAlbum();
    } else if (e.key === "Escape") {
      e.stopPropagation();
      this._isCreatingAlbum = false;
    }
  }

  private _onNewAlbumBlur() {
    setTimeout(() => {
      if (this._isCreatingAlbum) {
        this._submitNewAlbum();
      }
    }, 150);
  }

  private _submitNewAlbum() {
    if (!this._isCreatingAlbum) return;
    const name = this._newAlbumName.trim();
    this._isCreatingAlbum = false;
    if (name) {
      this.dispatchEvent(
        new CustomEvent("album-create", {
          detail: { name },
          bubbles: true,
          composed: true
        })
      );
    }
  }

  private _onAlbumDeleteClick(e: Event, id: string) {
    e.stopPropagation();
    if (confirm("Are you sure you want to delete this album? Your photos inside it will NOT be deleted from disk.")) {
      this.dispatchEvent(
        new CustomEvent("album-delete", {
          detail: { id },
          bubbles: true,
          composed: true
        })
      );
    }
  }

  private _onAlbumEditStart(e: Event, id: string) {
    e.stopPropagation();
    this.editingAlbumId = id;
    this.updateComplete.then(() => {
      const input = this.shadowRoot?.querySelector(`input[data-album-id="${id}"]`) as HTMLInputElement | null;
      if (input) {
        input.focus();
        input.select();
      }
    });
  }

  private _onAlbumRenameSubmit(id: string, newName: string) {
    this.editingAlbumId = undefined;
    if (!newName.trim()) return;
    this.dispatchEvent(
      new CustomEvent("album-rename", {
        detail: { id, name: newName.trim() },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onAlbumRenameKeyDown(e: KeyboardEvent, id: string) {
    if (e.key === "Enter") {
      e.stopPropagation();
      const input = e.target as HTMLInputElement;
      this._onAlbumRenameSubmit(id, input.value);
    } else if (e.key === "Escape") {
      e.stopPropagation();
      this.editingAlbumId = undefined;
    }
  }

  private _onAlbumRenameBlur(e: Event, id: string) {
    const input = e.target as HTMLInputElement;
    setTimeout(() => {
      if (this.editingAlbumId === id) {
        this._onAlbumRenameSubmit(id, input.value);
      }
    }, 150);
  }

  private _onToggleOps(e: Event, id: string) {
    e.stopPropagation();
    this.openOpsLibraryId = this.openOpsLibraryId === id ? undefined : id;
  }

  private _onAllClick() {
    this.dispatchEvent(
      new CustomEvent("library-select", {
        detail: { id: undefined },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onLibraryClick(e: Event, id: string) {
    const target = e.target as HTMLElement;
    if (this.editingLibraryId === id) {
      return;
    }
    if (target.closest(".nav-item-ops") || target.closest(".nav-op-btn") || target.closest(".name-edit-input") || target.closest(".more-btn")) {
      return;
    }
    this.dispatchEvent(
      new CustomEvent("library-select", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onScanClick(e: Event, id: string) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("library-scan", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onDeleteClick(e: Event, id: string) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("library-delete", {
        detail: { id },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onEditStart(e: Event, id: string) {
    e.stopPropagation();
    this.editingLibraryId = id;
    this.updateComplete.then(() => {
      const input = this.shadowRoot?.querySelector(`input[data-id="${id}"]`) as HTMLInputElement | null;
      if (input) {
        input.focus();
        input.select();
      }
    });
  }

  private _onRenameSubmit(id: string, newName: string) {
    this.editingLibraryId = undefined;
    this.dispatchEvent(
      new CustomEvent("library-rename", {
        detail: { id, name: newName.trim() },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onRenameKeyDown(e: KeyboardEvent, id: string) {
    if (e.key === "Enter") {
      e.stopPropagation();
      const input = e.target as HTMLInputElement;
      this._onRenameSubmit(id, input.value);
    } else if (e.key === "Escape") {
      e.stopPropagation();
      this.editingLibraryId = undefined;
    }
  }

  private _onRenameBlur(e: Event, id: string) {
    const input = e.target as HTMLInputElement;
    // Delay slightly to prevent conflict with other button clicks
    setTimeout(() => {
      if (this.editingLibraryId === id) {
        this._onRenameSubmit(id, input.value);
      }
    }, 150);
  }

  private _onMoveUp(e: Event, id: string) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("library-reorder", {
        detail: { id, direction: "up" },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onMoveDown(e: Event, id: string) {
    e.stopPropagation();
    this.dispatchEvent(
      new CustomEvent("library-reorder", {
        detail: { id, direction: "down" },
        bubbles: true,
        composed: true
      })
    );
  }

  render() {
    const totalCount = this.libraries.reduce((acc, l) => acc + (l.imageCount || 0), 0);

    return html`
      <div class="section-label">Libraries</div>
      <div id="libraries_list" class="nav-list">
        <div 
          class="nav-item ${!this.activeLibraryId && !this.activeAlbumId ? 'active' : ''}" 
          id="lib_all_trigger"
          @click="${this._onAllClick}"
        >
          <span class="material-symbols-outlined icon">photo_library</span>
          <span class="nav-text">All Indexed Assets</span>
          <span class="nav-count">${totalCount}</span>
        </div>
        ${this.libraries.map((lib, index) => {
          const isActive = this.activeLibraryId === lib.id;
          const isEditing = this.editingLibraryId === lib.id;
          const isOpsOpen = this.openOpsLibraryId === lib.id;

          return html`
            <div 
              class="nav-item ${isActive ? 'active' : ''} ${isEditing ? 'editing' : ''}" 
              data-id="${lib.id}"
              @click="${(e: Event) => this._onLibraryClick(e, lib.id)}"
            >
              <span class="material-symbols-outlined icon">collections</span>
              ${isEditing ? html`
                <input
                  type="text"
                  class="name-edit-input"
                  data-id="${lib.id}"
                  .value="${lib.name || ''}"
                  @click="${(e: Event) => e.stopPropagation()}"
                  @keydown="${(e: KeyboardEvent) => this._onRenameKeyDown(e, lib.id)}"
                  @blur="${(e: Event) => this._onRenameBlur(e, lib.id)}"
                />
              ` : html`
                <span class="nav-text truncate" title="${lib.name || ''}">${lib.name || ''}</span>
              `}
              
              ${isOpsOpen ? html`
                <div class="nav-item-ops open" @click="${(e: Event) => e.stopPropagation()}">
                  <button 
                    class="nav-op-btn text-accent rename-lib-btn" 
                    title="Rename catalog"
                    @click="${(e: Event) => { this._onEditStart(e, lib.id); this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">edit</span>
                  </button>
                  <button 
                    class="nav-op-btn text-accent scan-lib-sub-btn" 
                    data-id="${lib.id}" 
                    title="Scan catalog directory on disk"
                    @click="${(e: Event) => { this._onScanClick(e, lib.id); this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">sync</span>
                  </button>
                  <button 
                    class="nav-op-btn ${index === 0 ? 'disabled' : ''}" 
                    title="Move library up"
                    ?disabled="${index === 0}"
                    @click="${(e: Event) => { this._onMoveUp(e, lib.id); if (index > 0) this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">keyboard_arrow_up</span>
                  </button>
                  <button 
                    class="nav-op-btn ${index === this.libraries.length - 1 ? 'disabled' : ''}" 
                    title="Move library down"
                    ?disabled="${index === this.libraries.length - 1}"
                    @click="${(e: Event) => { this._onMoveDown(e, lib.id); if (index < this.libraries.length - 1) this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">keyboard_arrow_down</span>
                  </button>
                  <button 
                    class="nav-op-btn text-red delete-lib-sub-btn" 
                    data-id="${lib.id}" 
                    title="Untrack directory (Keep original files on disk)"
                    @click="${(e: Event) => { this._onDeleteClick(e, lib.id); this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">delete</span>
                  </button>
                  <button 
                    class="nav-op-btn close-ops-btn" 
                    title="Dismiss options"
                    @click="${(e: Event) => { e.stopPropagation(); this.openOpsLibraryId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">close</span>
                  </button>
                </div>
              ` : html`
                <span class="nav-count">${lib.imageCount || 0}</span>
                <button 
                  class="more-btn" 
                  title="Library Options"
                  @click="${(e: Event) => this._onToggleOps(e, lib.id)}"
                >
                  <span class="material-symbols-outlined">more_vert</span>
                </button>
              `}
            </div>
          `;
        })}
      </div>

      <div class="section-header">
        <div class="section-label">Albums</div>
        <button class="add-album-header-btn" title="Create New Album" @click="${this._onAlbumCreateClick}">
          <span class="material-symbols-outlined">add</span>
        </button>
      </div>
      <div id="albums_list" class="nav-list">
        ${this._isCreatingAlbum ? html`
          <div class="nav-item editing">
            <span class="material-symbols-outlined icon">folder_special</span>
            <input
              type="text"
              class="name-edit-input create-album-input"
              .value="${this._newAlbumName}"
              placeholder="Album name..."
              @click="${(e: Event) => e.stopPropagation()}"
              @input="${(e: Event) => { this._newAlbumName = (e.target as HTMLInputElement).value; }}"
              @keydown="${(e: KeyboardEvent) => this._onNewAlbumKeyDown(e)}"
              @blur="${this._onNewAlbumBlur}"
            />
          </div>
        ` : ""}
        ${this.albums.length === 0 && !this._isCreatingAlbum ? html`
          <div class="empty-nav-state">No albums yet</div>
        ` : this.albums.map((album) => {
          const isActive = this.activeAlbumId === album.id;
          const isEditing = this.editingAlbumId === album.id;
          const isOpsOpen = this.openOpsAlbumId === album.id;

          return html`
            <div 
              class="nav-item ${isActive ? 'active' : ''} ${isEditing ? 'editing' : ''}" 
              data-album-id="${album.id}"
              @click="${(e: Event) => this._onAlbumClick(e, album.id)}"
            >
              <span class="material-symbols-outlined icon">folder_special</span>
              ${isEditing ? html`
                <input
                  type="text"
                  class="name-edit-input"
                  data-album-id="${album.id}"
                  .value="${album.name || ''}"
                  @click="${(e: Event) => e.stopPropagation()}"
                  @keydown="${(e: KeyboardEvent) => this._onAlbumRenameKeyDown(e, album.id)}"
                  @blur="${(e: Event) => this._onAlbumRenameBlur(e, album.id)}"
                />
              ` : html`
                <span class="nav-text truncate" title="${album.name || ''}">${album.name || ''}</span>
              `}

              ${isOpsOpen ? html`
                <div class="nav-item-ops open" @click="${(e: Event) => e.stopPropagation()}">
                  <button 
                    class="nav-op-btn text-accent rename-alb-btn" 
                    title="Rename album"
                    @click="${(e: Event) => { this._onAlbumEditStart(e, album.id); this.openOpsAlbumId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">edit</span>
                  </button>
                  <button 
                    class="nav-op-btn text-red delete-alb-sub-btn" 
                    data-id="${album.id}" 
                    title="Delete album (Keep original files on disk)"
                    @click="${(e: Event) => { this._onAlbumDeleteClick(e, album.id); this.openOpsAlbumId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">delete</span>
                  </button>
                  <button 
                    class="nav-op-btn close-ops-btn" 
                    title="Dismiss options"
                    @click="${(e: Event) => { e.stopPropagation(); this.openOpsAlbumId = undefined; }}"
                  >
                    <span class="material-symbols-outlined">close</span>
                  </button>
                </div>
              ` : html`
                <span class="nav-count">${album.imageCount || 0}</span>
                <button 
                  class="more-btn" 
                  title="Album Options"
                  @click="${(e: Event) => this._onToggleOpsAlbum(e, album.id)}"
                >
                  <span class="material-symbols-outlined">more_vert</span>
                </button>
              `}
            </div>
          `;
        })}
      </div>
    `;
  }
}
