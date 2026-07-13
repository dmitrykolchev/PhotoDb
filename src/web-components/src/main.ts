import { Library, Image, Tag, ImageFlag, ScanResult, Album } from "./types";
import * as SignalR from "@microsoft/signalr";

// Force load registration side-effects
import "./components/Sidebar";
import "./components/FilterPanel";
import "./components/InfoPanel";
import "./components/ImageGrid";
import "./components/StatusBar";
import "./components/ApplicationHeader";
import "./components/Lightbox";
import "./components/CreateLibraryDialog";
import "./components/ImageEditorDialog";

import { Sidebar } from "./components/Sidebar";
import { FilterPanel } from "./components/FilterPanel";
import { InfoPanel } from "./components/InfoPanel";
import { ImageGrid } from "./components/ImageGrid";
import { StatusBar } from "./components/StatusBar";
import { ApplicationHeader } from "./components/ApplicationHeader";
import { Lightbox } from "./components/Lightbox";
import { CreateLibraryDialog } from "./components/CreateLibraryDialog";
import { ImageEditorDialog } from "./components/ImageEditorDialog";

// ==========================================
// FRONT-END APP STATE ENGINE
// ==========================================
let libraries: Library[] = [];
let activeLibraryId: string | undefined = undefined;
let albums: Album[] = [];
let activeAlbumId: string | undefined = undefined;
let images: Image[] = [];
let selectedImage: Image | undefined = undefined;

// Filter fields
let searchQuery: string = "";
let ratingFilter: number = 0; // 0 = no rating filter, 1-5 = min stars
let activeFlags: ImageFlag[] = [];
let activeTags: string[] = [];

// Sorting & Paging states
let sortBy: "dateCreated" | "fileName" | "rating" | "size" = "dateCreated";
let sortOrder: "asc" | "desc" = "desc";
let currentPage: number = 1;
let limit: number = 48;
let totalPages: number = 1;
let totalCount: number = 0;

// Virtual mode & Infinite-scroll state tracking
let isVirtualMode: boolean = true;
let isFetching: boolean = false;

// Tag Autocomplete Memory
let allTagsList: Tag[] = [];

// Component Instances
let sidebarComp: Sidebar;
let filterPanelComp: FilterPanel;
let infoPanelComp: InfoPanel;
let imageGridComp: ImageGrid;
let statusBarComp: StatusBar;
let appHeaderComp: ApplicationHeader;
let libraryDialogComp: CreateLibraryDialog;
let lightboxComp: Lightbox;
let editorDialogComp: ImageEditorDialog;

// ==========================================
// CENTRAL APPLICATION ENTRUST
// ==========================================
document.addEventListener("DOMContentLoaded", () => {
    init();
});

const connection = new SignalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

connection.on("messageReceived", (username: string, message: string) => {
    console.log(`${username} - ${message}`)
});

connection.start().catch((err) => console.debug(err));


function init() {
    // Query UI Component Instances
    sidebarComp = document.getElementById("sidebar_comp") as Sidebar;
    filterPanelComp = document.getElementById("filter_panel_comp") as FilterPanel;
    infoPanelComp = document.getElementById("info_panel_comp") as InfoPanel;
    imageGridComp = document.getElementById("image_grid_comp") as ImageGrid;
    statusBarComp = document.getElementById("status_bar_comp") as StatusBar;
    appHeaderComp = document.getElementById("app_header_comp") as ApplicationHeader;
    libraryDialogComp = document.getElementById("library_dialog_comp") as CreateLibraryDialog;
    lightboxComp = document.getElementById("lightbox_comp") as Lightbox;
    editorDialogComp = document.getElementById("editor_dialog_comp") as ImageEditorDialog;

    // Bind standard layout event listeners
    bindEvents();

    // Load initial catalog data
    fetchLibraries();
    fetchAlbums();
    fetchTagsCloud();
    fetchImages();
}



