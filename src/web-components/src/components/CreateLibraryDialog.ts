import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import dialogStyles from "./CreateLibraryDialog.css";

@customElement("photo-create-library-dialog")
export class CreateLibraryDialog extends LitElement {
  @property({ type: Boolean }) active = false;

  static styles = dialogStyles;

  private _onClose() {
    this.dispatchEvent(
      new CustomEvent("close", {
        bubbles: true,
        composed: true
      })
    );
  }

  private _onSubmit(e: Event) {
    e.preventDefault();
    const nameInput = this.shadowRoot?.querySelector("#modal_input_name") as HTMLInputElement | null;
    const pathInput = this.shadowRoot?.querySelector("#modal_input_path") as HTMLInputElement | null;

    if (nameInput && pathInput) {
      const name = nameInput.value.trim();
      const path = pathInput.value.trim();
      if (name && path) {
        this.dispatchEvent(
          new CustomEvent("submit", {
            detail: { name, path },
            bubbles: true,
            composed: true
          })
        );
        // Clear inputs
        nameInput.value = "";
        pathInput.value = "";
      }
    }
  }

  render() {
    if (!this.active) return html``;

    return html`
      <div class="modal-overlay" @click="${(e: MouseEvent) => { if (e.target === e.currentTarget) this._onClose(); }}">
        <div class="modal-card">
          <div class="modal-header">
            <h3 class="modal-title">Track Local Disk Directory</h3>
            <button class="modal-close-btn" @click="${this._onClose}">
              <span class="material-symbols-outlined">close</span>
            </button>
          </div>
          <form class="library-modal-form" @submit="${this._onSubmit}">
            <div class="form-group">
              <label for="modal_input_name">Friendly Catalog Name</label>
              <input 
                type="text" 
                id="modal_input_name" 
                placeholder="e.g., Summer Travels 2026" 
                required 
              />
            </div>

            <div class="form-group">
              <label for="modal_input_path">Absolute Local Folder path on Server Host</label>
              <input 
                type="text" 
                id="modal_input_path" 
                placeholder="e.g. /home/photos/vacations or D:\\Photos3\\Summer" 
                required 
              />
              <span class="field-hint">Note: Standard server scanning security rules require this path to exist on disk.</span>
            </div>

            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" @click="${this._onClose}">Cancel</button>
              <button type="submit" class="btn btn-accent">Save & Validate</button>
            </div>
          </form>
        </div>
      </div>
    `;
  }
}
