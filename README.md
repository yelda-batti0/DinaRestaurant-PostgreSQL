<div align="center">

# 🍽️ Lezzet Bahçesi

### Restoran Yönetim Paneli & Analitik Dashboard

*Rezervasyonlar, menü, kategoriler ve müşteri değerlendirmeleri — tek panelden, canlı verilerle.*

<br>

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=nuget&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Chart.js](https://img.shields.io/badge/Chart.js-4.x-FF6384?style=for-the-badge&logo=chartdotjs&logoColor=white)](https://www.chartjs.org/)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)

<br>

**🇹🇷 Türkçe** &nbsp;•&nbsp; [🇬🇧 English](README.en.md)

</div>

---

## 📑 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Öne Çıkan Özellikler](#-öne-çıkan-özellikler)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Teknoloji Yığını](#️-teknoloji-yığını)
- [Sistem Mimarisi](#️-sistem-mimarisi)
- [ViewComponent Kompozisyonu](#-viewcomponent-kompozisyonu)
- [Klasör Yapısı](#-klasör-yapısı)
- [Veritabanı Tasarımı & ER Diyagramı](#️-veritabanı-tasarımı--er-diyagramı)
- [UML Diyagramları](#-uml-diyagramları)
- [İstek Yaşam Döngüsü](#-i̇stek-yaşam-döngüsü)
- [Kurulum ve Çalıştırma](#-kurulum-ve-çalıştırma)
- [Yapılandırma](#️-yapılandırma)
- [Rota Haritası](#️-rota-haritası)
- [Sık Karşılaşılan Sorunlar](#-sık-karşılaşılan-sorunlar)
- [Yol Haritası](#️-yol-haritası)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)
- [Geliştirici](#️-geliştirici)

---

## 🎯 Proje Hakkında

Bir restoranın günlük işleyişinde veriler dağınıktır: rezervasyon defteri ayrı, menü ayrı, müşteri yorumları bambaşka bir yerdedir. **Lezzet Bahçesi**, bu parçalı akışı tek bir yönetim panelinde toplayarak *"Bugün kaç rezervasyon var? Hangi gün ve saatler yoğun? Hangi ürün beğenilmiyor?"* sorularına saniyeler içinde cevap verir.

Uygulama **ASP.NET Core MVC** üzerine kurulu, veri katmanında **PostgreSQL + Entity Framework Core (Code First)** kullanan bir web projesidir. En ayırt edici tarafı, sayfaların monolitik `.cshtml` dosyaları yerine **ViewComponent kompozisyonu** ile inşa edilmiş olmasıdır: her başlık, her grafik, her tablo kendi verisini kendi çeken bağımsız bir bileşendir.

### Neden bu proje?

| Problem | Çözüm |
|---|---|
| Rezervasyon yoğunluğu tahminle yönetiliyor | Gün × saat kırılımlı **ısı haritası** ile gerçek yoğunluk görselleştirmesi |
| Yorumlar denetimsiz yayınlanıyor | `Status` alanı üzerinden **admin onay mekanizması** |
| Menü performansı ölçülemiyor | Kategori bazlı **ürün dağılımı** ve **ortalama fiyat** analizleri |
| View dosyaları şişiyor, bakımı zorlaşıyor | Sayfa başına 5–10 **bağımsız ViewComponent** |

### Temel Tasarım Kararları

- **Sayfa = bileşen kompozisyonu.** `Dashboard/Index.cshtml` yalnızca ViewComponent çağrılarından oluşur; başlık, istatistik kartları, üç grafik, son rezervasyonlar, son yorumlar ve script bloğu ayrı ayrı bileşenlerdir. Biri hata verse sayfanın geri kalanı ayakta kalır.
- **Entity'ler asla View'a gönderilmez.** Tüm sunum verisi `Dtos/` altındaki özelleşmiş DTO'lar üzerinden taşınır; dönüşüm `Mapping/` klasöründeki profil ile merkezîdir.
- **İnce controller.** Controller yalnızca isteği karşılar; sorgu ve hesaplama `Services/` katmanındadır.
- **Null-safe analitik.** Isı haritası ve grafik sorgularında veri bulunmayan gün/saat kombinasyonları `0` olarak normalize edilir, matriste boşluk oluşmaz.

---

## 🚀 Öne Çıkan Özellikler

### 📊 1. Dashboard — Yönetim Paneli Ana Ekranı

`DashboardController` + `DashboardViewComponents` ile kurulmuştur.

| Bileşen | Görevi |
|---|---|
| `_DashboardStatisticsCardsComponentPartial` | Toplam rezervasyon, bekleyen/onaylanan/iptal kırılımı, toplam ürün, kategori ve yorum sayısı |
| `_DashboardLineChartComponentPartial` | Günlük rezervasyon trendi (Line) |
| `_DashboardBarChartComponentPartial` | Kategoriye göre ürün dağılımı (Bar) |
| `_DashboardPieChartComponentPartial` | Kategori ortalama fiyatları (Pie / Doughnut) |
| `_DashboardLastReservationsComponentPartial` | Son rezervasyon kayıtları tablosu |
| `_DashboardLastReviewsComponentPartial` | Son gelen yorumlar akışı |
| `_DashboardQuickActionsComponentPartial` | Sık kullanılan işlemlere hızlı erişim kısayolları |
| `_DashboardHeaderComponentPartial` | Panel üst başlığı ve özet bilgi |
| `_DashboardScriptsComponentPartial` | Chart.js konfigürasyonlarının tek noktadan yüklenmesi |

### 🔥 2. İstatistik & Isı Haritası Sayfası

`StatisticsController` + `StatisticsViewComponents` ile kurulmuştur.

- **`_StatisticsHeatmapComponentPartial`** — Haftanın günleri (satır) × saat dilimleri (sütun) matrisinde rezervasyon yoğunluğu. Hücre rengi rezervasyon sayısıyla orantılı koyulaşır; personel vardiyası ve stok planlaması için doğrudan kullanılabilir bir çıktı üretir.
- **`_StatisticsBigGridComponentPartial`** — Genel metriklerin geniş ızgara görünümü.
- **`_StatisticsCategoryTableComponentPartial`** — Kategori bazlı ürün sayısı ve ortalama fiyat tablosu.
- **`_StatisticsHeroComponentPartial`** — Sayfa üst bölümü ve öne çıkan metrik özeti.

### 💬 3. Müşteri Değerlendirmeleri (Review) Modülü

- **Ürün bazlı yorum sistemi:** Her ürün için ayrı puanlama (1–5 ⭐) ve serbest metin değerlendirmesi.
- **Onay mekanizması:** Yorumlar `Status = false` ile kaydedilir; yalnızca onaylananlar menüde görünür.
- **Moderasyon paneli:** `ReviewController` üzerinden onayla / reddet / sil aksiyonları.
- **Ortalama puan:** Ürünün yıldız ortalaması yalnızca onaylı yorumlar üzerinden hesaplanır.

### 📅 4. Rezervasyon Yönetimi

- Durum makinesiyle yönetilen akış: `Bekliyor → Onaylandı → Tamamlandı` / `İptal`.
- Ad, telefon, e-posta, kişi sayısı, tarih, saat ve açıklama alanları.
- Tarihe ve duruma göre filtreleme; ısı haritasının veri kaynağı bu tablodur.

### 🍕 5. Menü & Kategori Yönetimi

- `CategoryController` ve `ProductController` üzerinden tam CRUD.
- Kategori/ürün `Status` alanı ile aktif–pasif yönetimi; pasif kayıtlar analitiklerden otomatik düşer.
- **`MenuController` + `MenuViewComponents`** ile ziyaretçiye açık menü sayfası: navbar, üst görsel bölümü, ürün listesi, mobil görünüm ve footer ayrı bileşenlerdir.

---

## 📸 Ekran Görüntüleri

<img width="2833" height="1628" alt="İstatistikler - 2" src="https://github.com/user-attachments/assets/0d738733-4b04-4c1f-82a5-22c774ee507a" />
<img width="2827" height="1621" alt="İstatistikler - 1" src="https://github.com/user-attachments/assets/f94ce2af-ebfc-48e7-9a63-bd5c18a093c1" />
<img width="2839" height="1634" alt="Dashboard Paneli - 2" src="https://github.com/user-attachments/assets/a3efda18-29da-415b-8cb6-377a8b1671dc" />
<img width="2830" height="1620" alt="Dashboard Paneli - 1" src="https://github.com/user-attachments/assets/a5743e11-bd62-4e66-b27d-5bf79b03de77" />
<img width="2831" height="1634" alt="Değerlendirme Düzenle" src="https://github.com/user-attachments/assets/21c1246a-7acd-4ea4-8d99-315f15e034eb" />
<img width="2831" height="1625" alt="Değerlendirme Listesi" src="https://github.com/user-attachments/assets/d432c952-3214-4603-9db1-7581075224b0" />
<img width="2836" height="1629" alt="Yeni Değerlendirme Oluştur" src="https://github.com/user-attachments/assets/f5cd3268-6641-4bbd-bc6d-9ba05149119a" />
<img width="2822" height="1637" alt="Rezervasyon Güncelle" src="https://github.com/user-attachments/assets/fb299496-990c-44ce-afae-82905e6c27bc" />
<img width="2826" height="1632" alt="Rezervasyon Oluştur" src="https://github.com/user-attachments/assets/53e4d038-e3bf-4d30-a82b-a7b8ff3b3a13" />
<img width="2834" height="1637" alt="Rezervasyon Listesi" src="https://github.com/user-attachments/assets/dbf7771e-72e5-46a8-89f9-8afcb057d7c1" />
<img width="2833" height="1627" alt="Ürün Düzenle" src="https://github.com/user-attachments/assets/68e01242-9f1f-491f-aa3f-08c0d72642ef" />
<img width="2834" height="1629" alt="Yeni Ürün Ekle" src="https://github.com/user-attachments/assets/af5bc2fa-2239-4f71-ac9c-2d92c5d94d7a" />
<img width="2832" height="1640" alt="Ürün Listesi" src="https://github.com/user-attachments/assets/0c0a2377-d6ad-45b5-b296-50fa606a9d15" />
<img width="2836" height="1628" alt="Yeni Kategori Ekle" src="https://github.com/user-attachments/assets/ae08921c-c1a7-4719-8401-65e73b8abf8f" />
<img width="2866" height="1624" alt="Kategori Kart Listesi" src="https://github.com/user-attachments/assets/32052f22-7ca9-44b9-84ce-fb7fea8d829c" />
<img width="2833" height="1621" alt="Kategori Listesi" src="https://github.com/user-attachments/assets/ba7ed2d2-2c7c-4c2d-b72c-99a23a22e3c4" />
<img width="2831" height="1631" alt="Menü" src="https://github.com/user-attachments/assets/99d45595-e598-4a2f-a57b-3b66b80e23f9" />


---

## 🛠️ Teknoloji Yığını

| Alan | Teknoloji | Kullanım Amacı |
|---|---|---|
| **Dil & Runtime** | C# 12, .NET 6.0 | Uygulama çekirdeği |
| **Web Framework** | ASP.NET Core MVC | Controller / View / Routing altyapısı |
| **ORM** | Entity Framework Core 8 | Code First, LINQ sorguları, Migration yönetimi |
| **Veritabanı** | PostgreSQL | İlişkisel veri deposu (`DinnerMenuDb`) |
| **DB Sağlayıcı** | Npgsql.EntityFrameworkCore.PostgreSQL | EF Core ↔ PostgreSQL köprüsü |
| **Nesne Dönüşümü** | AutoMapper | Entity ↔ DTO dönüşümü (`Mapping/`) |
| **Frontend** | HTML5, CSS3, JavaScript (ES6+) | Arayüz ve etkileşim |
| **UI Kütüphanesi** | Bootstrap 5 | Responsive grid ve bileşenler |
| **Görselleştirme** | Chart.js | Line / Bar / Pie grafikleri |
| **Şablon Motoru** | Razor (.cshtml) | Sunucu taraflı render |
| **Mimari Yaklaşım** | ViewComponent, DTO, Service Layer, Dependency Injection | Modülerlik ve bakım kolaylığı |

---

## 🏗️ Sistem Mimarisi

Proje, sorumlulukların net biçimde ayrıldığı katmanlı bir yapıdadır. Controller iş mantığı içermez; hesaplama servis katmanında, veri erişimi `Context` üzerinden yapılır.

```mermaid
flowchart TB
    subgraph L1["1 - Istemci Katmani"]
        BR["Tarayici (Bootstrap 5)"]
        CJ["Chart.js"]
    end

    subgraph L2["2 - Sunum Katmani (ASP.NET Core MVC)"]
        CT["Controllers"]
        VC["ViewComponents"]
        VW["Razor Views"]
    end

    subgraph L3["3 - Is Katmani"]
        SV["Services"]
        DTO["Dtos"]
        MP["Mapping (AutoMapper)"]
    end

    subgraph L4["4 - Veri Erisim Katmani"]
        CX["Context / DbContext"]
        EN["Entities"]
    end

    DB[("PostgreSQL - DinnerMenuDb")]

    BR --> CT
    CJ --> CT
    CT --> VC
    VC --> VW
    VW --> BR
    CT --> SV
    VC --> SV
    SV --> MP
    MP --> DTO
    SV --> CX
    CX --> EN
    CX --> DB
```

### Uygulanan Yaklaşımlar

| Yaklaşım | Nerede? | Kazanım |
|---|---|---|
| **ViewComponent Kompozisyonu** | `ViewComponents/`, `Views/Shared/Components/` | Her sayfa parçası bağımsız, yeniden kullanılabilir ve kendi verisini çeken bir birim |
| **DTO Pattern** | `Dtos/` (özellik bazlı alt klasörler) | Entity'ler View'a sızmaz; yalnızca gerekli alanlar taşınır |
| **AutoMapper Profili** | `Mapping/` | Dönüşüm mantığı tek noktada, controller'lar temiz |
| **Service Layer** | `Services/` | İş kuralları ve analitik hesaplamalar controller'dan ayrıştırılmış |
| **Dependency Injection** | `Program.cs` | Servisler ve `DbContext` constructor üzerinden enjekte edilir |
| **Code First + Migrations** | `Migrations/` | Şema versiyonlanır, ortamlar arası tutarlılık sağlanır |
| **Özellik Bazlı Klasörleme** | `Dtos/`, `ViewComponents/` | `CategoryDtos`, `ChartDtos`, `DashboardViewComponents`... — dosya bulmak kolay |

---

## 🧩 ViewComponent Kompozisyonu

Projenin en karakteristik yanı budur: bir sayfa tek parça `.cshtml` değil, birden fazla bağımsız bileşenin bir araya gelmesidir.

```mermaid
flowchart LR
    subgraph P1["Dashboard Sayfasi"]
        D1["_DashboardHeadComponentPartial"]
        D2["_DashboardHeaderComponentPartial"]
        D3["_DashboardStatisticsCardsComponentPartial"]
        D4["_DashboardLineChartComponentPartial"]
        D5["_DashboardBarChartComponentPartial"]
        D6["_DashboardPieChartComponentPartial"]
        D7["_DashboardLastReservationsComponentPartial"]
        D8["_DashboardLastReviewsComponentPartial"]
        D9["_DashboardQuickActionsComponentPartial"]
        D10["_DashboardScriptsComponentPartial"]
    end

    subgraph P2["Istatistik Sayfasi"]
        S1["_StatisticsHeroComponentPartial"]
        S2["_StatisticsBigGridComponentPartial"]
        S3["_StatisticsCategoryTableComponentPartial"]
        S4["_StatisticsHeatmapComponentPartial"]
    end

    subgraph P3["Menu Sayfasi"]
        M1["_MenuHeadComponentPartial"]
        M2["_MenuNavbarComponentPartial"]
        M3["_MenuTopImageSectionComponentPartial"]
        M4["_MenuListComponentPartial"]
        M5["_MenuMobileComponentPartial"]
        M6["_MenuFooterComponentPartial"]
        M7["_MenuScriptsComponentPartial"]
    end
```

Ortak grafik altyapısı `ChartViewComponents/_ChartComponentPartial` içinde toplanmıştır; Chart.js yapılandırması tekrar edilmez.

---

## 📁 Klasör Yapısı

```
DinnerMenu/
│
├── 📂 Context/                             # EF Core DbContext
│   └── DinnerMenuContext.cs
│
├── 📂 Controllers/
│   ├── AdminLayoutController.cs            # Yönetim paneli layout bileşenleri
│   ├── CategoryController.cs               # Kategori CRUD
│   ├── DashboardController.cs              # Ana panel
│   ├── HomeController.cs                   # Genel giriş sayfası
│   ├── MenuController.cs                   # Ziyaretçiye açık menü
│   ├── ProductController.cs                # Ürün CRUD
│   ├── ReservationController.cs            # Rezervasyon yönetimi
│   ├── ReviewController.cs                 # Yorum moderasyonu
│   └── StatisticsController.cs             # İstatistik & ısı haritası
│
├── 📂 Dtos/                                # Özellik bazlı veri taşıma nesneleri
│   ├── CategoryDtos/
│   ├── ChartDtos/                          # Grafik ve ısı haritası verileri
│   ├── ProductDtos/
│   ├── ReservationDtos/
│   └── ReviewDtos/
│
├── 📂 Entities/                            # Code First varlık sınıfları
│   ├── Category.cs
│   ├── Product.cs
│   ├── Reservation.cs
│   └── Review.cs
│
├── 📂 Mapping/                             # AutoMapper profilleri
├── 📂 Migrations/                          # EF Core migration geçmişi
├── 📂 Models/                              # ViewModel / ErrorViewModel
├── 📂 Properties/
│   └── launchSettings.json
│
├── 📂 Services/                            # İş kuralları & analitik sorgular
│
├── 📂 ViewComponents/
│   ├── ChartViewComponents/                # Ortak grafik bileşeni
│   ├── DashboardViewComponents/            # Panel bileşenleri
│   ├── MenuViewComponents/                 # Menü sayfası bileşenleri
│   └── StatisticsViewComponents/           # İstatistik & heatmap bileşenleri
│
├── 📂 Views/
│   ├── AdminLayout/
│   ├── Category/
│   ├── Dashboard/
│   ├── Home/
│   ├── Menu/
│   ├── Product/
│   ├── Reservation/
│   ├── Review/
│   ├── Statistics/
│   ├── Shared/
│   │   └── Components/                     # Her ViewComponent'in Default.cshtml'i
│   │       ├── _ChartComponentPartial/
│   │       ├── _DashboardStatisticsCardsComponentPartial/
│   │       ├── _DashboardLineChartComponentPartial/
│   │       ├── _StatisticsHeatmapComponentPartial/
│   │       └── ...
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
│
├── 📂 wwwroot/
│   ├── css/
│   ├── js/
│   ├── lib/
│   └── images/screenshots/
│
├── appsettings.json
├── Program.cs
└── README.md
```

---

## 🗄️ Veritabanı Tasarımı & ER Diyagramı

Veri modeli bilinçli olarak sade tutulmuştur: dört varlık, iki ilişki. Müşteri bilgisi ayrı bir tabloda tutulmaz — rezervasyon ve yorum kayıtları iletişim/isim alanlarını kendi içlerinde taşır.


```mermaid
erDiagram
    CATEGORY ||--o{ PRODUCT : icerir
    PRODUCT ||--o{ REVIEW : alir

    CATEGORY {
        int CategoryId PK
        string CategoryName
        string ImageUrl
        bool Status
    }

    PRODUCT {
        int ProductId PK
        int CategoryId FK
        string ProductName
        string Description
        decimal Price
        string ImageUrl
        bool Status
    }

    REVIEW {
        int ReviewId PK
        int ProductId FK
        string Name
        string Comment
        int Rating
        DateTime ReviewDate
        bool Status
    }

    RESERVATION {
        int ReservationId PK
        string Name
        string Mail
        string Phone
        int PersonCount
        DateTime ReservationDate
        string ReservationTime
        string Description
        string Status
    }
```

### Tablo Açıklamaları

| Tablo | Amaç | Kritik Alanlar |
|---|---|---|
| `Categories` | Menü kategorileri | `Status` — pasif kategoriler analitiklere dahil edilmez |
| `Products` | Menü ürünleri | `Price` (decimal), `CategoryId` (FK), `Status` |
| `Reviews` | Ürün değerlendirmeleri | `Rating` (1–5), `Status` — onay mekanizmasının anahtarı |
| `Reservations` | Rezervasyon kayıtları | `ReservationDate` + `ReservationTime` → ısı haritasının veri kaynağı |

### İlişkiler

```
Category  1 ────< N  Product     (Bir kategoride çok ürün)
Product   1 ────< N  Review      (Bir ürüne çok yorum)
Reservation                      (Bağımsız tablo, yabancı anahtar taşımaz)
```

---

## 📐 UML Diyagramları

### 1️⃣ Sınıf Diyagramı — Entities

```mermaid
classDiagram
    class Category {
        +int CategoryId
        +string CategoryName
        +string ImageUrl
        +bool Status
        +ICollection~Product~ Products
    }

    class Product {
        +int ProductId
        +string ProductName
        +string Description
        +decimal Price
        +string ImageUrl
        +bool Status
        +int CategoryId
        +Category Category
        +ICollection~Review~ Reviews
    }

    class Review {
        +int ReviewId
        +string Name
        +string Comment
        +int Rating
        +DateTime ReviewDate
        +bool Status
        +int ProductId
        +Product Product
    }

    class Reservation {
        +int ReservationId
        +string Name
        +string Mail
        +string Phone
        +int PersonCount
        +DateTime ReservationDate
        +string ReservationTime
        +string Description
        +string Status
    }

    Category "1" --> "*" Product
    Product "1" --> "*" Review
```

### 2️⃣ Servis & Bileşen Katmanı

```mermaid
classDiagram
    class DinnerMenuContext {
        +DbSet~Category~ Categories
        +DbSet~Product~ Products
        +DbSet~Reservation~ Reservations
        +DbSet~Review~ Reviews
        #OnModelCreating(ModelBuilder) void
    }

    class IStatisticsService {
        <<interface>>
        +GetSummaryAsync() DashboardSummaryDto
        +GetCategoryTableAsync() List~CategoryStatisticDto~
        +GetHeatmapAsync() List~HeatmapCellDto~
    }

    class StatisticsService {
        -DinnerMenuContext _context
        -IMapper _mapper
    }

    class IChartService {
        <<interface>>
        +GetDailyReservationsAsync() List~LineChartDto~
        +GetProductCountByCategoryAsync() List~BarChartDto~
        +GetAveragePriceByCategoryAsync() List~PieChartDto~
    }

    class ChartService {
        -DinnerMenuContext _context
    }

    class IReviewService {
        <<interface>>
        +GetPendingAsync() List~ResultReviewDto~
        +ApproveAsync(int id) Task
        +RejectAsync(int id) Task
    }

    class ReviewService {
        -DinnerMenuContext _context
    }

    class GeneralMapping {
        <<AutoMapper Profile>>
        +CreateMap() void
    }

    IStatisticsService <|.. StatisticsService
    IChartService <|.. ChartService
    IReviewService <|.. ReviewService
    StatisticsService --> DinnerMenuContext
    ChartService --> DinnerMenuContext
    ReviewService --> DinnerMenuContext
    StatisticsService --> GeneralMapping
```


### 3️⃣ Sequence Diyagramı — Dashboard Yüklenmesi

```mermaid
sequenceDiagram
    autonumber
    actor U as Yonetici
    participant BR as Tarayici
    participant DC as DashboardController
    participant VC as ViewComponent
    participant SV as Service
    participant CX as DinnerMenuContext
    participant DB as PostgreSQL
    participant CJ as Chart.js

    U->>BR: /Dashboard/Index adresine gider
    BR->>DC: GET /Dashboard/Index
    DC->>BR: Index.cshtml render baslar

    Note over VC,DB: Her ViewComponent kendi verisini bagimsiz ceker

    BR->>VC: Invoke _DashboardStatisticsCardsComponentPartial
    VC->>SV: GetSummaryAsync()
    SV->>CX: CountAsync sorgulari
    CX->>DB: SELECT COUNT(*)
    DB-->>CX: Sonuc kumesi
    CX-->>SV: Entity verisi
    SV-->>VC: DashboardSummaryDto
    VC-->>BR: Metrik kartlari HTML

    BR->>VC: Invoke _DashboardLineChartComponentPartial
    VC->>SV: GetDailyReservationsAsync()
    SV->>CX: Son 7 gun GroupBy Date
    CX->>DB: SELECT date, COUNT(*) GROUP BY date
    DB-->>CX: Gunluk toplamlar
    SV-->>VC: LineChartDto listesi
    VC-->>BR: canvas ve JSON veri

    BR->>VC: Invoke _DashboardPieChartComponentPartial
    VC->>SV: GetAveragePriceByCategoryAsync()
    SV->>CX: GroupBy CategoryId, Average Price
    CX->>DB: SELECT category, AVG(price) GROUP BY category
    DB-->>CX: Ortalama fiyatlar
    SV-->>VC: PieChartDto listesi
    VC-->>BR: canvas ve JSON veri

    BR->>CJ: Grafikleri ciz
    CJ-->>U: Tamamlanmis Dashboard
```

### 4️⃣ Sequence Diyagramı — Isı Haritası Üretimi

```mermaid
sequenceDiagram
    autonumber
    actor U as Yonetici
    participant SC as StatisticsController
    participant HC as HeatmapViewComponent
    participant SV as StatisticsService
    participant CX as DinnerMenuContext
    participant DB as PostgreSQL

    U->>SC: GET /Statistics/Index
    SC->>HC: Invoke _StatisticsHeatmapComponentPartial
    HC->>SV: GetHeatmapAsync()
    SV->>CX: Reservations GroupBy Gun, Saat
    CX->>DB: Zaman bazli toplama sorgusu
    DB-->>CX: Dolu hucrelerin sayilari
    CX-->>SV: Ham sonuc kumesi

    Note over SV: Rezervasyonu olmayan gun ve saat kombinasyonlari 0 ile doldurulur

    SV-->>HC: 7 x N boyutunda HeatmapCellDto matrisi
    HC-->>U: Renk yogunluklu matris tablosu
```

### 5️⃣ Sequence Diyagramı — Yorum Moderasyonu

```mermaid
sequenceDiagram
    autonumber
    actor C as Ziyaretci
    actor A as Yonetici
    participant RC as ReviewController
    participant RS as ReviewService
    participant DB as PostgreSQL
    participant MN as Menu Sayfasi

    C->>RC: POST /Review/Create
    RC->>RS: CreateAsync(dto)
    RS->>DB: INSERT Review, Status = false
    DB-->>RS: Kayit olustu
    RS-->>C: Yorumunuz onay bekliyor

    Note over MN: Onaysiz yorum menude gorunmez

    A->>RC: GET /Review/Index
    RC->>RS: GetPendingAsync()
    RS->>DB: SELECT WHERE Status = false
    DB-->>RS: Bekleyen yorum listesi
    RS-->>A: Moderasyon tablosu

    alt Onaylandi
        A->>RC: POST /Review/Approve
        RC->>RS: ApproveAsync(id)
        RS->>DB: UPDATE Status = true
        DB-->>MN: Yorum yayina alinir
        MN-->>C: Yorum ve ortalama puan guncellenir
    else Reddedildi
        A->>RC: POST /Review/Reject
        RC->>RS: RejectAsync(id)
        RS->>DB: UPDATE veya DELETE
        Note over MN: Yorum hicbir zaman yayinlanmaz
    end
```

### 6️⃣ Durum Diyagramı — Rezervasyon Yaşam Döngüsü

```mermaid
stateDiagram-v2
    [*] --> Bekliyor : Rezervasyon talebi olusturulur

    Bekliyor --> Onaylandi : Yonetici onaylar
    Bekliyor --> Iptal : Talep reddedilir

    Onaylandi --> Tamamlandi : Misafir geldi, hizmet verildi
    Onaylandi --> Iptal : Son dakika iptali

    Tamamlandi --> [*]
    Iptal --> [*]

    note right of Bekliyor
        Varsayilan durum.
        Dashboard'da Bekleyen sayacina dahildir.
    end note

    note right of Onaylandi
        Isi haritasi yogunluk
        hesabina dahil edilir.
    end note
```

### 7️⃣ Use Case Diyagramı

```mermaid
flowchart LR
    subgraph AK["Aktorler"]
        A1["Ziyaretci"]
        A2["Yonetici"]
    end

    subgraph SYS["Lezzet Bahcesi Sistemi"]
        UC1["Menuyu Goruntule"]
        UC2["Urun Yorumu Yap"]
        UC3["Rezervasyon Talebi Olustur"]
        UC4["Dashboard Metriklerini Izle"]
        UC5["Grafik Analizlerini Incele"]
        UC6["Isi Haritasi Analizi Yap"]
        UC7["Rezervasyonlari Yonet"]
        UC8["Yorumlari Onayla veya Reddet"]
        UC9["Kategori ve Urun Yonet"]
    end

    A1 --> UC1
    A1 --> UC2
    A1 --> UC3
    A2 --> UC4
    A2 --> UC5
    A2 --> UC6
    A2 --> UC7
    A2 --> UC8
    A2 --> UC9
```

---

## 🔄 İstek Yaşam Döngüsü

| # | Aşama | Sorumlu | Ne olur? |
|---|---|---|---|
| 1 | HTTP İsteği | Kestrel / Middleware | İstek karşılanır, routing çalışır |
| 2 | Controller Action | `DashboardController` | İsteği karşılar, View döndürür |
| 3 | ViewComponent | `DashboardViewComponents` | Sayfa parçası kendi verisini talep eder |
| 4 | Service | `Services/` | İş kuralı ve analitik hesaplama yapılır |
| 5 | DbContext | `DinnerMenuContext` | LINQ ifadesi SQL'e çevrilir |
| 6 | PostgreSQL | Veritabanı | Sorgu çalışır, satırlar döner |
| 7 | Entity → DTO | `Mapping/` (AutoMapper) | Sadece gerekli alanlar taşınır |
| 8 | Razor View | `Views/Shared/Components/` | Bileşenin HTML çıktısı üretilir |
| 9 | Chart.js | Tarayıcı | JSON veriden grafik çizilir |

---

## ⚡ Kurulum ve Çalıştırma

### Gereksinimler

| Gereksinim | Minimum Sürüm | İndirme |
|---|---|---|
| .NET SDK | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| PostgreSQL | 14+ (16 önerilir) | [postgresql.org](https://www.postgresql.org/download/) |
| pgAdmin 4 | Güncel | [pgadmin.org](https://www.pgadmin.org/download/) |
| IDE | Visual Studio 2022 / VS Code | — |
| EF Core CLI | 8.0 | `dotnet tool install --global dotnet-ef` |

### Adım Adım Kurulum

**1. Depoyu klonlayın**

```bash
git clone https://github.com/yelda-batti0/DinaRestaurant-PostgreSQL.git
cd DinaRestaurant-PostgreSQL
```

**2. Bağımlılıkları yükleyin**

```bash
dotnet restore
```

**3. PostgreSQL veritabanını oluşturun**

```sql
CREATE DATABASE "DinnerMenuDb"
    WITH ENCODING = 'UTF8'
    TEMPLATE = template0;
```

**4. Bağlantı dizesini yapılandırın**

`appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DinnerMenuDb;Username=postgres;Password=SIFRENIZ;Client Encoding=UTF8;"
  },
  "AllowedHosts": "*"
}
```

> 🔐 Şifrenizi repoya göndermeyin. Geliştirme ortamında User Secrets kullanın:
> ```bash
> dotnet user-secrets init
> dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;..."
> ```
>
> Bağlantı bilgisi `Context/DinnerMenuContext.cs` içinde `OnConfiguring` ile tanımlıysa, düzenlemeyi orada yapmanız gerekir.

**5. Migration'ları uygulayın**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Visual Studio **Package Manager Console** üzerinden:

```powershell
Add-Migration InitialCreate
Update-Database
```

**6. Uygulamayı çalıştırın**

```bash
dotnet run
# veya sıcak yeniden yükleme ile
dotnet watch run
```

**7. Tarayıcıda açın**

```
https://localhost:7044     → Ana sayfa
https://localhost:7044/Dashboard    → Yönetim paneli
https://localhost:7044/Statistics   → İstatistik & ısı haritası
https://localhost:7044/Menu         → Menü sayfası
```

> Portlar `Properties/launchSettings.json` dosyasında tanımlıdır.

### 🐳 Docker ile PostgreSQL (Opsiyonel)

```yaml
# docker-compose.yml
services:
  postgres:
    image: postgres:16-alpine
    container_name: lezzet-postgres
    environment:
      POSTGRES_DB: DinnerMenuDb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - lezzet-data:/var/lib/postgresql/data

volumes:
  lezzet-data:
```

```bash
docker compose up -d
```

---

## ⚙️ Yapılandırma

| Ayar | Dosya | Açıklama |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | `appsettings.json` | PostgreSQL bağlantı bilgileri |
| `applicationUrl` | `Properties/launchSettings.json` | Uygulamanın çalışacağı port |
| Servis kayıtları | `Program.cs` | DI konteynerine servis ekleme |
| Fluent API kısıtları | `Context/DinnerMenuContext.cs` | İlişkiler, indeksler, `decimal` precision |
| Dönüşüm profilleri | `Mapping/` | Entity ↔ DTO eşleştirmeleri |

**`Program.cs` — tipik servis kaydı:**

```csharp
builder.Services.AddDbContext<DinnerMenuContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddControllersWithViews();
```

---

## 🗺️ Rota Haritası

| HTTP | Rota | Controller / Action | Açıklama |
|---|---|---|---|
| `GET` | `/` | `HomeController.Index` | Genel giriş sayfası |
| `GET` | `/Menu` | `MenuController.Index` | Ziyaretçiye açık menü |
| `GET` | `/Dashboard` | `DashboardController.Index` | Yönetim paneli ana ekranı |
| `GET` | `/Statistics` | `StatisticsController.Index` | İstatistikler ve ısı haritası |
| `GET` | `/Category` | `CategoryController.Index` | Kategori listesi |
| `GET` `POST` | `/Category/Create` | `CategoryController.Create` | Yeni kategori |
| `GET` `POST` | `/Category/Update/{id}` | `CategoryController.Update` | Kategori güncelleme |
| `GET` | `/Category/Delete/{id}` | `CategoryController.Delete` | Kategori silme |
| `GET` | `/Product` | `ProductController.Index` | Ürün listesi |
| `GET` `POST` | `/Product/Create` | `ProductController.Create` | Yeni ürün |
| `GET` | `/Reservation` | `ReservationController.Index` | Rezervasyon listesi |
| `POST` | `/Reservation/Approve/{id}` | `ReservationController.Approve` | Rezervasyonu onaylar |
| `POST` | `/Reservation/Cancel/{id}` | `ReservationController.Cancel` | Rezervasyonu iptal eder |
| `GET` | `/Review` | `ReviewController.Index` | Yorum moderasyon listesi |
| `POST` | `/Review/Approve/{id}` | `ReviewController.Approve` | Yorumu yayına alır |
| `POST` | `/Review/Reject/{id}` | `ReviewController.Reject` | Yorumu reddeder |

> Action adları projedeki gerçek imzalarla eşleştirilmelidir.

---

## 🐛 Sık Karşılaşılan Sorunlar

<details>
<summary><b>❗ "Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone'"</b></summary>

Npgsql 6.0+ sürümlerinde `DateTime` davranışı değişti. İki çözümden birini uygulayın:

```csharp
// Program.cs — en üste ekleyin
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

veya entity'lerde UTC kullanın:

```csharp
public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
```

</details>

<details>
<summary><b>❗ Türkçe karakterler bozuk görünüyor</b></summary>

Bağlantı dizesinde `Client Encoding=UTF8;` bulunduğundan ve veritabanının UTF8 ile oluşturulduğundan emin olun. Ayrıca layout dosyasında:

```html
<meta charset="utf-8" />
```

</details>

<details>
<summary><b>❗ ViewComponent bulunamıyor / render edilmiyor</b></summary>

Bileşenin görünüm dosyası şu yolda ve tam olarak `Default.cshtml` adıyla bulunmalıdır:

```
Views/Shared/Components/{BilesenAdi}/Default.cshtml
```

Sınıf adı `XComponentPartialViewComponent` ise çağrı `@await Component.InvokeAsync("XComponentPartial")` biçimindedir; klasör adı da `_` öneki dahil birebir eşleşmelidir.

</details>

<details>
<summary><b>❗ Grafikler boş görünüyor</b></summary>

Tarayıcı konsolunu açın (F12). Sık nedenler:
- Chart.js CDN yüklenmemiş → `_DashboardScriptsComponentPartial` içindeki script sırasını kontrol edin.
- Veri `@Html.Raw(Json.Serialize(Model))` ile serialize edilmemiş.
- Boş veri seti → null-safe projeksiyon uygulanmamış.

</details>

<details>
<summary><b>❗ Isı haritasında bazı hücreler boş</b></summary>

Rezervasyon bulunmayan gün/saat kombinasyonları için varsayılan `0` üretilmelidir:

```csharp
var matrix = Enumerable.Range(0, 7)
    .SelectMany(day => hours.Select(hour => new HeatmapCellDto
    {
        Day   = day,
        Hour  = hour,
        Count = data.FirstOrDefault(x => x.Day == day && x.Hour == hour)?.Count ?? 0
    })).ToList();
```

</details>

<details>
<summary><b>❗ AutoMapper "Unmapped members were found" hatası</b></summary>

`Mapping/` klasöründeki profil dosyasında ilgili eşleştirmenin tanımlı olduğundan emin olun:

```csharp
CreateMap<Product, ResultProductDto>().ReverseMap();
```

</details>

---

## 🗓️ Yol Haritası

- [x] Dashboard metrik kartları ve hızlı işlem kısayolları
- [x] Chart.js entegrasyonu (Line / Bar / Pie)
- [x] Rezervasyon ısı haritası
- [x] Yorum onay ve moderasyon sistemi
- [x] Kategori & ürün yönetimi
- [x] ViewComponent tabanlı sayfa kompozisyonu
- [ ] ASP.NET Core Identity ile rol bazlı kimlik doğrulama
- [ ] `Customer` ve `Order` varlıklarının eklenmesi (sipariş takibi)
- [ ] SignalR ile gerçek zamanlı rezervasyon bildirimleri
- [ ] Excel / PDF rapor dışa aktarımı
- [ ] REST API katmanı + Swagger dokümantasyonu
- [ ] Birim testleri (xUnit + Moq)
- [ ] Çok dilli arayüz (Localization)
- [ ] Redis ile dashboard sorgu önbellekleme

---

## 🤝 Katkıda Bulunma

1. Depoyu **fork** edin
2. Yeni bir dal oluşturun: `git checkout -b feature/harika-ozellik`
3. Değişikliklerinizi commit edin: `git commit -m "feat: harika özellik eklendi"`
4. Dalınızı push edin: `git push origin feature/harika-ozellik`
5. Bir **Pull Request** açın

**Commit biçimi:** [Conventional Commits](https://www.conventionalcommits.org/) — `feat:` · `fix:` · `docs:` · `refactor:` · `test:` · `chore:`

---

## 📄 Lisans

Bu proje **MIT Lisansı** ile lisanslanmıştır. Ayrıntılar için [LICENSE](LICENSE) dosyasına bakın.

---

## ✒️ Geliştirici

<div align="center">

### Yelda Battı
**Bilişim Sistemleri Mühendisliği Öğrencisi**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/yelda-batti)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/yelda-batti0)

<br>

⭐ Proje işinize yaradıysa yıldız bırakmayı unutmayın!

</div>