// ==========================================
// API CLIENT CALLS
// ==========================================
function sortLibrariesBySavedOrder(libsList: Library[]): Library[] {
    try {
        const savedOrderStr = localStorage.getItem("photolab_library_order");
        if (savedOrderStr) {
            const savedIds: string[] = JSON.parse(savedOrderStr);
            const libsMap = new Map(libsList.map(l => [l.id, l]));
            const sorted: Library[] = [];

            for (const id of savedIds) {
                const lib = libsMap.get(id);
                if (lib) {
                    sorted.push(lib);
                    libsMap.delete(id);
                }
            }
            for (const lib of libsMap.values()) {
                sorted.push(lib);
            }
            return sorted;
        }
    } catch (e) {
        console.error("Failed to parse library order", e);
    }
    return libsList;
}

function saveLibraryOrder(libsList: Library[]) {
    const ids = libsList.map(l => l.id);
    localStorage.setItem("photolab_library_order", JSON.stringify(ids));
}

async function fetchLibraries() {
    try {
        const res = await fetch("/api/libraries");
        if (!res.ok) throw new Error("Could not pull libraries list");
        const rawLibs = await res.json();
        libraries = sortLibrariesBySavedOrder(rawLibs);

        renderLibraries();
        renderInfoPanel();
    } catch (err: any) {
        showToast(err.message || "Error reading libraries", "error");
    }
}

async function fetchAlbums() {
    try {
        const res = await fetch("/api/albums");
        if (!res.ok) throw new Error("Could not pull albums list");
        albums = await res.json();

        renderAlbums();
        renderInfoPanel();
    } catch (err: any) {
        showToast(err.message || "Error reading albums", "error");
    }
}

function renderAlbums() {
    if (sidebarComp) {
        sidebarComp.albums = [...albums];
        sidebarComp.activeAlbumId = activeAlbumId;
        sidebarComp.requestUpdate();
    }
}

async function fetchTagsCloud() {
    try {
        const res = await fetch("/api/tags");
        if (!res.ok) throw new Error("Could not loading tags cloud");
        allTagsList = await res.json();

        renderTagsCloud();
        renderInfoPanel();
    } catch (err: any) {
        showToast(err.message || "Failed pulling tag statistics", "error");
    }
}

async function fetchImages(isAppend: boolean = false) {
    if (isFetching) return;
    isFetching = true;
    try {
        // Construct search queries params
        const params = new URLSearchParams();
        if (activeLibraryId) params.append("libraryId", activeLibraryId);
        if (activeAlbumId) params.append("albumId", activeAlbumId);
        if (searchQuery.trim() !== "") params.append("search", searchQuery.trim());
        if (ratingFilter > 0) params.append("minRating", ratingFilter.toString());

        if (activeFlags.length > 0) params.append("flags", activeFlags.join(","));
        if (activeTags.length > 0) params.append("tags", activeTags.join(","));

        params.append("sortBy", sortBy);
        params.append("sortOrder", sortOrder);
        params.append("page", currentPage.toString());
        params.append("limit", limit.toString());

        const res = await fetch(`/api/images?${params.toString()}`);
        if (!res.ok) throw new Error("Gallery indexing query failed");

        const data = await res.json();
        if (isAppend) {
            const existingIds = new Set(images.map(img => img.id));
            const newUnique = data.images.filter((img: any) => !existingIds.has(img.id));
            images = [...images, ...newUnique];
        } else {
            images = data.images;
        }
        totalCount = data.totalCount;
        totalPages = data.totalPages;

        renderImageGrid();
        updateStatusLine();
    } catch (err: any) {
        showToast(err.message || "Could not retrieve photos", "error");
    } finally {
        isFetching = false;
    }
}

// ==========================================
// REACTIVE UPDATES RENDERS
// ==========================================

function renderLibraries() {
    if (sidebarComp) {
        sidebarComp.libraries = [...libraries];
        sidebarComp.activeLibraryId = activeLibraryId;
    }
}

function renderTagsCloud() {
    if (filterPanelComp) {
        filterPanelComp.allTagsList = [...allTagsList];
        filterPanelComp.activeTags = [...activeTags];
        filterPanelComp.ratingFilter = ratingFilter;
        filterPanelComp.activeFlags = [...activeFlags];
        filterPanelComp.requestUpdate();
    }
}

