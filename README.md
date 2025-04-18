# Mecmua Blog Platformu

Mecmua, ASP.NET Core kullanılarak oluşturulmuş modern ve zengin özelliklere sahip bir blog platformudur. Temiz ve duyarlı bir kullanıcı arayüzü ile blog makalelerini yayınlamak ve yönetmek için kapsamlı bir içerik yönetim sistemi sunar.

## Özellikler

- **Kullanıcı Yönetimi**
  - Rol tabanlı yetkilendirme (Admin, Editör, Yazar, Üye)
  - Kullanıcı kaydı ve kimlik doğrulama
  - Profil fotoğrafları ve biyografi içeren kullanıcı profilleri

- **İçerik Yönetimi**
  - Makale oluşturma ve yönetme
  - Kategori ve etiket organizasyonu
  - Zengin metin editörü desteği
  - Medya yüklemeleri
  - Öne çıkan makaleler
  - Görüntülenme sayısı takibi

- **Etkileşim Özellikleri**
  - Yorumlar ve moderasyon
  - Beğeni fonksiyonu
  - Bildirim sistemi
  - Makale arama

- **Duyarlı Kullanıcı Arayüzü**
  - Modern, duyarlı tasarım
  - Mobil uyumlu arayüz
  - Temiz tipografi

## Teknoloji Altyapısı

- **Backend**
  - ASP.NET Core 6+
  - Entity Framework Core
  - ASP.NET Core Identity
  - SQL Server

- **Frontend**
  - HTML5, CSS3, JavaScript
  - Abstract şablonundan duyarlı tasarım

## Gereksinimler

Bu projeyi çalıştırmak için aşağıdakilere ihtiyacınız vardır:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) veya daha yenisi
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express veya Developer sürümü yeterlidir)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) veya [Visual Studio Code](https://code.visualstudio.com/)

## Başlangıç

### Veritabanı Kurulumu

1. `appsettings.json` dosyasındaki bağlantı dizesini SQL Server örneğinize göre güncelleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SUNUCUNUZ;Database=MecmuaDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

2. Uygulama ilk kez çalıştırıldığında veritabanını otomatik olarak oluşturacak ve başlangıç verilerini ekleyecektir.

### Uygulamayı Çalıştırma

#### Visual Studio Kullanarak:

1. Çözüm dosyasını (`Mecmua.sln`) Visual Studio'da açın
2. Bağlantı dizesinin doğru yapılandırıldığından emin olun
3. Projeyi derlemek ve çalıştırmak için F5 tuşuna basın
4. Uygulama başlayacak ve varsayılan web tarayıcınızda açılacaktır

#### Komut Satırı Kullanarak:

1. Proje dizinine gidin
2. Aşağıdaki komutları çalıştırın:

```bash
dotnet restore
dotnet build
dotnet run
```

3. Web tarayıcınızı açın ve `https://localhost:5001` veya `http://localhost:5000` adresine gidin

### Varsayılan Kullanıcılar

Uygulama aşağıdaki varsayılan kullanıcılarla birlikte gelmektedir:

| E-posta | Şifre | Rol |
|-------|----------|------|
| admin@mecmua.com | Admin123. | Admin |
| editor@mecmua.com | Editor123. | Editör |
| yazar@mecmua.com | Yazar123. | Yazar |
| uye@mecmua.com | Uye123. | Üye |

## Proje Yapısı

- **Controllers/**: Tüm MVC denetleyicilerini içerir
- **Models/**: Veri modellerini içerir
- **Views/**: Razor görünümlerini içerir
- **Data/**: Veritabanı bağlamını ve depoları içerir
- **wwwroot/**: Statik dosyaları içerir (CSS, JS, görseller)
- **Areas/**: Admin paneli gibi alana özgü özellikleri içerir

## Uygulama Akışı

1. Kullanıcılar giriş yapmadan makaleleri görebilirler
2. Yazarlar makale oluşturup gönderebilirler
3. Editörler makaleleri inceleyip yayınlayabilirler
4. Adminler platform üzerinde tam kontrole sahiptir
5. Üyeler makalelere yorum yapabilir ve içerikle etkileşimde bulunabilirler

## Özelleştirme

### Tema Değiştirme

Site, Abstract temasından CSS kullanır. Görünümü şu dosyaları değiştirerek özelleştirebilirsiniz:

- `wwwroot/css/theme.css`
- `wwwroot/css/site.css`
- `wwwroot/css/abstract/styles.css`

### Özellik Ekleme

Modüler mimari kolay genişlemeye izin verir. Ek özellikler uygulamak için yeni denetleyiciler ve görünümler ekleyebilirsiniz.

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için LICENSE dosyasına bakın.

## Teşekkürler

- Abstract temasını temel alır
- ASP.NET Core ile oluşturulmuştur
- Veri erişimi için Entity Framework Core kullanır
- Kimlik doğrulama ve yetkilendirme için ASP.NET Core Identity kullanır 