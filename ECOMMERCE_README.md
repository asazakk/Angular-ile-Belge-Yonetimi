# 🛍️ Çoklu Platform E-Ticaret Yönetim ve Otomasyon Sistemi

Modern, güvenli ve ölçeklenebilir bir çoklu platform e-ticaret yönetim sistemi. Trendyol, Hepsiburada, Amazon, N11 ve ÇiçekSepeti gibi platformları tek panelden yönetmenizi sağlar. .NET 9 Clean Architecture backend ve Angular 20 frontend teknolojileri kullanılarak geliştirilmiştir.

## 🚀 Özellikler

### 🛒 E-Ticaret Yönetimi

#### Mağaza Yönetimi
- ✅ Çoklu mağaza desteği
- ✅ Mağaza bazlı yetkilendirme
- ✅ Mağaza istatistikleri ve raporları

#### Platform Entegrasyonu
- ✅ Trendyol entegrasyonu (Hazır)
- ✅ Hepsiburada entegrasyonu (Hazır)
- ✅ Amazon entegrasyonu (Hazır)
- ✅ N11 entegrasyonu (Hazır)
- ✅ ÇiçekSepeti entegrasyonu (Hazır)
- ✅ API key ve token yönetimi
- ✅ Otomatik senkronizasyon
- ✅ Bağlantı testi

#### Ürün Yönetimi
- ✅ Merkezi ürün yönetimi
- ✅ Ürün oluşturma, düzenleme, silme
- ✅ Çoklu platform ürün listeleme
- ✅ Platform bazlı fiyat yönetimi
- ✅ Platform bazlı stok yönetimi
- ✅ Ürün kategorileme
- ✅ Çoklu ürün görseli
- ✅ SKU ve barkod desteği
- ✅ Otomatik stok uyarıları
- ✅ Gelişmiş arama ve filtreleme

#### Stok Yönetimi
- ✅ Merkezi stok takibi
- ✅ Otomatik stok senkronizasyonu
- ✅ Düşük stok uyarıları
- ✅ Stok değişim geçmişi
- ✅ Platform bazlı stok ayırma
- ✅ Sipariş sonrası otomatik stok düşümü

#### Fiyat Yönetimi
- ✅ Merkezi fiyat yönetimi
- ✅ Platform bazlı fiyat stratejileri
- ✅ Dinamik fiyatlandırma
- ✅ Kampanya yönetimi
- ✅ Fiyat değişim geçmişi
- ✅ Maliyet ve kar marjı takibi

#### Sipariş Yönetimi
- ✅ Tüm platformlardan merkezi sipariş takibi
- ✅ Sipariş durum güncellemeleri
- ✅ Kargo takip numarası yönetimi
- ✅ Sipariş detay görüntüleme
- ✅ Tarih bazlı sipariş filtreleme
- ✅ Sipariş istatistikleri
- ✅ Otomatik platform senkronizasyonu

### 📄 Belge Yönetimi (Mevcut)
- ✅ Belge oluşturma, düzenleme, silme
- ✅ Çoklu dosya formatı desteği
- ✅ Belge durum yönetimi
- ✅ Gelişmiş arama ve filtreleme

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
- ✅ Satış istatistikleri
- ✅ Stok durumu raporları
- ✅ Platform bazlı performans analizi
- ✅ En çok satan ürünler
- ✅ Gelir ve kar raporları

## 🏗️ Mimari

### 📋 Clean Architecture Pattern

