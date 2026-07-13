import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import headerStyles from "./ApplicationHeader.css";

@customElement("photo-app-header")
export class ApplicationHeader extends LitElement {
  @property({ type: String }) searchQuery = "";

  static styles = headerStyles;

  private _onInput(e: Event) {
    const input = e.target as HTMLInputElement;
    this.searchQuery = input.value;
    this.dispatchEvent(
      new CustomEvent("search-change", {
        detail: { query: this.searchQuery },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onClear() {
    this.searchQuery = "";
    const input = this.shadowRoot?.querySelector("#search_input") as HTMLInputElement | null;
    if (input) {
      input.value = "";
      input.focus();
    }
    this.dispatchEvent(
      new CustomEvent("search-change", {
        detail: { query: "" },
        bubbles: true,
        composed: true
      })
    );
  }

  private _onAddLibraryClick() {
    this.dispatchEvent(
      new CustomEvent("add-library-click", {
        bubbles: true,
        composed: true
      })
    );
  }

  render() {
    return html`
      <header class="app-header">
        <div class="logo-area">
          <div class="logo-box"></div>
          <span class="logo-text">PHOTO_LAB <span class="version-label">v1.3</span></span>
        </div>
        
        <div class="search-bar-wrap">
          <span class="material-symbols-outlined search-icon">search</span>
          <input 
            id="search_input" 
            type="text" 
            .value="${this.searchQuery}"
            placeholder="Search images, tags, or metadata..." 
            autocomplete="off"
            @input="${this._onInput}"
          />
          <button 
            id="clear_search_btn" 
            class="clear-search-btn" 
            style="display: ${this.searchQuery ? 'flex' : 'none'};"
            @click="${this._onClear}"
          >
            <span class="material-symbols-outlined">close</span>
          </button>
        </div>

        <div class="actions-area">
          <button id="add_library_btn" class="btn btn-accent" @click="${this._onAddLibraryClick}">
            <span class="material-symbols-outlined btn-icon">folder_open</span>
            + Add Library
          </button>
        </div>
      </header>
    `;
  }
}
