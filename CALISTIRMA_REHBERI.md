# 🚀 Projeyi Çalıştırma Rehberi

Bu rehber, projeyi ilk kez çalıştıracak geliştiriciler için adım adım talimatları içerir.

## 📋 Gerekli Yazılımlar

### Backend için:
- ✅ .NET 9 SDK - [İndir](https://dotnet.microsoft.com/download/dotnet/9.0)
- ✅ SQL Server LocalDB (SQL Server Express ile birlikte gelir) - [İndir](https://www.microsoft.com/sql-server/sql-server-downloads)
- ✅ Visual Studio Code (önerilen) - [İndir](https://code.visualstudio.com/)

### Frontend için:
- ✅ Node.js 18+ - [İndir](https://nodejs.org/)
- ✅ npm 9+ (Node.js ile birlikte gelir)
- ✅ Angular CLI 20

## 🎯 Adım Adım Kurulum

### 1️⃣ Repository'yi Klonlayın

```bash
git clone https://github.com/asazakk/Angular-ile-Belge-Yonetimi.git
cd Angular-ile-Belge-Yonetimi
```

### 2️⃣ Backend'i Hazırlayın ve Çalıştırın

#### A. Gerekli Paketleri Yükleyin
```bash
cd src/Danistay.WebAPI
dotnet restore
```

#### B. Veritabanını Oluşturun
```bash
# Migration'ları oluştur (eğer yoksa)
dotnet ef migrations add InitialCreate --project ../Danistay.Infrastructure

# Veritabanını oluştur ve migration'ları uygula
dotnet ef database update --project ../Danistay.Infrastructure
```

#### C. Backend'i Çalıştırın
```bash
dotnet run
```

✅ **Backend çalışıyor!**
- API: http://localhost:5235
- Swagger UI: http://localhost:5235/swagger

### 3️⃣ Frontend'i Hazırlayın ve Çalıştırın

Yeni bir terminal açın:

#### A. Frontend Klasörüne Gidin
```bash
cd danistay-frontend
```

#### B. Angular CLI'yi Global Olarak Yükleyin (eğer yoksa)
```bash
npm install -g @angular/cli@20
```

#### C. Gerekli Paketleri Yükleyin
```bash
npm install
```

#### D. Frontend'i Çalıştırın
```bash
npm start
# veya
ng serve
```

✅ **Frontend çalışıyor!**
- Web App: http://localhost:4200

## 🔐 İlk Giriş

Projeyi açtıktan sonra şu test kullanıcıları ile giriş yapabilirsiniz:

### Admin Kullanıcı
- **Kullanıcı Adı:** `admin`
- **Şifre:** `admin123`
- **Rol:** Yönetici
- **Departman:** Bilgi İşlem

### Normal Kullanıcı
- **Kullanıcı Adı:** `user1`
- **Şifre:** `user123`
- **Rol:** Kullanıcı
- **Departman:** E-Ticaret

## 🎨 Projeyi Görüntüleme

### 1. API Endpoints'leri Görüntüleme (Swagger)

Backend çalışırken tarayıcıda açın:
```
http://localhost:5235/swagger
```

Swagger UI'da tüm API endpoints'lerini görebilir ve test edebilirsiniz:

- **Auth Endpoints**: `/api/auth/*` - Giriş ve kimlik doğrulama
- **Products Endpoints**: `/api/products/*` - Ürün yönetimi
- **Orders Endpoints**: `/api/orders/*` - Sipariş yönetimi
- **Stores Endpoints**: `/api/stores/*` - Mağaza yönetimi
- **Platform Integrations**: `/api/platformintegrations/*` - Platform entegrasyonları
- **Documents Endpoints**: `/api/documents/*` - Belge yönetimi

### 2. Web Uygulamasını Görüntüleme

Frontend çalışırken tarayıcıda açın:
```
http://localhost:4200
```

Giriş yaptıktan sonra şu sayfaları görebilirsiniz:

#### Mevcut Sayfalar (Belge Yönetimi):
- 📊 **Dashboard**: Ana sayfa ve istatistikler
- 📄 **Belgeler**: Belge listesi ve yönetimi
- 👤 **Profil**: Kullanıcı profili

#### Yeni E-Ticaret API'leri (Backend Hazır):
API endpoints'leri hazır ancak frontend sayfaları henüz geliştirilmemiş:
- 🏪 Mağazalar
- 📦 Ürünler
- 📋 Siparişler
- 🔌 Platform Entegrasyonları

## 🧪 API'leri Test Etme

### Swagger UI ile Test

1. http://localhost:5235/swagger adresini açın
2. **Authorize** butonuna tıklayın
3. Önce `/api/auth/login` endpoint'ini kullanarak giriş yapın:
   ```json
   {
     "username": "admin",
     "password": "admin123"
   }
   ```
4. Dönen token'ı kopyalayın
5. **Authorize** butonuna tekrar tıklayın ve token'ı yapıştırın: `Bearer <token>`
6. Artık diğer tüm endpoints'leri test edebilirsiniz!

### Örnek API Çağrıları

#### Mağaza Oluşturma
```bash
curl -X POST http://localhost:5235/api/stores \
  -H "Authorization: Bearer <your-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Benim Mağazam",
    "description": "İlk e-ticaret mağazam"
  }'
```

#### Ürün Oluşturma
```bash
curl -X POST http://localhost:5235/api/products \
  -H "Authorization: Bearer <your-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "iPhone 15 Pro",
    "sku": "IPHONE15PRO-256",
    "basePrice": 45000,
    "stockQuantity": 100,
    "categoryId": 1,
    "storeId": 1
  }'
```

## 🐛 Sorun Giderme

### Backend Sorunları

#### "LocalDB başlatılamıyor" hatası:
```bash
# SQL Server Express'i yükleyin
# LocalDB'yi başlatın
sqllocaldb start MSSQLLocalDB
```

#### "Migration hatası":
```bash
# Veritabanını sıfırla ve yeniden oluştur
dotnet ef database drop --force --project ../Danistay.Infrastructure
dotnet ef database update --project ../Danistay.Infrastructure
```

#### Port zaten kullanılıyor (5235):
```bash
# Program.cs'de portu değiştirin veya çalışan uygulamayı kapatın
# Alternatif olarak:
dotnet run --urls "http://localhost:5236"
```

### Frontend Sorunları

#### "npm install" hatası:
```bash
# npm cache'i temizle
npm cache clean --force
npm install
```

#### "ng komut bulunamadı":
```bash
# Angular CLI'yi global olarak yükle
npm install -g @angular/cli@20
```

#### Port zaten kullanılıyor (4200):
```bash
# Farklı port kullan
ng serve --port 4201
```

## 📊 Veritabanını Görüntüleme

### SQL Server Management Studio (SSMS) ile:
1. SSMS'i açın
2. Server name: `(localdb)\mssqllocaldb`
3. Authentication: Windows Authentication
4. Connect'e tıklayın
5. Databases altında `DanistayBelgeYonetimiDb` veritabanını bulun

### VS Code SQL Extension ile:
1. VS Code'da SQL Server extension'ını yükleyin
2. Connection string: `Server=(localdb)\mssqllocaldb;Database=DanistayBelgeYonetimiDb;Trusted_Connection=true;`

## 🎯 Sonraki Adımlar

### Geliştirme İçin:

1. **Backend Geliştirme**:
   - Service implementasyonlarını tamamlayın
   - Repository implementasyonlarını tamamlayın
   - Platform API entegrasyonlarını ekleyin

2. **Frontend Geliştirme**:
   - E-ticaret sayfalarını oluşturun
   - API servisleri ekleyin
   - Component'leri geliştirin

3. **Test**:
   - Unit testleri yazın
   - Integration testleri ekleyin
   - E2E testleri oluşturun

### Test Senaryoları:

1. **Belge Yönetimi Test**:
   - Admin ile giriş yapın
   - Yeni belge oluşturun
   - Belge durumunu değiştirin
   - Belge arayın

2. **E-Ticaret API Test** (Swagger ile):
   - Mağaza oluşturun
   - Kategori ekleyin
   - Ürün ekleyin
   - Platform entegrasyonu oluşturun

## 📞 Yardım

Sorunlarla karşılaşırsanız:
1. Log'ları kontrol edin (console output)
2. ECOMMERCE_README.md dosyasına bakın
3. IMPLEMENTATION_GUIDE.md'deki örnekleri inceleyin
4. GitHub Issues'da yeni issue açın

## ✅ Kontrol Listesi

Başarılı kurulum için:

- [ ] .NET 9 SDK yüklü
- [ ] Node.js 18+ yüklü
- [ ] SQL Server LocalDB çalışıyor
- [ ] Backend çalışıyor (http://localhost:5235)
- [ ] Swagger açılıyor (http://localhost:5235/swagger)
- [ ] Frontend çalışıyor (http://localhost:4200)
- [ ] Giriş yapabiliyorum (admin/admin123)
- [ ] Dashboard görünüyor
- [ ] API test ediliyor (Swagger)

---

**İyi Kodlamalar! 🚀**
