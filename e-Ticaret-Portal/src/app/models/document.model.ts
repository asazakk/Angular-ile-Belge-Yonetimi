export enum DocumentType {
  Karar = 1,
  Duyuru = 2,
  Genelge = 3,
  Rapor = 4,
  Dilekce = 5,
  Evrak = 6,
  Diğer = 99
}

export enum DocumentStatus {
  Draft = 1,      // Taslak
  Pending = 2,    // Beklemede
  Approved = 3,   // Onaylandı
  Rejected = 4,   // Reddedildi
  Published = 5,  // Yayınlandı
  Archived = 6    // Arşivlendi
}

export enum DocumentActionType {
  Created = 0,
  Updated = 1,
  StatusChanged = 2,
  Approved = 3,
  Rejected = 4,
  Deleted = 5,
  Restored = 6,
  Archived = 7,
  Downloaded = 8,
  Viewed = 9
}

export interface Document {
  id: number;
  title: string;
  description?: string;
  fileName?: string;
  filePath?: string;
  fileSize?: number;
  documentType: DocumentType;
  status: DocumentStatus;
  createdByUserId: number;
  updatedByUserId?: number;
  isDeleted: boolean;
  createdAt: Date;
  updatedAt: Date;
  createdByUser?: any;
  updatedByUser?: any;
  documentActions?: DocumentAction[];
}

export interface DocumentAction {
  id: number;
  documentId: number;
  userId: number;
  actionType: DocumentActionType;
  description?: string;
  actionDate: Date;
  user?: any;
}

export interface DocumentCreateDto {
  title: string;
  description?: string;
  documentType: DocumentType;
  file?: File;
}

export interface DocumentUpdateDto {
  title: string;
  description?: string;
  documentType: DocumentType;
  status: DocumentStatus;
  file?: File;
}