function renderImageGrid() {
    if (imageGridComp) {
        imageGridComp.images = images;
        imageGridComp.albums = albums;
        imageGridComp.selectedImage = selectedImage;
        imageGridComp.sortBy = sortBy;
        imageGridComp.sortOrder = sortOrder;
        imageGridComp.limit = limit;
        imageGridComp.totalCount = totalCount;
        imageGridComp.currentPage = currentPage;
        imageGridComp.totalPages = totalPages;
        imageGridComp.isVirtualMode = isVirtualMode;

        const activeLib = libraries.find(l => l.id === activeLibraryId);
        const activeAlb = albums.find(a => a.id === activeAlbumId);
        imageGridComp.activeLibraryName = activeAlb ? `Album: ${activeAlb.name}` : (activeLib ? activeLib.name : "All Locally Indexed Images");
        imageGridComp.activeLibraryPath = activeLib ? activeLib.path : "";
        imageGridComp.requestUpdate();
    }
}

function renderInfoPanel() {
    if (infoPanelComp) {
        infoPanelComp.selectedImage = selectedImage;
        infoPanelComp.libraries = libraries;
        infoPanelComp.allTagsList = allTagsList;
        (infoPanelComp as any).albums = albums;
    }
}

async function selectImage(imgId: string) {
    const found = images.find(img => img.id === imgId);
    if (!found) return;

    selectedImage = found;

    renderImageGrid();
    renderInfoPanel();

    try {
        const res = await fetch(`/api/images/${imgId}`);
        if (res.ok) {
            const detailed = await res.json();
            if (selectedImage && selectedImage.id === imgId) {
                selectedImage = detailed;
                renderInfoPanel();

                // Also sync to editor if open
                if (editorDialogComp && editorDialogComp.open) {
                    editorDialogComp.selectedImage = detailed;
                    editorDialogComp.requestUpdate();
                }

                // Also update lightbox if open
                if (lightboxComp && lightboxComp.active) {
                    lightboxComp.image = detailed;
                    lightboxComp.requestUpdate();
                }
            }
        }
    } catch (err) {
        console.error("Failed fetching rich metadata for selected image:", err);
    }
}

function updateStatusLine() {
    if (statusBarComp) {
        statusBarComp.totalCount = totalCount;
        statusBarComp.librariesCount = libraries.length;
        statusBarComp.requestUpdate();
    }
}

// ==========================================
// API MUTATION OPERATIONS
// ==========================================

async function updateImageRating(imgId: string, ratingValue: number) {
    try {
        const res = await fetch(`/api/images/${imgId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ rating: ratingValue })
        });
        if (!res.ok) throw new Error("Could not index metadata rating stars");

        const updated = await res.json();

        // Update local lists
        images = images.map(img => img.id === imgId ? { ...img, rating: updated.rating } : img);
        if (selectedImage && selectedImage.id === imgId) {
            selectedImage = { ...selectedImage, rating: updated.rating };
        }

        renderImageGrid();
        renderInfoPanel();
        showToast(`Image rating logged as ${updated.rating} ★`, "success");
    } catch (err: any) {
        showToast(err.message || "Failed updating photography rating", "error");
    }
}

async function updateImageFlag(imgId: string, flagType: ImageFlag) {
    try {
        const res = await fetch(`/api/images/${imgId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ flag: flagType })
        });
        if (!res.ok) throw new Error("Could not log status flag metadata");

        const updated = await res.json();

        images = images.map(img => img.id === imgId ? { ...img, flag: updated.flag } : img);
        if (selectedImage && selectedImage.id === imgId) {
            selectedImage = { ...selectedImage, flag: updated.flag };
        }

        renderImageGrid();
        renderInfoPanel();
        showToast(`Status flag updated to '${updated.flag}'`, "success");
    } catch (err: any) {
        showToast(err.message || "Failed setting file state flags", "error");
    }
}