```
├── 🌐 Presentation Layer (Angular Frontend)
│   ├── E-Commerce Pages (Stores, Products, Orders, Platforms)
│   ├── Document Pages (Login, Dashboard, Document Management, Profile)
│   ├── Services (HTTP Client, Authentication, E-Commerce)
│   └── Models (TypeScript Interfaces)
│
├── 🎯 API Layer (.NET WebAPI)
│   ├── Controllers (Auth, Documents, Products, Orders, Stores, Platforms)
│   ├── Middleware (Authentication, CORS)
│   └── Configuration (JWT, Swagger)
│
├── 🧠 Application Layer
│   ├── Services (Auth, Document, Product, Order, Store, Platform)
│   ├── DTOs (Data Transfer Objects)
│   └── Interfaces (Service Contracts)
│
├── 🏢 Domain Layer
│   ├── Entities (User, Document, Store, Product, Order, Platform)
│   ├── Enums (Document, E-Commerce, Platform Types)
│   └── Interfaces (Repository Contracts)
│
└── 🔧 Infrastructure Layer
    ├── Data (EF Core DbContext)
    ├── Repositories (Generic Repository, Unit of Work)
    └── Services (File, Platform API Services)
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

#### Platform Integrations
- **Trendyol API:** REST API entegrasyonu
- **Hepsiburada API:** REST API entegrasyonu
- **Amazon MWS:** Marketplace Web Service
- **N11 API:** REST API entegrasyonu
- **ÇiçekSepeti API:** REST API entegrasyonu

## 🚀 Kurulum ve Çalıştırma

### ⚡ Hızlı Başlangıç

#### 1. Repository'yi Klonlayın
```bash
git clone https://github.com/asazakk/Angular-ile-Belge-Yonetimi.git
cd Angular-ile-Belge-Yonetimi
```

#### 2. Backend'i Çalıştırın (.NET API)
```bash
cd src/Danistay.WebAPI
dotnet restore
dotnet run
```
🌐 API: http://localhost:5235 (Swagger: http://localhost:5235/swagger)

#### 3. Frontend'i Çalıştırın (Angular)
```bash
cd danistay-frontend
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
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Secret": "MySecretKeyForECommerceApp2025!@#$%^&*()",
    "Issuer": "ECommerceApp",
    "Audience": "ECommerceAppUsers",
    "ExpirationHours": 24
  },
  "PlatformSettings": {
    "Trendyol": {
      "BaseUrl": "https://api.trendyol.com",
      "Version": "v1"
    },
    "Hepsiburada": {
      "BaseUrl": "https://mpop-sit.hepsiburada.com",
      "Version": "v1"
    }
  }
}
```

### Frontend Configuration
- API Base URL: `http://localhost:5235/api`
- Auto-generated environment files
- Material UI theming

## 👤 Test Kullanıcıları

| Kullanıcı Adı | Şifre | Rol | Departman |
|---|---|---|---|
| `admin` | `admin123` | Yönetici | Bilgi İşlem |
| `user1` | `user123` | Kullanıcı | E-Ticaret |

## 🎯 API Endpoints

### 🔐 Authentication
```
POST /api/auth/login          # Kullanıcı girişi
GET  /api/auth/me            # Mevcut kullanıcı bilgileri
```

### 🏪 Stores (Mağazalar)
```
GET    /api/stores                    # Kullanıcının mağazaları
GET    /api/stores/{id}              # Mağaza detayı
GET    /api/stores/active            # Aktif mağazalar
POST   /api/stores                   # Yeni mağaza oluştur
PUT    /api/stores/{id}              # Mağaza güncelle
DELETE /api/stores/{id}              # Mağaza sil
```

### 🔌 Platform Integrations (Platform Entegrasyonları)
```
GET    /api/platformintegrations/store/{storeId}           # Mağaza entegrasyonları
GET    /api/platformintegrations/{id}                      # Entegrasyon detayı
POST   /api/platformintegrations                           # Yeni entegrasyon
PUT    /api/platformintegrations/{id}                      # Entegrasyon güncelle
DELETE /api/platformintegrations/{id}                      # Entegrasyon sil
POST   /api/platformintegrations/{id}/test-connection     # Bağlantı testi
POST   /api/platformintegrations/{id}/sync-products       # Ürün senkronizasyonu
POST   /api/platformintegrations/{id}/sync-orders         # Sipariş senkronizasyonu
POST   /api/platformintegrations/{id}/full-sync           # Tam senkronizasyon
```

### 📦 Products (Ürünler)
```
GET    /api/products/store/{storeId}                    # Mağaza ürünleri
GET    /api/products/{id}                               # Ürün detayı
GET    /api/products/{id}/with-platforms                # Ürün + platform bilgisi
GET    /api/products/store/{storeId}/low-stock          # Düşük stoklu ürünler
GET    /api/products/store/{storeId}/search?term=...    # Ürün arama
POST   /api/products                                     # Yeni ürün oluştur
PUT    /api/products/{id}                                # Ürün güncelle
DELETE /api/products/{id}                                # Ürün sil
PUT    /api/products/{id}/stock                          # Stok güncelle
PUT    /api/products/{id}/price                          # Fiyat güncelle
POST   /api/products/{id}/sync/{platformId}             # Platform senkronizasyonu
```

### 📋 Orders (Siparişler)
```
GET    /api/orders/store/{storeId}                      # Mağaza siparişleri
GET    /api/orders/{id}                                 # Sipariş detayı
GET    /api/orders/{id}/details                         # Sipariş + ürünler
GET    /api/orders/status/{status}                      # Duruma göre siparişler
GET    /api/orders/recent?count=10                      # Son siparişler
GET    /api/orders/date-range?startDate=...&endDate=... # Tarih aralığı
PUT    /api/orders/{id}/status                          # Sipariş durumu güncelle
GET    /api/orders/store/{storeId}/sales                # Satış toplamları
GET    /api/orders/store/{storeId}/statistics           # Sipariş istatistikleri
POST   /api/orders/sync/platform/{platformId}           # Platform senkronizasyonu
```

