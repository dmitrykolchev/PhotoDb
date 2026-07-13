import { LitElement, html } from "lit";
import { customElement, property } from "lit/decorators.js";
import statusBarStyles from "./StatusBar.css";

@customElement("photo-status-bar")
export class StatusBar extends LitElement {
  @property({ type: Number }) totalCount = 0;
  @property({ type: Number }) librariesCount = 0;
  @property({ type: String }) databaseName = "SQLite (app.db)";
  @property({ type: String }) allocatedMemory = "142MB";
  @property({ type: String }) serverStatus = "READY";

  static styles = statusBarStyles;

  render() {
    return html`
      <footer class="status-bar">
        <div class="status-item">
          <span class="status-dot"></span>
          <span>DB_CONNECTED: ${this.databaseName}</span>
        </div>
        <div class="status-item hidden-mobile">
          <span class="material-symbols-outlined">analytics</span>
          <span>Index: ${this.totalCount.toLocaleString()} files (${this.librariesCount} Libraries tracking)</span>
        </div>
        <div class="status-item hidden-mobile">
          <span class="material-symbols-outlined">memory</span>
          <span>Allocated: ${this.allocatedMemory}</span>
        </div>
        <div class="status-item align-right">
          <span class="material-symbols-outlined">dns</span>
          <span>SERVER_STATUS: ${this.serverStatus}</span>
        </div>
      </footer>
    `;
  }
}