async function addTagToImage(imgId: string, value: string) {
    try {
        const res = await fetch(`/api/images/${imgId}/tags`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ tagName: value })
        });
        if (!res.ok) throw new Error("Failed assign tag relationship");

        const updated = await res.json();

        images = images.map(img => img.id === imgId ? { ...img, tags: updated.tags } : img);
        if (selectedImage && selectedImage.id === imgId) {
            selectedImage = { ...selectedImage, tags: updated.tags };
        }

        renderInfoPanel();
        fetchTagsCloud(); // refresh sidebar tags cloud
        showToast(`Tag #${value} added to file`, "success");
    } catch (err: any) {
        showToast(err.message || "Failed allocating tag", "error");
    }
}

async function removeTagFromImage(imgId: string, tagName: string) {
    try {
        const res = await fetch(`/api/api/images/${imgId}/tags/${encodeURIComponent(tagName)}`, {
            method: "DELETE"
        });

        const correctRes = res.ok ? res : await fetch(`/api/images/${imgId}/tags/${encodeURIComponent(tagName)}`, { method: "DELETE" });
        if (!correctRes.ok) throw new Error("Could not drop tag assignment link");

        const updated = await correctRes.json();

        images = images.map(img => img.id === imgId ? { ...img, tags: updated.tags } : img);
        if (selectedImage && selectedImage.id === imgId) {
            selectedImage = { ...selectedImage, tags: updated.tags };
        }

        renderInfoPanel();
        fetchTagsCloud();
        showToast(`Tag #${tagName} unassigned`, "info");
    } catch (err: any) {
        showToast(err.message || "Error dropping tag", "error");
    }
}

async function saveMemo(descriptionText: string) {
    if (!selectedImage) return;

    try {
        const res = await fetch(`/api/images/${selectedImage.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ description: descriptionText })
        });
        if (!res.ok) throw new Error("Failed to save description");

        const imgId = selectedImage.id;
        images = images.map(img => img.id === imgId ? { ...img, description: descriptionText } : img);
        selectedImage = { ...selectedImage, description: descriptionText };

        renderInfoPanel();
        showToast("Image memo saved successfully", "success");
    } catch (err: any) {
        showToast("Error updating image memo", "error");
    }
}

async function scanLibrary(libId: string) {
    if (imageGridComp) {
        imageGridComp.isScanning = true;
    }

    try {
        const res = await fetch(`/api/libraries/${libId}/scan`, { method: "POST" });
        if (!res.ok) {
            const errData = await res.json();
            throw new Error(errData.error || "Catalog folder scan error");
        }

        const result: ScanResult = await res.json();

        showToast(
            `扫描完成! Добавлено: ${result.added}, Удалено: ${result.removed}, Всего: ${result.total} (${(result.elapsedMs / 1000).toFixed(1)}s)`,
            "success"
        );

        fetchLibraries();
        fetchImages();
        fetchTagsCloud();
    } catch (err: any) {
        showToast(err.message || "Host scanner crashed during traverse", "error");
    } finally {
        if (imageGridComp) {
            imageGridComp.isScanning = false;
        }
    }
}

async function deleteLibrary(libId: string) {
    try {
        const res = await fetch(`/api/libraries/${libId}`, { method: "DELETE" });
        if (!res.ok) throw new Error("Unlink library execution failed");

        showToast("Library successfully detached from PhotoLab catalog", "info");

        if (activeLibraryId === libId) activeLibraryId = undefined;

        fetchLibraries();
        fetchImages();
        fetchTagsCloud();
    } catch (err: any) {
        showToast(err.message || "Failed detaching catalog", "error");
    }
}

async function renameLibrary(libId: string, newName: string) {
    if (!newName.trim()) return;
    try {
        const res = await fetch(`/api/libraries/${libId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name: newName.trim() })
        });
        if (!res.ok) throw new Error("Could not rename library");
        const updated = await res.json();
        libraries = libraries.map(lib => lib.id === libId ? { ...lib, name: updated.name } : lib);
        renderLibraries();
        renderImageGrid();
        showToast(`Library renamed to "${updated.name}"`, "success");
    } catch (err: any) {
        showToast(err.message || "Failed to rename library", "error");
    }
}

