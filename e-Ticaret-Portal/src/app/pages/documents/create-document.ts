import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';

import { DocumentService } from '../../services/document.service';
import { DocumentType, DocumentCreateDto } from '../../models/document.model';

@Component({
  selector: 'app-create-document',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatToolbarModule
  ],
  template: `
    <mat-toolbar color="primary">
      <button mat-icon-button (click)="goBack()">
        <mat-icon>arrow_back</mat-icon>
      </button>
      <span>Yeni Belge Oluştur</span>
    </mat-toolbar>

    <div class="create-document-container">
      <mat-card>
        <mat-card-header>
          <mat-card-title>Belge Bilgileri</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="documentForm" (ngSubmit)="onSubmit()">
            <div class="form-row">
              <mat-form-field appearance="outline" class="full-width">
                <mat-label>Belge Başlığı</mat-label>
                <input matInput formControlName="title" placeholder="Belge başlığını giriniz">
                <mat-error *ngIf="documentForm.get('title')?.hasError('required')">
                  Belge başlığı zorunludur
                </mat-error>
              </mat-form-field>
            </div>

            <div class="form-row">
              <mat-form-field appearance="outline" class="full-width">
                <mat-label>Belge Türü</mat-label>
                <mat-select formControlName="documentType">
                  <mat-option [value]="DocumentType.Karar">Karar</mat-option>
                  <mat-option [value]="DocumentType.Duyuru">Duyuru</mat-option>
                  <mat-option [value]="DocumentType.Genelge">Genelge</mat-option>
                  <mat-option [value]="DocumentType.Rapor">Rapor</mat-option>
                  <mat-option [value]="DocumentType.Dilekce">Dilekçe</mat-option>
                  <mat-option [value]="DocumentType.Evrak">Evrak</mat-option>
                </mat-select>
                <mat-error *ngIf="documentForm.get('documentType')?.hasError('required')">
                  Belge türü seçiniz
                </mat-error>
              </mat-form-field>
            </div>

            <div class="form-row">
              <mat-form-field appearance="outline" class="full-width">
                <mat-label>Açıklama</mat-label>
                <textarea matInput formControlName="description" rows="4" placeholder="Belge açıklaması (isteğe bağlı)"></textarea>
              </mat-form-field>
            </div>

            <div class="form-row">
              <mat-form-field appearance="outline" class="full-width">
                <mat-label>Dosya Seç</mat-label>
                <input matInput readonly [value]="selectedFileName" (click)="fileInput.click()">
                <mat-icon matSuffix>attach_file</mat-icon>
              </mat-form-field>
              <input #fileInput type="file" hidden (change)="onFileSelected($event)" accept=".pdf,.doc,.docx,.txt,.jpg,.png">
            </div>

            <div class="form-actions">
              <button mat-button type="button" (click)="goBack()">İptal</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="documentForm.invalid || isSubmitting">
                <mat-spinner *ngIf="isSubmitting" diameter="20" class="button-spinner"></mat-spinner>
                <mat-icon *ngIf="!isSubmitting">save</mat-icon>
                {{ isSubmitting ? 'Kaydediliyor...' : 'Kaydet' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .create-document-container {
      padding: 20px;
      max-width: 800px;
      margin: 0 auto;
    }

    .form-row {
      margin-bottom: 20px;
    }

    .full-width {
      width: 100%;
    }

    .form-actions {
      display: flex;
      justify-content: flex-end;
      gap: 12px;
      margin-top: 24px;
    }

    .button-spinner {
      margin-right: 8px;
    }
  `]
})
export class CreateDocument {
  documentForm: FormGroup;
  isSubmitting = false;
  selectedFile: File | null = null;
  selectedFileName = '';
  DocumentType = DocumentType;

  constructor(
    private fb: FormBuilder,
    private documentService: DocumentService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.documentForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      documentType: ['', Validators.required],
      description: ['']
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.selectedFileName = file.name;
    }
  }

  onSubmit(): void {
    if (this.documentForm.valid) {
      this.isSubmitting = true;

      const createDto: DocumentCreateDto = {
        title: this.documentForm.get('title')?.value,
        documentType: this.documentForm.get('documentType')?.value,
        description: this.documentForm.get('description')?.value,
        file: this.selectedFile || undefined
      };

      this.documentService.createDocument(createDto).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          if (response.success) {
            this.snackBar.open('Belge başarıyla oluşturuldu', 'Tamam', {
              duration: 3000
            });
            this.router.navigate(['/dashboard']);
          } else {
            this.snackBar.open('Belge oluşturulamadı: ' + response.message, 'Tamam', {
              duration: 5000
            });
          }
        },
        error: (error) => {
          this.isSubmitting = false;
          console.error('Error creating document:', error);
          this.snackBar.open('Belge oluşturulurken hata oluştu', 'Tamam', {
            duration: 5000
          });
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