### 📄 Documents (Belgeler)
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
│   │   ├── 📁 Entities/                 # Varlıklar
│   │   │   ├── User, Document           # Belge yönetimi
│   │   │   ├── Store, PlatformIntegration # E-Ticaret
│   │   │   ├── Product, ProductPlatform  # Ürün yönetimi
│   │   │   ├── Order, OrderItem          # Sipariş yönetimi
│   │   │   └── StockHistory, PriceHistory # Geçmiş kayıtları
│   │   ├── 📁 Enums/                    # Enumerations
│   │   │   ├── DocumentStatus, DocumentType
│   │   │   ├── PlatformType, OrderStatus
│   │   │   └── StockStatus, PriceStrategy
│   │   └── 📁 Interfaces/               # Repository interfaces
│   │
│   ├── 📁 Danistay.Application/         # Application Layer
│   │   ├── 📁 Services/                 # Business logic
│   │   ├── 📁 DTOs/                     # Data Transfer Objects
│   │   │   ├── Auth/, Documents/
│   │   │   ├── Products/, Orders/
│   │   │   ├── Stores/, Platforms/
│   │   └── 📁 Interfaces/               # Service interfaces
│   │
│   ├── 📁 Danistay.Infrastructure/      # Infrastructure Layer
│   │   ├── 📁 Data/                     # EF Core DbContext
│   │   ├── 📁 Repositories/             # Data access
│   │   └── 📁 Services/                 # External services
│   │       ├── FileService
│   │       └── PlatformApiServices/
│   │
│   └── 📁 Danistay.WebAPI/              # Web API Layer
│       ├── 📁 Controllers/              # API Controllers
│       │   ├── AuthController
│       │   ├── DocumentsController
│       │   ├── ProductsController
│       │   ├── OrdersController
│       │   ├── StoresController
│       │   └── PlatformIntegrationsController
│       ├── 📁 wwwroot/uploads/          # File storage
│       └── 📄 Program.cs                # Application entry point
│
├── 📁 danistay-frontend/                # Frontend (Angular)
│   ├── 📁 src/app/
│   │   ├── 📁 pages/                    # Page components
│   │   │   ├── 📁 login/                # Login page
│   │   │   ├── 📁 dashboard/            # Dashboard page
│   │   │   ├── 📁 documents/            # Document management
│   │   │   ├── 📁 stores/               # Store management
│   │   │   ├── 📁 products/             # Product management
│   │   │   ├── 📁 orders/               # Order management
│   │   │   ├── 📁 platforms/            # Platform integration
│   │   │   └── 📁 profile/              # User profile
│   │   │
│   │   ├── 📁 services/                 # HTTP services
│   │   │   ├── 📄 auth.service.ts       # Authentication
│   │   │   ├── 📄 document.service.ts   # Document operations
│   │   │   ├── 📄 product.service.ts    # Product operations
│   │   │   ├── 📄 order.service.ts      # Order operations
│   │   │   ├── 📄 store.service.ts      # Store operations
│   │   │   └── 📄 platform.service.ts   # Platform operations
│   │   │
│   │   ├── 📁 models/                   # TypeScript models
│   │   └── 📁 app.routes.ts             # Routing configuration
│   │
│   ├── 📄 package.json                  # Dependencies
│   └── 📄 angular.json                  # Angular configuration
│
├── 📄 README.md                         # Bu dosya
├── 📄 ECOMMERCE.md                      # E-Ticaret dokümantasyonu
├── 📄 .gitignore                        # Git ignore rules
└── 📄 DanistayBelgeYonetimi.sln        # Visual Studio solution
```

## 🔄 Geliştirme Süreci

### 🐛 Debug
```bash
# Backend debug
cd src/Danistay.WebAPI
dotnet run --environment Development

# Frontend debug
cd danistay-frontend  
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

### E-Commerce Tables

#### Stores
- Id, Name, Description, UserId
- IsActive, CreatedAt, UpdatedAt

#### PlatformIntegrations
- Id, StoreId, PlatformType, PlatformStoreName
- ApiKey, ApiSecret, AccessToken, SellerId
- IsActive, LastSyncDate, LastSyncStatus

#### Categories
- Id, Name, Description, ParentCategoryId
- ImageUrl, DisplayOrder, IsActive

#### Products
- Id, Name, SKU, Barcode, Description
- CategoryId, StoreId, BasePrice, CostPrice
- StockQuantity, MinStockLevel, StockStatus
- Weight, Dimensions, MainImageUrl
- IsActive, PriceStrategy