function reorderLibrary(libId: string, direction: "up" | "down") {
    const idx = libraries.findIndex(l => l.id === libId);
    if (idx === -1) return;

    const targetIdx = direction === "up" ? idx - 1 : idx + 1;
    if (targetIdx < 0 || targetIdx >= libraries.length) return;

    // Swap
    const temp = libraries[idx];
    libraries[idx] = libraries[targetIdx];
    libraries[targetIdx] = temp;

    saveLibraryOrder(libraries);
    renderLibraries();
}

async function createLibrary(name: string, diskPath: string) {
    try {
        const res = await fetch("/api/libraries", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name, path: diskPath })
        });

        if (!res.ok) {
            const errDetails = await res.json();
            throw new Error(errDetails.error || "Server validation rejected library path");
        }

        const created: Library = await res.json();
        showToast(`Created Library '${created.name}' successfully. Initiating scan!`, "success");

        closeLibraryModal();

        activeLibraryId = created.id;
        fetchLibraries();
        scanLibrary(created.id);
    } catch (err: any) {
        showToast(err.message || "Failed tracking catalog folder", "error");
    }
}

// === ALBUM MUTATION OPERATIONS ===

async function createAlbum(name: string) {
    try {
        const res = await fetch("/api/albums", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name })
        });
        if (!res.ok) throw new Error("Could not create album");
        const created = await res.json();
        showToast(`Album '${created.name}' created`, "success");
        fetchAlbums();
    } catch (err: any) {
        showToast(err.message || "Failed creating album", "error");
    }
}

