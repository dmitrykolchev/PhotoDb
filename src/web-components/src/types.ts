export interface Library {
  id: string;
  name: string;
  path: string;
  dateCreated: string;
  lastScanDate: string | null;
  imageCount?: number;
}

export type ImageFlag = "Favorite" | "Recycle" | "Process" | "None";

export interface Image {
  id: string;
  libraryId: string;
  name: string;
  filePath: string;
  created: string; // File birthtime / mtime
  indexedDate: string;
  rating: number; // 0 (unrated) to 5 stars
  flag: ImageFlag;
  hash: string | null; // For deduplication
  size: number; // in bytes
  width?: number | null;
  height?: number | null;
  tags: string[]; // Aggregated array of tag names for convenience
  cameraModel?: string | null;
  lensModel?: string | null;
  focalLength?: string | null;
  aperture?: string | null;
  isoSpeed?: string | null;
  exposureTime?: string | null;
  shootingDate?: string | null;
  description?: string | null;
  albums?: Album[];
}

export interface Tag {
  id: string;
  name: string;
  imageCount?: number;
}

export interface ImageTag {
  imageId: string;
  tagId: string;
}

export interface DatabaseState {
  libraries: Library[];
  images: Image[];
  tags: Tag[];
  imageTags: ImageTag[];
  albums?: Album[];
  albumImages?: AlbumImage[];
}

export interface Album {
  id: string;
  name: string;
  dateCreated: string;
  imageCount?: number;
}

export interface AlbumImage {
  albumId: string;
  imageId: string;
}

export interface ScanResult {
  added: number;
  removed: number;
  total: number;
  elapsedMs: number;
}

export interface GalleryQuery {
  libraryId?: string;
  search?: string;
  minRating?: number;
  flags?: ImageFlag[];
  tags?: string[];
  page?: number;
  limit?: number;
  sortBy?: "dateCreated" | "fileName" | "rating" | "size";
  sortOrder?: "asc" | "desc";
}