#### ProductPlatforms
- Id, ProductId, PlatformIntegrationId
- PlatformProductId, PlatformPrice, PlatformStockQuantity
- IsListed, AutoSync, LastSyncDate

#### Orders
- Id, OrderNumber, PlatformIntegrationId, PlatformOrderId
- Status, TotalAmount, ShippingAmount, DiscountAmount
- CustomerName, CustomerEmail, CustomerPhone
- ShippingAddress, CargoCompany, TrackingNumber
- ShippedDate, DeliveredDate

#### OrderItems
- Id, OrderId, ProductId, ProductName, SKU
- Quantity, UnitPrice, DiscountAmount, TotalPrice

#### StockHistory
- Id, ProductId, PreviousQuantity, NewQuantity
- ChangeQuantity, ChangeType, Reason, OrderId

#### PriceHistory
- Id, ProductId, PreviousPrice, NewPrice
- ChangeType, Reason, ChangedByUserId

### Document Management Tables

#### Users
- Id, Username, Email, FirstName, LastName
- PasswordHash, Department, Position
- IsActive, LastLoginDate

#### Documents
- Id, Title, Description, DocumentType, Status
- FilePath, FileExtension, FileSize
- CreatedByUserId, UpdatedByUserId

#### DocumentActions
- Id, DocumentId, UserId, ActionType
- ActionDate, Notes

## 🎯 Gelecek Geliştirmeler

### E-Ticaret Modülü
- [ ] WhatsApp/Telegram bildirimleri
- [ ] Email otomasyonu
- [ ] Yapay zeka destekli satış tahminleri
- [ ] Dinamik fiyatlandırma algoritması
- [ ] Rakip fiyat analizi
- [ ] Otomatik stok siparişi
- [ ] Kargo entegrasyonları (Yurtiçi, Aras, MNG)
- [ ] Faturalandırma entegrasyonu (Paraşüt, eFatura)
- [ ] Müşteri yönetim sistemi (CRM)
- [ ] Promosyon ve kampanya yönetimi
- [ ] İade ve değişim yönetimi
- [ ] Çoklu dil desteği

### Belge Yönetim Modülü
- [ ] Email bildirimleri
- [ ] Dosya versiyonlama
- [ ] Belge kategorileri
- [ ] İleri düzey arama filtreleri
- [ ] Belge paylaşım özellikleri

### Genel İyileştirmeler
- [ ] Mobile responsive iyileştirmeler
- [ ] Docker containerization
- [ ] Unit test coverage artırımı
- [ ] Performance optimizasyonu
- [ ] Güvenlik testleri
- [ ] CI/CD pipeline
- [ ] Monitoring ve logging (Serilog, Application Insights)

## 📈 Platform API Entegrasyonları

### Desteklenen Platformlar

#### 1. Trendyol
- API Versiyonu: v1
- Desteklenen İşlemler:
  - Ürün listeleme ve güncelleme
  - Stok senkronizasyonu
  - Sipariş çekme
  - Kargo bilgisi güncelleme

#### 2. Hepsiburada
- API Versiyonu: v1
- Desteklenen İşlemler:
  - Ürün yönetimi
  - Stok takibi
  - Sipariş yönetimi
  - Fiyat güncelleme

#### 3. Amazon
- API: MWS (Marketplace Web Service)
- Desteklenen İşlemler:
  - Ürün katalog yönetimi
  - Envanter yönetimi
  - Sipariş işlemleri
  - Raporlama

#### 4. N11
- API Versiyonu: v1
- Desteklenen İşlemler:
  - Ürün entegrasyonu
  - Stok yönetimi
  - Sipariş takibi

#### 5. ÇiçekSepeti
- API Versiyonu: v1
- Desteklenen İşlemler:
  - Ürün senkronizasyonu
  - Sipariş yönetimi

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
- **Repository:** [Angular-ile-Belge-Yonetimi](https://github.com/asazakk/Angular-ile-Belge-Yonetimi)

## 💡 Önemli Notlar

### Güvenlik
- API anahtarları ve şifreler asla kod içerisinde saklanmaz
- Tüm hassas bilgiler environment variables veya secure vault'ta tutulur
- JWT token'lar güvenli şekilde yönetilir

### Performans
- Database indexleme optimize edilmiştir
- Ağır yükler için background job kullanılır
- Caching stratejileri uygulanmıştır

### Ölçeklenebilirlik
- Mikroservis mimarisine uygun tasarım
- Yatay ölçeklenebilir yapı
- Load balancing desteği

---

**🚀 Happy Coding & Happy Selling!** 

Bu proje modern yazılım geliştirme pratikleri, Clean Architecture prensipleri ve e-ticaret best practice'lerine uygun olarak geliştirilmiştir.
