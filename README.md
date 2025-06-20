# Görev Takip Sistemi

## Proje Hakkında
Görev Takip Sistemi, ekiplerin ve bireylerin görevlerini etkili şekilde yönetebilmesi için geliştirilmiş, çok katmanlı bir web uygulamasıdır. Hem modern bir frontend arayüzü hem de ölçeklenebilir bir backend mimarisi sunar.

## Temel Özellikler

### Kullanıcı Rolleri
- **Yönetici (Admin):** Kullanıcı ve görev yönetimi, raporlar
- **Çalışan (Worker):** Kişisel görev görüntüleme ve güncelleme
- **Yönetici (Manager):** Ekip ve görev atamaları, performans takibi
- **AI Asistan:** Yapay zeka destekli görev önerileri ve otomasyon

### Modüller ve Sayfalar
- **Görev Yönetimi:** Görev ekleme, atama, güncelleme, silme
- **Kullanıcı Yönetimi:** Personel ekleme ve yönetme
- **Raporlama:** Görev ve kullanıcı bazlı raporlar
- **Durum Görüntüleme:** Görevlerin ilerleme durumlarını izleme
- **Ayarlar:** Kişisel ve sistem ayarlarının yönetimi
- **Şifre Güncelleme:** Kullanıcı şifrelerini değiştirme
- **Yetkilendirme:** Rol bazlı erişim kontrolleri

## Proje Yapısı

```
/
├── .github/                 # GitHub yapılandırmaları
├── Entities/                # Veri varlıkları (C#)
├── GorevTakipSistemi/       # Backend uygulama kodları (.NET/C#)
│   ├── Program.cs           # Uygulama başlatıcı dosyası
│   ├── appsettings.json     # Genel yapılandırmalar
│   ├── nlog.config          # Log yapılandırması
│   └── ...                  # Diğer backend dosyaları
├── Presentation/            # Sunum katmanı (varsa)
├── Repositories/            # Veri erişim katmanı
├── Services/                # Hizmet katmanı (iş mantığı)
├── gorev-takip-sistemi-frontend/ # Frontend (HTML)
│   ├── index.html           # Giriş sayfası
│   ├── admin-page.html      # Yönetici paneli
│   ├── worker-page.html     # Çalışan paneli
│   └── ...                  # Diğer frontend dosyaları
└── GorevTakipSistemi.sln    # Çözüm dosyası (Solution)
```

## Kurulum ve Çalıştırma

### Backend (.NET)
1. Depoyu klonlayın:
    ```bash
    git clone https://github.com/MertUstun7/GorevTakipSistemi.git
    ```
2. .NET SDK kurulu olduğundan emin olun.
3. Backend dizinine gidin:
    ```bash
    cd GorevTakipSistemi
    ```
4. Gerekli NuGet paketlerini yükleyin:
    ```bash
    dotnet restore
    ```
5. Uygulamayı başlatın:
    ```bash
    dotnet run
    ```

### Frontend (HTML)
1. `gorev-takip-sistemi-frontend` klasörüne gidin.
2. İlgili HTML dosyasını tarayıcıda açın veya bir sunucu üzerinde barındırın.
3. (Opsiyonel) Backend API ile entegrasyon için gerekli endpoint ayarlarını kontrol edin (örn: fetch/axios ile API istekleri).

## Katkı Sağlama

1. Depoyu fork'layın.
2. Yeni bir branch oluşturun (`git checkout -b ozellik/yeniozellik`).
3. Değişikliklerinizi yapın ve commit edin.
4. Branch'inizi gönderin (`git push origin ozellik/yeniozellik`).
5. Bir pull request açın.

## Lisans
Bu proje için henüz bir lisans belirtilmemiştir.

## Daha Fazla Bilgi
Daha fazla dosya ve detay için lütfen 
- [Depo ana dizinini görüntüleyin](https://github.com/MertUstun7/GorevTakipSistemi/blob/master/)
- [Backend klasörünü görüntüleyin](https://github.com/MertUstun7/GorevTakipSistemi/tree/master/GorevTakipSistemi)
- [Frontend klasörünü görüntüleyin](https://github.com/MertUstun7/GorevTakipSistemi/tree/master/gorev-takip-sistemi-frontend)

Eklemek veya değiştirmek istediğiniz bölümler varsa belirtin!
