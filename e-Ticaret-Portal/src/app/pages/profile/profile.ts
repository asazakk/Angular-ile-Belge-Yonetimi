import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';

import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatDividerModule
  ],
  template: `
    <mat-toolbar color="primary">
      <button mat-icon-button (click)="goBack()">
        <mat-icon>arrow_back</mat-icon>
      </button>
      <span>Profil Bilgileri</span>
    </mat-toolbar>

    <div class="profile-container">
      <div class="profile-grid">
        <!-- Profil Bilgileri Kartı -->
        <mat-card class="profile-card">
          <mat-card-header>
            <div mat-card-avatar class="profile-avatar">
              <mat-icon>account_circle</mat-icon>
            </div>
            <mat-card-title>{{ currentUser?.firstName }} {{ currentUser?.lastName }}</mat-card-title>
            <mat-card-subtitle>{{ currentUser?.position }} - {{ currentUser?.department }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <div class="profile-info">
              <div class="info-row">
                <mat-icon>person</mat-icon>
                <span>{{ currentUser?.username }}</span>
              </div>
              <div class="info-row">
                <mat-icon>email</mat-icon>
                <span>{{ currentUser?.email }}</span>
              </div>
              <div class="info-row">
                <mat-icon>business</mat-icon>
                <span>{{ currentUser?.department }}</span>
              </div>
              <div class="info-row">
                <mat-icon>work</mat-icon>
                <span>{{ currentUser?.position }}</span>
              </div>
              <div class="info-row" *ngIf="currentUser?.lastLoginDate">
                <mat-icon>login</mat-icon>
                <span>Son giriş: {{ currentUser?.lastLoginDate | date:'dd/MM/yyyy HH:mm' }}</span>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <!-- Şifre Değiştirme Kartı -->
        <mat-card class="password-card">
          <mat-card-header>
            <mat-card-title>Şifre Değiştir</mat-card-title>
            <mat-card-subtitle>Güvenliğiniz için düzenli olarak şifrenizi değiştirin</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <form [formGroup]="passwordForm" (ngSubmit)="onPasswordChange()">
              <div class="form-row">
                <mat-form-field appearance="outline" class="full-width">
                  <mat-label>Mevcut Şifre</mat-label>
                  <input matInput type="password" formControlName="currentPassword">
                  <mat-error *ngIf="passwordForm.get('currentPassword')?.hasError('required')">
                    Mevcut şifre gerekli
                  </mat-error>
                </mat-form-field>
              </div>

              <div class="form-row">
                <mat-form-field appearance="outline" class="full-width">
                  <mat-label>Yeni Şifre</mat-label>
                  <input matInput type="password" formControlName="newPassword">
                  <mat-error *ngIf="passwordForm.get('newPassword')?.hasError('required')">
                    Yeni şifre gerekli
                  </mat-error>
                  <mat-error *ngIf="passwordForm.get('newPassword')?.hasError('minlength')">
                    Şifre en az 6 karakter olmalı
                  </mat-error>
                </mat-form-field>
              </div>

              <div class="form-row">
                <mat-form-field appearance="outline" class="full-width">
                  <mat-label>Yeni Şifre (Tekrar)</mat-label>
                  <input matInput type="password" formControlName="confirmPassword">
                  <mat-error *ngIf="passwordForm.get('confirmPassword')?.hasError('required')">
                    Şifre tekrarı gerekli
                  </mat-error>
                  <mat-error *ngIf="passwordForm.get('confirmPassword')?.hasError('mismatch')">
                    Şifreler eşleşmiyor
                  </mat-error>
                </mat-form-field>
              </div>

              <div class="form-actions">
                <button mat-raised-button color="primary" type="submit" [disabled]="passwordForm.invalid || isChangingPassword">
                  <mat-spinner *ngIf="isChangingPassword" diameter="20" class="button-spinner"></mat-spinner>
                  <mat-icon *ngIf="!isChangingPassword">lock</mat-icon>
                  {{ isChangingPassword ? 'Değiştiriliyor...' : 'Şifreyi Değiştir' }}
                </button>
              </div>
            </form>
          </mat-card-content>
        </mat-card>

        <!-- Hesap İstatistikleri -->
        <mat-card class="stats-card">
          <mat-card-header>
            <mat-card-title>Hesap İstatistikleri</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="stats-grid">
              <div class="stat-item">
                <mat-icon>description</mat-icon>
                <div>
                  <span class="stat-number">0</span>
                  <span class="stat-label">Oluşturduğum Belgeler</span>
                </div>
              </div>
              <div class="stat-item">
                <mat-icon>edit</mat-icon>
                <div>
                  <span class="stat-number">0</span>
                  <span class="stat-label">Düzenlediğim Belgeler</span>
                </div>
              </div>
              <div class="stat-item">
                <mat-icon>check_circle</mat-icon>
                <div>
                  <span class="stat-number">0</span>
                  <span class="stat-label">Onayladığım Belgeler</span>
                </div>
              </div>
            </div>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .profile-container {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
    }

    .profile-grid {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 20px;
    }

    @media (max-width: 768px) {
      .profile-grid {
        grid-template-columns: 1fr;
      }
    }

    .profile-card {
      grid-row: span 2;
    }

    .profile-avatar {
      background: #3f51b5;
      color: white;
      display: flex;
      align-items: center;
      justify-content: center;
    }

    .profile-avatar mat-icon {
      font-size: 40px;
      width: 40px;
      height: 40px;
    }

    .profile-info {
      margin-top: 20px;
    }

    .info-row {
      display: flex;
      align-items: center;
      margin-bottom: 12px;
    }

    .info-row mat-icon {
      margin-right: 12px;
      color: #666;
      width: 24px;
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
      margin-top: 20px;
    }

    .button-spinner {
      margin-right: 8px;
    }

    .stats-grid {
      display: flex;
      flex-direction: column;
      gap: 16px;
    }

    .stat-item {
      display: flex;
      align-items: center;
      gap: 12px;
    }

    .stat-item mat-icon {
      color: #3f51b5;
    }

    .stat-number {
      font-size: 24px;
      font-weight: bold;
      display: block;
    }

    .stat-label {
      color: #666;
      font-size: 14px;
    }
  `]
})
export class Profile implements OnInit {
  currentUser: User | null = null;
  passwordForm: FormGroup;
  isChangingPassword = false;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.passwordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });

    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
  }

  passwordMatchValidator(form: FormGroup) {
    const newPassword = form.get('newPassword');
    const confirmPassword = form.get('confirmPassword');
    
    if (newPassword && confirmPassword && newPassword.value !== confirmPassword.value) {
      confirmPassword.setErrors({ mismatch: true });
    } else if (confirmPassword?.errors?.['mismatch']) {
      delete confirmPassword.errors['mismatch'];
      if (Object.keys(confirmPassword.errors).length === 0) {
        confirmPassword.setErrors(null);
      }
    }
    return null;
  }

  onPasswordChange(): void {
    if (this.passwordForm.valid) {
      this.isChangingPassword = true;

      // Şifre değiştirme API çağrısı burada yapılacak
      // Şimdilik sadece mesaj gösteriyoruz
      setTimeout(() => {
        this.isChangingPassword = false;
        this.snackBar.open('Şifre değiştirme özelliği yakında aktif olacak', 'Tamam', {
          duration: 3000
        });
        this.passwordForm.reset();
      }, 2000);
    }
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
