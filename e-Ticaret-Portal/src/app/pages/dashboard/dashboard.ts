import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../services/auth.service';
import { DocumentService } from '../../services/document.service';
import { User } from '../../models/user.model';
import { Document, DocumentType, DocumentStatus } from '../../models/document.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    MatTableModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatDividerModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  currentUser: User | null = null;
  recentDocuments: Document[] = [];
  isLoadingDocuments = true;
  
  // Statistics
  totalDocuments = 0;
  pendingDocuments = 0;
  approvedDocuments = 0;
  rejectedDocuments = 0;
  
  displayedColumns: string[] = ['title', 'type', 'status', 'createdAt', 'actions'];

  constructor(
    private authService: AuthService,
    private documentService: DocumentService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    // Kullanıcı bilgilerini al
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });

    // Eğer giriş yapılmamışsa login sayfasına yönlendir
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.loadDashboardData();
  }

  private loadDashboardData(): void {
    this.loadRecentDocuments();
    this.loadStatistics();
  }

  private loadRecentDocuments(): void {
    this.isLoadingDocuments = true;
    console.log('DEBUG: Loading recent documents...');
    
    this.documentService.getRecentDocuments(10).subscribe({
      next: (response) => {
        console.log('DEBUG: Recent documents response:', response);
        if (response.success) {
          this.recentDocuments = response.data;
          console.log('DEBUG: Recent documents loaded:', this.recentDocuments.length);
        } else {
          console.error('DEBUG: API returned error:', response.message);
        }
        this.isLoadingDocuments = false;
      },
      error: (error) => {
        console.error('DEBUG: Error loading recent documents:', error);
        
        // Fallback: getAllDocuments kullan ve son 10 belgeyi al
        console.log('DEBUG: Falling back to getAllDocuments...');
        this.documentService.getAllDocuments().subscribe({
          next: (response) => {
            if (response.success) {
              // Son 10 belgeyi al (CreatedAt'e göre sıralı olarak)
              this.recentDocuments = response.data
                .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
                .slice(0, 10);
              console.log('DEBUG: Fallback - loaded documents:', this.recentDocuments.length);
            }
            this.isLoadingDocuments = false;
          },
          error: (fallbackError) => {
            console.error('DEBUG: Fallback also failed:', fallbackError);
            this.isLoadingDocuments = false;
          }
        });
      }
    });
  }

  private loadStatistics(): void {
    console.log('DEBUG: Loading statistics...');
    // Tüm belgeleri al ve istatistikleri hesapla
    this.documentService.getAllDocuments().subscribe({
      next: (response) => {
        console.log('DEBUG: All documents response:', response);
        if (response.success) {
          const documents = response.data;
          
          // Async olarak güncellemeleri yap
          setTimeout(() => {
            this.totalDocuments = documents.length;
            this.pendingDocuments = documents.filter(d => d.status === DocumentStatus.Pending).length;
            this.approvedDocuments = documents.filter(d => d.status === DocumentStatus.Approved).length;
            this.rejectedDocuments = documents.filter(d => d.status === DocumentStatus.Rejected).length;
            
            console.log('DEBUG: Statistics calculated:', {
              total: this.totalDocuments,
              pending: this.pendingDocuments,
              approved: this.approvedDocuments,
              rejected: this.rejectedDocuments
            });
            
            this.cdr.detectChanges();
          }, 0);
        } else {
          console.error('DEBUG: Statistics API returned error:', response.message);
        }
      },
      error: (error) => {
        console.error('DEBUG: Error loading statistics:', error);
      }
    });
  }

  getDocumentTypeText(type: DocumentType): string {
    switch (type) {
      case DocumentType.Karar: return 'Karar';
      case DocumentType.Duyuru: return 'Duyuru';
      case DocumentType.Genelge: return 'Genelge';
      case DocumentType.Rapor: return 'Rapor';
      case DocumentType.Dilekce: return 'Dilekçe';
      case DocumentType.Evrak: return 'Evrak';
      default: return 'Diğer';
    }
  }

  getDocumentStatusText(status: DocumentStatus): string {
    switch (status) {
      case DocumentStatus.Draft: return 'Taslak';
      case DocumentStatus.Pending: return 'Beklemede';
      case DocumentStatus.Approved: return 'Onaylandı';
      case DocumentStatus.Rejected: return 'Reddedildi';
      case DocumentStatus.Published: return 'Yayınlandı';
      case DocumentStatus.Archived: return 'Arşivlendi';
      default: return 'Bilinmiyor';
    }
  }

  createDocument(): void {
    this.router.navigate(['/documents/create']);
  }

  viewDocument(document: Document): void {
    this.router.navigate(['/documents', document.id]);
  }

  editDocument(document: Document): void {
    this.router.navigate(['/documents', document.id, 'edit']);
  }

  downloadDocument(document: Document): void {
    this.documentService.downloadDocument(document.id).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const link = window.document.createElement('a');
        link.href = url;
        link.download = document.fileName || `document-${document.id}`;
        link.click();
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        console.error('Error downloading document:', error);
      }
    });
  }

  deleteDocument(document: Document): void {
    if (confirm(`"${document.title}" belgesini silmek istediğinizden emin misiniz?`)) {
      this.documentService.deleteDocument(document.id).subscribe({
        next: (response) => {
          if (response.success) {
            this.loadDashboardData(); // Verileri yeniden yükle
          }
        },
        error: (error) => {
          console.error('Error deleting document:', error);
        }
      });
    }
  }

  viewProfile(): void {
    this.router.navigate(['/profile']);
  }

  changePassword(): void {
    this.router.navigate(['/change-password']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
