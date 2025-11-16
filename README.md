# 📋 e-Ticaret Yönetim Sistemi

Modern, güvenli ve ölçeklenebilir bir belge yönetim sistemi. .NET 9 Clean Architecture backend ve Angular 20 frontend teknolojileri kullanılarak geliştirilmiştir.

## 🚀 Özellikler

### 📄 Belge Yönetimi
- ✅ Belge oluşturma, düzenleme, silme
- ✅ Çoklu dosya formatı desteği (PDF, DOC, DOCX, TXT, JPG, PNG)
- ✅ Belge durum yönetimi (Taslak, Beklemede, Onaylandı, Reddedildi, Yayınlandı, Arşivlendi)
- ✅ Gelişmiş arama ve filtreleme
- ✅ Dosya indirme işlemleri

### 🔐 Güvenlik & Kimlik Doğrulama
- ✅ JWT Token tabanlı kimlik doğrulama
- ✅ BCrypt şifre hashleme
- ✅ Rol tabanlı yetkilendirme
- ✅ Güvenli API endpoints

### 👥 Kullanıcı Yönetimi
- ✅ Kullanıcı profil yönetimi
- ✅ Şifre değiştirme
- ✅ Son giriş takibi
- ✅ Departman ve pozisyon yönetimi

### 📊 Dashboard & Raporlama
- ✅ İnteraktif dashboard
- ✅ Belge istatistikleri
- ✅ Son belgeler görüntüleme
- ✅ Kullanıcı aktivite takibi

## 🏗️ Mimari

### 📋 Clean Architecture Pattern

```
├── 🌐 Presentation Layer (Angular Frontend)
│   ├── Pages (Login, Dashboard, Document Management, Profile)
│   ├── Services (HTTP Client, Authentication, Document)
│   └── Models (TypeScript Interfaces)
│
├── 🎯 API Layer (.NET WebAPI)
│   ├── Controllers (Auth, Documents)
│   ├── Middleware (Authentication, CORS)
│   └── Configuration (JWT, Swagger)
│
├── 🧠 Application Layer
│   ├── Services (AuthService, DocumentService)
│   ├── DTOs (Data Transfer Objects)
│   └── Interfaces (Service Contracts)
│
├── 🏢 Domain Layer
│   ├── Entities (User, Document, DocumentAction)
│   ├── Enums (DocumentType, DocumentStatus, ActionType)
│   └── Interfaces (Repository Contracts)
│
└── 🔧 Infrastructure Layer
    ├── Data (EF Core DbContext)
    ├── Repositories (Generic Repository, Unit of Work)
    └── Services (FileService)
```

### 🛠️ Teknoloji Stack

#### Backend (.NET 9)
- **Framework:** ASP.NET Core Web API
- **Database:** SQL Server LocalDB
- **ORM:** Entity Framework Core 9.0
- **Authentication:** JWT Bearer
- **Password Security:** BCrypt.Net
- **Documentation:** Swagger/OpenAPI
- **Architecture:** Clean Architecture + Repository Pattern

#### Frontend (Angular 20)
- **Framework:** Angular 20 (Latest)
- **UI Library:** Angular Material
- **Rendering:** Server-Side Rendering (SSR)
- **HTTP Client:** Angular HttpClient
- **Routing:** Angular Router
- **Styling:** SCSS

## 🚀 Kurulum ve Çalıştırma

### ⚡ Hızlı Başlangıç

#### 1. Repository'yi Klonlayın
```bash
git clone https://github.com/asazakk/Angular-ile-Belge-Y-netimi.git
cd Angular-ile-Belge-Y-netimi
```

#### 2. Backend'i Çalıştırın (.NET API)
```bash
cd src/e-Ticaret.WebAPI
dotnet restore
dotnet run
```
🌐 API: http://localhost:5235 (Swagger: http://localhost:5235/swagger)

#### 3. Frontend'i Çalıştırın (Angular)
```bash
cd e-Ticaret-Portal
npm install
npm start
```
🌐 Web App: http://localhost:4200

### 📋 Gereksinimler

#### Backend
- .NET 9 SDK
- SQL Server LocalDB (otomatik oluşturulur)
- Visual Studio Code veya Visual Studio

#### Frontend
- Node.js 18+ 
- npm 9+
- Angular CLI 20

## 🔧 Konfigürasyon

### Backend Configuration (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DanistayBelgeYonetimiDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Secret": "MySecretKeyForDanistayApp2025!@#$%^&*()",
    "Issuer": "DanistayApp",
    "Audience": "DanistayAppUsers",
    "ExpirationHours": 24
  }
}
```

### Frontend Configuration
- API Base URL: `http://localhost:5235/api`
- Auto-generated environment files
- Material UI theming

## 👤 Test Kullanıcıları

Sistem test kullanıcıları ile birlikte gelir:

| Kullanıcı Adı | Şifre | Rol | Departman |
|---|---|---|---|
| `admin` | `admin123` | Yönetici | Bilgi İşlem |
| `user1` | `user123` | Kullanıcı | Hukuk İşleri |