async function renameAlbum(albumId: string, newName: string) {
    if (!newName.trim()) return;
    try {
        const res = await fetch(`/api/albums/${albumId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name: newName.trim() })
        });
        if (!res.ok) throw new Error("Could not rename album");
        const updated = await res.json();
        showToast(`Album renamed to "${updated.name}"`, "success");
        fetchAlbums();
    } catch (err: any) {
        showToast(err.message || "Failed to rename album", "error");
    }
}

async function deleteAlbum(albumId: string) {
    try {
        const res = await fetch(`/api/albums/${albumId}`, { method: "DELETE" });
        if (!res.ok) throw new Error("Failed deleting album");
        showToast("Album deleted", "info");
        if (activeAlbumId === albumId) activeAlbumId = undefined;
        fetchAlbums();
        fetchImages();
    } catch (err: any) {
        showToast(err.message || "Failed to delete album", "error");
    }
}

async function addImageToAlbum(albumId: string, imageId: string) {
    try {
        const res = await fetch(`/api/albums/${albumId}/images`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ imageId })
        });
        if (!res.ok) throw new Error("Could not add photo to album");
        showToast("Added to album", "success");
        fetchAlbums();
        // Refresh currently selected image metadata to reflect album assignment
        if (selectedImage && selectedImage.id === imageId) {
            const imgRes = await fetch(`/api/images/${imageId}`);
            if (imgRes.ok) {
                selectedImage = await imgRes.json();
                renderInfoPanel();
            }
        }
    } catch (err: any) {
        showToast(err.message || "Failed to add image to album", "error");
    }
}

async function addImagesToAlbumBatch(albumId: string, imageIds: string[]) {
    try {
        const res = await fetch(`/api/albums/${albumId}/images/batch`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ imageIds })
        });
        if (!res.ok) throw new Error("Could not add photos to album");
        const data = await res.json();
        showToast(`Успешно добавлено ${data.addedCount ?? imageIds.length} фото в альбом`, "success");

        if (imageGridComp) {
            imageGridComp.clearSelection();
        }

        fetchAlbums();

        // Refresh currently selected image metadata if it's one of the batch added photos
        if (selectedImage && imageIds.includes(selectedImage.id)) {
            const imgRes = await fetch(`/api/images/${selectedImage.id}`);
            if (imgRes.ok) {
                selectedImage = await imgRes.json();
                renderInfoPanel();
            }
        }
    } catch (err: any) {
        showToast(err.message || "Failed to add photos to album", "error");
    }
}

async function removeImageFromAlbum(albumId: string, imageId: string) {
    try {
        const res = await fetch(`/api/albums/${albumId}/images/${imageId}`, {
            method: "DELETE"
        });
        if (!res.ok) throw new Error("Could not remove photo from album");
        showToast("Removed from album", "info");
        fetchAlbums();
        // Refresh currently selected image metadata
        if (selectedImage && selectedImage.id === imageId) {
            const imgRes = await fetch(`/api/images/${imageId}`);
            if (imgRes.ok) {
                selectedImage = await imgRes.json();
                renderInfoPanel();
            }
        }
        // If currently viewing the album, reload images list
        if (activeAlbumId === albumId) {
            fetchImages();
        }
    } catch (err: any) {
        showToast(err.message || "Failed to remove image from album", "error");
    }
}

async function saveEditedImage(imageId: string, imageBase64: string) {
    try {
        showToast("Saving changes...", "info");
        const res = await fetch(`/api/images/${imageId}/edit`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ imageBase64 })
        });
        if (!res.ok) {
            const errDetails = await res.json();
            throw new Error(errDetails.error || "Failed to save edited image");
        }
        const updated = await res.json();
        showToast("Image edits saved successfully!", "success");

        // Update local states
        images = images.map(img => img.id === imageId ? updated : img);
        if (selectedImage && selectedImage.id === imageId) {
            selectedImage = updated;
        }
        renderImageGrid();
        renderInfoPanel();
        // Also sync to editor if open
        if (editorDialogComp && editorDialogComp.open) {
            editorDialogComp.selectedImage = updated;
            editorDialogComp.requestUpdate();
        }
    } catch (err: any) {
        showToast(err.message || "Failed to save edited image", "error");
    }
}

function openImageEditor() {
    if (!selectedImage) return;
    if (editorDialogComp) {
        editorDialogComp.selectedImage = selectedImage;
        editorDialogComp.open = true;
        editorDialogComp.requestUpdate();
    }
}

function closeImageEditor() {
    if (editorDialogComp) {
        editorDialogComp.open = false;
        editorDialogComp.requestUpdate();
    }
}

// ==========================================
// MODAL DIALOGS AND POPUP MANAGEMENT
// ==========================================
function openLibraryModal() {
    if (libraryDialogComp) {
        libraryDialogComp.active = true;
        libraryDialogComp.requestUpdate();
    }
}

function closeLibraryModal() {
    if (libraryDialogComp) {
        libraryDialogComp.active = false;
        libraryDialogComp.requestUpdate();
    }
}

// HIGH-RESOLUTION LIGHTBOX CAROUSEL
let currentLightboxIndex = -1;

function openLightbox() {
    if (!selectedImage) return;

    currentLightboxIndex = images.findIndex(img => img.id === selectedImage!.id);

    if (lightboxComp) {
        lightboxComp.image = selectedImage;
        lightboxComp.active = true;
        lightboxComp.requestUpdate();
    }
}

function closeLightbox() {
    if (lightboxComp) {
        lightboxComp.active = false;
        lightboxComp.requestUpdate();
    }
}

function navigateLightbox(direction: "prev" | "next") {
    if (images.length === 0 || currentLightboxIndex === -1) return;

    if (direction === "prev") {
        currentLightboxIndex = currentLightboxIndex > 0 ? currentLightboxIndex - 1 : images.length - 1;
    } else {
        currentLightboxIndex = currentLightboxIndex < images.length - 1 ? currentLightboxIndex + 1 : 0;
    }

    const nextImage = images[currentLightboxIndex];
    selectedImage = nextImage;
    selectImage(nextImage.id);

    if (lightboxComp) {
        lightboxComp.image = nextImage;
        lightboxComp.requestUpdate();
    }
}

function showToast(message: string, type: "success" | "error" | "info" = "info") {
    const container = document.getElementById("toast_container");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = `toast toast-${type}`;

    let iconName = "info";
    if (type === "success") iconName = "check_circle";
    else if (type === "error") iconName = "error";

    toast.innerHTML = `
    <span class="material-symbols-outlined" style="font-size:16px;">${iconName}</span>
    <span class="toast-message">${message}</span>
  `;

    container.appendChild(toast);

    setTimeout(() => {
        toast.style.animation = "toast-entrance 0.2s cubic-bezier(0.4, 0, 1, 1) reverse forwards";
        setTimeout(() => {
            if (toast.parentNode === container) {
                container.removeChild(toast);
            }
        }, 200);
    }, 4000);
}

// ==========================================
// CENTRAL EVENT BINDINGS
// ==========================================
function bindEvents() {
    // 1. Sidebar Component Callbacks
    if (sidebarComp) {
        sidebarComp.addEventListener("library-select", (e: any) => {
            activeLibraryId = e.detail.id;
            activeAlbumId = undefined;
            currentPage = 1;
            renderLibraries();
            renderAlbums();
            fetchImages();
        });

        sidebarComp.addEventListener("library-scan", (e: any) => {
            scanLibrary(e.detail.id);
        });

        sidebarComp.addEventListener("library-delete", (e: any) => {
            const lib = libraries.find(l => l.id === e.detail.id);
            if (lib && confirm(`Are you sure you want to untrack catalog '${lib.name}'?\n\nThis will remove file metadata references from local SQLite. Your original image files will remain entirely untouched.`)) {
                deleteLibrary(e.detail.id);
            }
        });

        sidebarComp.addEventListener("library-rename", (e: any) => {
            renameLibrary(e.detail.id, e.detail.name);
        });

        sidebarComp.addEventListener("library-reorder", (e: any) => {
            reorderLibrary(e.detail.id, e.detail.direction);
        });

        // Album callbacks
        sidebarComp.addEventListener("album-select", (e: any) => {
            activeAlbumId = e.detail.id;
            activeLibraryId = undefined;
            currentPage = 1;
            renderLibraries();
            renderAlbums();
            fetchImages();
        });

        sidebarComp.addEventListener("album-create", (e: any) => {
            createAlbum(e.detail.name);
        });

        sidebarComp.addEventListener("album-delete", (e: any) => {
            deleteAlbum(e.detail.id);
        });

        sidebarComp.addEventListener("album-rename", (e: any) => {
            renameAlbum(e.detail.id, e.detail.name);
        });
    }

    // 2. FilterPanel Component Callbacks
    if (filterPanelComp) {
        filterPanelComp.addEventListener("flag-toggle", (e: any) => {
            const type = e.detail.flag;
            const idx = activeFlags.indexOf(type);
            if (idx > -1) {
                activeFlags = activeFlags.filter(f => f !== type);
            } else {
                activeFlags = [...activeFlags, type];
            }
            currentPage = 1;
            renderTagsCloud();
            fetchImages();
        });

        filterPanelComp.addEventListener("rating-change", (e: any) => {
            ratingFilter = e.detail.rating;
            currentPage = 1;
            renderTagsCloud();
            fetchImages();
        });

        filterPanelComp.addEventListener("rating-clear", () => {
            ratingFilter = 0;
            currentPage = 1;
            renderTagsCloud();
            fetchImages();
        });

        filterPanelComp.addEventListener("tag-toggle", (e: any) => {
            const name = e.detail.tagName;
            const idx = activeTags.indexOf(name);
            if (idx > -1) {
                activeTags = activeTags.filter(t => t !== name);
            } else {
                activeTags = [...activeTags, name];
            }
            currentPage = 1;
            renderTagsCloud();
            fetchImages();
        });

        filterPanelComp.addEventListener("clear-all", () => {
            searchQuery = "";
            ratingFilter = 0;
            activeFlags = [];
            activeTags = [];
            currentPage = 1;

            if (appHeaderComp) {
                appHeaderComp.searchQuery = "";
                appHeaderComp.requestUpdate();
            }

            renderTagsCloud();
            fetchImages();
            showToast("All filters reset", "info");
        });
    }

    // 3. ImageGrid Component Callbacks
    if (imageGridComp) {
        imageGridComp.addEventListener("image-select", (e: any) => {
            selectImage(e.detail.id);
        });

        imageGridComp.addEventListener("image-double-click", (e: any) => {
            selectImage(e.detail.id);
            openLightbox();
        });

        imageGridComp.addEventListener("image-edit", (e: any) => {
            selectImage(e.detail.id);
            openImageEditor();
        });

        imageGridComp.addEventListener("sort-change", (e: any) => {
            sortBy = e.detail.sortBy;
            currentPage = 1;
            fetchImages();
        });

        imageGridComp.addEventListener("sort-order-toggle", () => {
            sortOrder = sortOrder === "desc" ? "asc" : "desc";
            currentPage = 1;
            fetchImages();
        });

        imageGridComp.addEventListener("limit-change", (e: any) => {
            limit = e.detail.limit;
            currentPage = 1;
            fetchImages();
        });

        imageGridComp.addEventListener("page-change", (e: any) => {
            currentPage = e.detail.page;
            fetchImages();
        });

        imageGridComp.addEventListener("virtual-mode-toggle", (e: any) => {
            isVirtualMode = e.detail.enabled;
            currentPage = 1;
            fetchImages();
        });

        imageGridComp.addEventListener("load-more", () => {
            if (currentPage < totalPages && !isFetching) {
                currentPage += 1;
                fetchImages(true);
            }
        });

        imageGridComp.addEventListener("batch-add-to-album", async (e: any) => {
            const { albumId, imageIds } = e.detail;
            await addImagesToAlbumBatch(albumId, imageIds);
        });
    }

    // 4. InfoPanel Component Callbacks
    if (infoPanelComp) {
        infoPanelComp.addEventListener("rating-change", (e: any) => {
            if (selectedImage) updateImageRating(selectedImage.id, e.detail.rating);
        });

        infoPanelComp.addEventListener("flag-change", (e: any) => {
            if (selectedImage) updateImageFlag(selectedImage.id, e.detail.flag);
        });

        infoPanelComp.addEventListener("tag-add", (e: any) => {
            if (selectedImage) addTagToImage(selectedImage.id, e.detail.tagName);
        });

        infoPanelComp.addEventListener("tag-remove", (e: any) => {
            if (selectedImage) removeTagFromImage(selectedImage.id, e.detail.tagName);
        });

        infoPanelComp.addEventListener("save-memo", (e: any) => {
            if (selectedImage) saveMemo(e.detail.memo);
        });

        infoPanelComp.addEventListener("fullscreen", () => {
            openLightbox();
        });

        // Album associations
        infoPanelComp.addEventListener("album-add", (e: any) => {
            if (selectedImage) addImageToAlbum(e.detail.albumId, selectedImage.id);
        });

        infoPanelComp.addEventListener("album-remove", (e: any) => {
            if (selectedImage) removeImageFromAlbum(e.detail.albumId, selectedImage.id);
        });

        // Image edits
        infoPanelComp.addEventListener("open-image-editor", () => {
            openImageEditor();
        });
    }

    // 4b. ImageEditorDialog Callbacks
    if (editorDialogComp) {
        editorDialogComp.addEventListener("editor-close", () => {
            closeImageEditor();
        });

        editorDialogComp.addEventListener("editor-save", (e: any) => {
            if (selectedImage) {
                saveEditedImage(selectedImage.id, e.detail.imageBase64);
            }
        });
    }

    // 5. Application Header Callbacks
    if (appHeaderComp) {
        appHeaderComp.addEventListener("add-library-click", () => {
            openLibraryModal();
        });

        appHeaderComp.addEventListener("search-change", (e: any) => {
            searchQuery = e.detail.query;
            currentPage = 1;
            fetchImages();
        });
    }

    // 6. Create Library Dialog Callbacks
    if (libraryDialogComp) {
        libraryDialogComp.addEventListener("close", () => {
            closeLibraryModal();
        });

        libraryDialogComp.addEventListener("submit", (e: any) => {
            const { name, path } = e.detail;
            createLibrary(name, path);
            closeLibraryModal();
        });
    }

    // 7. Lightbox Callbacks
    if (lightboxComp) {
        lightboxComp.addEventListener("close", () => {
            closeLightbox();
        });

        lightboxComp.addEventListener("prev", () => {
            navigateLightbox("prev");
        });

        lightboxComp.addEventListener("next", () => {
            navigateLightbox("next");
        });
    }
}
