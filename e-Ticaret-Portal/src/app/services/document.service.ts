import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Document, DocumentCreateDto, DocumentUpdateDto, DocumentStatus, DocumentType } from '../models/document.model';
import { ApiResponse, PagedResult } from '../models/common.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private readonly baseUrl = 'http://localhost:5235/api/documents'; // API URL'ini buraya yazın

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  getAllDocuments(): Observable<ApiResponse<Document[]>> {
    console.log('DEBUG: Calling getAllDocuments API');
    return this.http.get<ApiResponse<Document[]>>(this.baseUrl, {
      headers: this.getAuthHeaders()
    });
  }

  getDocumentById(id: number): Observable<ApiResponse<Document>> {
    return this.http.get<ApiResponse<Document>>(`${this.baseUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
  }

  createDocument(documentCreateDto: DocumentCreateDto): Observable<ApiResponse<Document>> {
    console.log('DEBUG: Creating document:', documentCreateDto);
    const formData = new FormData();
    formData.append('title', documentCreateDto.title);
    formData.append('documentType', documentCreateDto.documentType.toString());
    
    if (documentCreateDto.description) {
      formData.append('description', documentCreateDto.description);
    }
    
    if (documentCreateDto.file) {
      formData.append('file', documentCreateDto.file);
    }

    return this.http.post<ApiResponse<Document>>(this.baseUrl, formData, {
      headers: this.getAuthHeadersForFile()
    });
  }

  updateDocument(id: number, documentUpdateDto: DocumentUpdateDto): Observable<ApiResponse<Document>> {
    const formData = new FormData();
    formData.append('title', documentUpdateDto.title);
    formData.append('documentType', documentUpdateDto.documentType.toString());
    formData.append('status', documentUpdateDto.status.toString());
    
    if (documentUpdateDto.description) {
      formData.append('description', documentUpdateDto.description);
    }
    
    if (documentUpdateDto.file) {
      formData.append('file', documentUpdateDto.file);
    }

    return this.http.put<ApiResponse<Document>>(`${this.baseUrl}/${id}`, formData, {
      headers: this.getAuthHeadersForFile()
    });
  }

  deleteDocument(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.baseUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
  }

  getDocumentsByStatus(status: DocumentStatus): Observable<ApiResponse<Document[]>> {
    return this.http.get<ApiResponse<Document[]>>(`${this.baseUrl}/status/${status}`, {
      headers: this.getAuthHeaders()
    });
  }

  getDocumentsByType(documentType: DocumentType): Observable<ApiResponse<Document[]>> {
    return this.http.get<ApiResponse<Document[]>>(`${this.baseUrl}/type/${documentType}`, {
      headers: this.getAuthHeaders()
    });
  }

  getMyDocuments(): Observable<ApiResponse<Document[]>> {
    return this.http.get<ApiResponse<Document[]>>(`${this.baseUrl}/my-documents`, {
      headers: this.getAuthHeaders()
    });
  }

  getRecentDocuments(count: number = 10): Observable<ApiResponse<Document[]>> {
    console.log('DEBUG: Calling getRecentDocuments API with count:', count);
    return this.http.get<ApiResponse<Document[]>>(`${this.baseUrl}/recent?count=${count}`, {
      headers: this.getAuthHeaders()
    });
  }

  searchDocuments(searchTerm: string): Observable<ApiResponse<Document[]>> {
    return this.http.get<ApiResponse<Document[]>>(`${this.baseUrl}/search?term=${encodeURIComponent(searchTerm)}`, {
      headers: this.getAuthHeaders()
    });
  }

  downloadDocument(id: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/${id}/download`, {
      headers: this.getAuthHeaders(),
      responseType: 'blob'
    });
  }

  approveDocument(id: number, comment?: string): Observable<ApiResponse<boolean>> {
    const body = comment ? { comment } : {};
    return this.http.post<ApiResponse<boolean>>(`${this.baseUrl}/${id}/approve`, body, {
      headers: this.getAuthHeaders()
    });
  }

  rejectDocument(id: number, comment?: string): Observable<ApiResponse<boolean>> {
    const body = comment ? { comment } : {};
    return this.http.post<ApiResponse<boolean>>(`${this.baseUrl}/${id}/reject`, body, {
      headers: this.getAuthHeaders()
    });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  private getAuthHeadersForFile(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
      // Content-Type'ı FormData için otomatik olarak ayarlanır
    });
  }
}