## 🎯 API Endpoints

### 🔐 Authentication
```
POST /api/auth/login          # Kullanıcı girişi
GET  /api/auth/me            # Mevcut kullanıcı bilgileri
```

### 📄 Documents
```
GET    /api/documents                    # Tüm belgeler
GET    /api/documents/{id}              # Belge detayı
GET    /api/documents/recent?count=10   # Son belgeler
GET    /api/documents/my-documents      # Kullanıcının belgeleri
GET    /api/documents/search?term=...   # Belge arama
POST   /api/documents                   # Yeni belge oluştur
PUT    /api/documents/{id}              # Belge güncelle
DELETE /api/documents/{id}              # Belge sil
GET    /api/documents/{id}/download     # Belge indir
```

## 📂 Proje Yapısı

```
📦 Angular-ile-Belge-Yönetimi/
├── 📁 src/                              # Backend (.NET)
│   ├── 📁 Danistay.Domain/              # Domain Layer
│   │   ├── 📁 Entities/                 # Varlıklar (User, Document, DocumentAction)
│   │   ├── 📁 Enums/                    # Enumerations
│   │   └── 📁 Interfaces/               # Repository interfaces
│   │
│   ├── 📁 e-Ticaret.Application/         # Application Layer
│   │   ├── 📁 Services/                 # Business logic
│   │   ├── 📁 DTOs/                     # Data Transfer Objects
│   │   └── 📁 Interfaces/               # Service interfaces
│   │
│   ├── 📁 e-Ticaret.Infrastructure/      # Infrastructure Layer
│   │   ├── 📁 Data/                     # EF Core DbContext
│   │   ├── 📁 Repositories/             # Data access
│   │   └── 📁 Services/                 # External services
│   │
│   └── 📁 Danistay.WebAPI/              # Web API Layer
│       ├── 📁 Controllers/              # API Controllers
│       ├── 📁 wwwroot/uploads/          # File storage
│       └── 📄 Program.cs                # Application entry point
│
├── 📁 e-Ticaret-Portal/                # Frontend (Angular)
│   ├── 📁 src/app/
│   │   ├── 📁 pages/                    # Page components
│   │   │   ├── 📁 login/                # Login page
│   │   │   ├── 📁 dashboard/            # Dashboard page
│   │   │   ├── 📁 documents/            # Document management
│   │   │   └── 📁 profile/              # User profile
│   │   │
│   │   ├── 📁 services/                 # HTTP services
│   │   │   ├── 📄 auth.service.ts       # Authentication
│   │   │   └── 📄 document.service.ts   # Document operations
│   │   │
│   │   ├── 📁 models/                   # TypeScript models
│   │   └── 📁 app.routes.ts             # Routing configuration
│   │
│   ├── 📄 package.json                  # Dependencies
│   └── 📄 angular.json                  # Angular configuration
│
├── 📄 README.md                         # Bu dosya
├── 📄 .gitignore                        # Git ignore rules
└── 📄 e-Ticaret.sln        # Visual Studio solution
```

## 🔄 Geliştirme Süreci

### 🐛 Debug
```bash
# Backend debug
cd src/e-Ticaret.WebAPI
dotnet run --environment Development

# Frontend debug
cd e-Ticaret-Portal  
ng serve --configuration development
```

### 🏗️ Build
```bash
# Backend build
dotnet build --configuration Release

# Frontend build
ng build --configuration production
```

### 🧪 Test
```bash
# Backend tests
dotnet test

# Frontend tests
ng test
```

## 📊 Database Schema

### Users Table
- Id, Username, Email, FirstName, LastName
- PasswordHash, Department, Position
- IsActive, LastLoginDate, CreatedAt, UpdatedAt

### Documents Table
- Id, Title, Description, DocumentType, Status
- FilePath, FileExtension, FileSize
- CreatedByUserId, UpdatedByUserId
- IsDeleted, CreatedAt, UpdatedAt

### DocumentActions Table
- Id, DocumentId, UserId, ActionType
- ActionDate, Notes
- Audit trail for all document operations

## 🤝 Katkı Sağlama

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişiklikleri commit edin (`git commit -m 'Add some amazing feature'`)
4. Branch'i push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 📞 İletişim

- **Geliştirici:** [asazakk](https://github.com/asazakk)
- **Repository:** [Angular-ile-Belge-Y-netimi](https://github.com/asazakk/Angular-ile-Belge-Y-netimi)

## 🎯 Gelecek Güncellemeler

- [ ] Email bildirimleri
- [ ] Dosya versiyonlama
- [ ] Belge kategorileri
- [ ] İleri düzey arama filtreleri
- [ ] Belge paylaşım özellikleri
- [ ] Mobile responsive iyileştirmeler
- [ ] Docker containerization
- [ ] Unit test coverage artırımı

---

**🚀 Happy Coding!** 

Bu proje modern yazılım geliştirme pratikleri ve Clean Architecture prensiplerine uygun olarak geliştirilmiştir.
