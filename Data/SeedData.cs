using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mecmua.Models;
using System.Text.Json;

namespace Mecmua.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Check if roles already exist
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                // Create roles if they don't exist
                string[] roleNames = { "Admin", "Editör", "Yazar", "Üye" };
                
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                
                // Create admin user if it doesn't exist
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                
                if (await userManager.FindByEmailAsync("admin@mecmua.com") == null)
                {
                    var adminUser = new AppUser
                    {
                        UserName = "admin@mecmua.com",
                        Email = "admin@mecmua.com",
                        EmailConfirmed = true,
                        Nickname = "Admin",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    
                    var result = await userManager.CreateAsync(adminUser, "Admin123.");
                    
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
                
                // Create more users if they don't exist
                if (await userManager.FindByEmailAsync("editor@mecmua.com") == null)
                {
                    var editorUser = new AppUser
                    {
                        UserName = "editor@mecmua.com",
                        Email = "editor@mecmua.com",
                        EmailConfirmed = true,
                        Nickname = "EditorMecmua",
                        ProfilePictureUrl = "/img/user1.jpg",
                        Bio = "İçerik editörü ve sosyal medya uzmanı",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    
                    var result = await userManager.CreateAsync(editorUser, "Editor123.");
                    
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(editorUser, "Editör");
                    }
                }
                
                if (await userManager.FindByEmailAsync("yazar@mecmua.com") == null)
                {
                    var yazarUser = new AppUser
                    {
                        UserName = "yazar@mecmua.com",
                        Email = "yazar@mecmua.com",
                        EmailConfirmed = true,
                        Nickname = "YazarMecmua",
                        ProfilePictureUrl = "/img/user2.jpg",
                        Bio = "Yazar ve içerik üreticisi",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    
                    var result = await userManager.CreateAsync(yazarUser, "Yazar123.");
                    
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(yazarUser, "Yazar");
                    }
                }
                
                if (await userManager.FindByEmailAsync("uye@mecmua.com") == null)
                {
                    var uyeUser = new AppUser
                    {
                        UserName = "uye@mecmua.com",
                        Email = "uye@mecmua.com",
                        EmailConfirmed = true,
                        Nickname = "ÜyeMecmua",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    
                    var result = await userManager.CreateAsync(uyeUser, "Uye123.");
                    
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(uyeUser, "Üye");
                    }
                }
                
                // Create default categories if they don't exist
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category
                        {
                            Name = "Genel",
                            Slug = "genel",
                            Description = "Genel kategoride yazılar",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Category
                        {
                            Name = "Teknoloji",
                            Slug = "teknoloji",
                            Description = "Teknoloji ile ilgili yazılar",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Category
                        {
                            Name = "Yaşam",
                            Slug = "yasam",
                            Description = "Yaşam ile ilgili yazılar",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Category
                        {
                            Name = "Sağlık",
                            Slug = "saglik",
                            Description = "Sağlık ile ilgili yazılar",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Category
                        {
                            Name = "Eğitim",
                            Slug = "egitim",
                            Description = "Eğitim ile ilgili yazılar",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                }
                
                // Create default tags if they don't exist
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag
                        {
                            Name = "Yazılım",
                            Slug = "yazilim",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Tag
                        {
                            Name = "Web Geliştirme",
                            Slug = "web-gelistirme",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Tag
                        {
                            Name = "Mobil",
                            Slug = "mobil",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Tag
                        {
                            Name = "Spor",
                            Slug = "spor",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Tag
                        {
                            Name = "Beslenme",
                            Slug = "beslenme",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                }
                
                // Create default settings if they don't exist
                if (!context.Settings.Any())
                {
                    context.Settings.AddRange(
                        new Setting
                        {
                            Key = "SiteTitle",
                            Value = "Mecmua",
                            Description = "Site başlığı",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Setting
                        {
                            Key = "SiteDescription",
                            Value = "Mecmua Blog Platformu",
                            Description = "Site açıklaması",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Setting
                        {
                            Key = "Logo",
                            Value = "/img/logo.png",
                            Description = "Site logosu",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Setting
                        {
                            Key = "Favicon",
                            Value = "/img/favicon.ico",
                            Description = "Site favicon",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Setting
                        {
                            Key = "FooterText",
                            Value = "© 2024 Mecmua Blog Platformu. Tüm hakları saklıdır.",
                            Description = "Alt bilgi metni",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        },
                        new Setting
                        {
                            Key = "EmailSender",
                            Value = "info@mecmua.com",
                            Description = "E-posta göndericisi",
                            CreatedAt = DateTime.Now,
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                }
                
                // Create articles if they don't exist
                if (!context.Articles.Any())
                {
                    var adminUser = await userManager.FindByEmailAsync("admin@mecmua.com");
                    var editorUser = await userManager.FindByEmailAsync("editor@mecmua.com");
                    var yazarUser = await userManager.FindByEmailAsync("yazar@mecmua.com");
                    
                    var teknoloji = context.Categories.FirstOrDefault(c => c.Slug == "teknoloji");
                    var saglik = context.Categories.FirstOrDefault(c => c.Slug == "saglik");
                    var yasam = context.Categories.FirstOrDefault(c => c.Slug == "yasam");
                    var egitim = context.Categories.FirstOrDefault(c => c.Slug == "egitim");
                    
                    // Create articles
                    var article1 = new Article
                    {
                        Title = "ASP.NET Core ile Web Geliştirme",
                        Slug = "asp-net-core-ile-web-gelistirme",
                        Summary = "ASP.NET Core ile modern web uygulamaları geliştirmek için temel bilgiler ve en iyi pratikler",
                        Content = @"<p>ASP.NET Core, Microsoft tarafından geliştirilen açık kaynak kodlu, platformlar arası bir web framework'üdür. Bu makale, ASP.NET Core ile modern web uygulamaları geliştirmeye başlamanız için ihtiyacınız olan temel bilgileri sunuyor.</p>

<h2>Neden ASP.NET Core?</h2>
<p>ASP.NET Core, performans, ölçeklenebilirlik ve modern web mimarileri için tasarlanmıştır. İşte ASP.NET Core'un en önemli avantajları:</p>

<ul>
  <li><strong>Platform Bağımsızlık:</strong> Windows, Mac ve Linux üzerinde çalışabilir</li>
  <li><strong>Yüksek Performans:</strong> .NET 6 ve daha üstü sürümlerle önemli performans artışı sağlar</li>
  <li><strong>Açık Kaynak:</strong> GitHub üzerinde geliştirilen açık kaynak bir projedir</li>
  <li><strong>Modern Mimari:</strong> Mikroservisler ve container mimarileri için ideal bir çözümdür</li>
  <li><strong>Dependency Injection:</strong> Framework seviyesinde built-in dependency injection desteği vardır</li>
</ul>

<h3>ASP.NET Core Projeleri Oluşturma</h3>
<p>Yeni bir ASP.NET Core projesi oluşturmak için .NET CLI (Command Line Interface) kullanabilirsiniz:</p>

<pre><code>dotnet new webapp -n MyCoreApp</code></pre>

<p>Bu komut, MyCoreApp adında yeni bir ASP.NET Core web uygulaması oluşturur. Proje oluşturulduktan sonra, aşağıdaki komutla çalıştırabilirsiniz:</p>

<pre><code>cd MyCoreApp
dotnet run</code></pre>

<h3>MVC Pattern</h3>
<p>ASP.NET Core, MVC (Model-View-Controller) tasarım desenini destekler. Bu desen, uygulamanızı üç ana bileşene ayırmanıza yardımcı olur:</p>

<ul>
  <li><strong>Model:</strong> Veri ve iş mantığınızı temsil eder</li>
  <li><strong>View:</strong> Kullanıcı arayüzünü temsil eder</li>
  <li><strong>Controller:</strong> Kullanıcı isteklerini işler ve model ile view arasında iletişim kurar</li>
</ul>

<h3>Razor Pages</h3>
<p>Razor Pages, ASP.NET Core ile gelen sayfa tabanlı bir programlama modelidir. MVC'ye alternatif olarak daha basit senaryolar için kullanılabilir.</p>

<h2>Entity Framework Core</h2>
<p>Entity Framework Core, ASP.NET Core uygulamalarınız için bir ORM (Object-Relational Mapping) çözümüdür. Veritabanı işlemlerinizi nesne yönelimli bir şekilde yapmanıza olanak tanır.</p>

<blockquote>
  <p>ASP.NET Core, modern web uygulamaları geliştirmek için en güçlü araçlardan biridir. Hem performans hem de geliştirici deneyimi açısından büyük avantajlar sunar.</p>
</blockquote>

<h3>ASP.NET Core Identity</h3>
<p>ASP.NET Core Identity, kimlik doğrulama, yetkilendirme ve kullanıcı yönetimi için bir çözüm sunar. Kullanıcı kaydı, giriş, şifre sıfırlama gibi temel özelliklerin yanı sıra rol tabanlı yetkilendirme de sağlar.</p>

<p>Sonuç olarak, ASP.NET Core, modern web uygulamaları geliştirmek için mükemmel bir seçimdir. Hem performans hem de geliştirici deneyimi açısından büyük avantajlar sunar. Bu makale, ASP.NET Core'a giriş niteliğindedir ve daha derinlemesine bilgi için resmi dokümantasyonu incelemenizi öneririz.</p>",
                        CategoryId = teknoloji.Id,
                        AuthorId = adminUser.Id,
                        CreatedAt = DateTime.Now.AddDays(-10),
                        ViewCount = 120,
                        IsFeatured = true,
                        IsActive = true,
                        MediaUrlsJson = JsonSerializer.Serialize(new List<string> { "/img/blog/aspnet.jpg" })
                    };
                    context.Articles.Add(article1);
                    
                    var article2 = new Article
                    {
                        Title = "Sağlıklı Beslenme İpuçları",
                        Slug = "saglikli-beslenme-ipuclari",
                        Summary = "Daha sağlıklı bir yaşam için beslenme alışkanlıklarını düzenleme rehberi",
                        Content = @"<p>Sağlıklı beslenme, genel sağlığınızı korumanın ve hastalık riskini azaltmanın en önemli adımlarından biridir. Bu makalede, günlük beslenme alışkanlıklarınızı düzenlemek için pratik ipuçları paylaşacağız.</p>

<h2>Su İçmeyi İhmal Etmeyin</h2>
<p>Yeterli su tüketimi, metabolizmanız, sindirim sisteminiz ve genel sağlığınız için çok önemlidir. Günde en az 8-10 bardak su içmeyi hedeflemelisiniz. Su içme ipuçları:</p>

<ul>
  <li>Yanınızda her zaman bir su şişesi bulundurun</li>
  <li>Her ana öğün ile birlikte bir bardak su için</li>
  <li>Uyanır uyanmaz bir bardak su için</li>
  <li>Telefonunuza su içme hatırlatıcıları kurun</li>
</ul>

<h2>Mevsiminde Sebze ve Meyve Tüketin</h2>
<p>Mevsiminde tüketilen sebze ve meyveler hem daha lezzetli hem de daha besleyicidir. Günde en az 5 porsiyon sebze ve meyve tüketmeyi hedefleyin. Farklı renklerde sebze ve meyve seçerek çeşitli besin öğelerinden faydalanabilirsiniz.</p>

<h3>Renkli Tabaklar Hazırlayın</h3>
<p>Beslenme uzmanları, tabağınızın farklı renklerde besinlerden oluşmasını önerir. Bu şekilde farklı vitamin ve minerallerden yararlanabilirsiniz:</p>

<ul>
  <li><strong>Kırmızı:</strong> Domates, kırmızı biber, çilek (antioksidanlar)</li>
  <li><strong>Yeşil:</strong> Ispanak, brokoli, kivi (folik asit, demir)</li>
  <li><strong>Mor/Mavi:</strong> Mor lahana, yaban mersini (antioksidanlar)</li>
  <li><strong>Sarı/Turuncu:</strong> Havuç, portakal (beta-karoten, C vitamini)</li>
</ul>

<h2>İşlenmiş Gıdalardan Uzak Durun</h2>
<p>İşlenmiş gıdalar genellikle yüksek miktarda tuz, şeker ve sağlıksız yağlar içerir. Bunun yerine tam gıdaları tercih edin:</p>

<blockquote>
  <p>Sağlıklı beslenme, bir diyet değil, yaşam tarzı değişikliğidir. Küçük adımlarla başlayın ve zaman içinde alışkanlıklarınızı değiştirin.</p>
</blockquote>

<h2>Protein Kaynaklarınızı Çeşitlendirin</h2>
<p>Protein, kas yapımı ve onarımı için gereklidir. Ancak protein kaynaklarınızı çeşitlendirmek önemlidir:</p>

<ul>
  <li>Yağsız et</li>
  <li>Balık (haftada en az 2 kez)</li>
  <li>Baklagiller (mercimek, nohut, fasulye)</li>
  <li>Yumurta</li>
  <li>Süt ürünleri</li>
  <li>Tofu ve diğer bitki bazlı protein kaynakları</li>
</ul>

<h2>Porsiyon Kontrolü Yapın</h2>
<p>Ne kadar sağlıklı beslenirseniz beslenin, porsiyon kontrolü yapmadan kilo kontrolü sağlamak zordur. Porsiyon kontrolü için birkaç ipucu:</p>

<ul>
  <li>Küçük tabaklar kullanın</li>
  <li>Yemek yerken ekranlardan uzak durun</li>
  <li>Yavaş yiyin ve her lokmanın tadını çıkarın</li>
  <li>Açlık ve tokluk sinyallerinize dikkat edin</li>
</ul>

<h2>Sonuç</h2>
<p>Sağlıklı beslenme, bir gecede değiştirebileceğiniz bir şey değildir. Küçük adımlarla başlayın ve zaman içinde alışkanlıklarınızı değiştirin. Unutmayın, genel amaç mükemmellik değil, tutarlılıktır.</p>",
                        CategoryId = saglik.Id,
                        AuthorId = editorUser.Id,
                        CreatedAt = DateTime.Now.AddDays(-8),
                        ViewCount = 85,
                        IsFeatured = true,
                        IsActive = true,
                        MediaUrlsJson = JsonSerializer.Serialize(new List<string> { "/img/blog/beslenme.jpg" })
                    };
                    context.Articles.Add(article2);
                    
                    var article3 = new Article
                    {
                        Title = "Ev Çalışma Ortamını Düzenleme",
                        Slug = "ev-calisma-ortamini-duzenleme",
                        Summary = "Evden çalışırken verimliliği artırmak için çalışma alanı düzenleme önerileri",
                        Content = @"<p>Evden çalışmak, iş-yaşam dengesini korumada bir çok avantaj sunsa da, verimli bir çalışma ortamı oluşturmak önemlidir. Bu makalede, ev çalışma alanınızı nasıl daha verimli hale getirebileceğinize dair ipuçları paylaşıyoruz.</p>

<h2>Düzenli Bir Alan Oluşturun</h2>
<p>Çalışma alanınızı ev yaşamından ayırmak, daha odaklanmış bir çalışma deneyimi sağlayabilir. Mümkünse, evinizde sadece çalışmaya ayrılmış bir alan oluşturun. Bu alan, bir oda, bir köşe veya hatta sadece bir masa olabilir.</p>

<ul>
  <li>Çalışma alanınızı sadece iş için kullanın</li>
  <li>Her gün düzenli ve temiz tutun</li>
  <li>Dikkat dağıtıcı öğeleri uzaklaştırın</li>
</ul>

<h2>Ergonomik Bir Çalışma İstasyonu Kurun</h2>
<p>Uzun saatler çalışırken fiziksel sağlığınızı korumak için ergonomik bir çalışma istasyonu önemlidir:</p>

<ul>
  <li><strong>Sandalye:</strong> İyi bir bel desteğine sahip, ayarlanabilir bir sandalye tercih edin</li>
  <li><strong>Masa:</strong> Doğru yükseklikte bir masa kullanın (dirseğinizle aynı seviyede olmalı)</li>
  <li><strong>Monitör:</strong> Gözlerinizle aynı seviyede veya biraz aşağıda olmalı</li>
  <li><strong>Klavye ve Fare:</strong> Bileklerinizi doğal bir pozisyonda tutacak şekilde yerleştirin</li>
</ul>

<h3>Doğal Işık ve Havalandırma</h3>
<p>Doğal ışık, moralinizi yükseltir ve göz yorgunluğunu azaltır. Çalışma alanınızı mümkünse bir pencere yanına kurun. Ayrıca düzenli olarak odanızı havalandırmak da önemlidir.</p>

<h2>Teknoloji ve Bağlantı</h2>
<p>Evden çalışırken güvenilir bir internet bağlantısı ve doğru teknolojik ekipmanlara sahip olmak çok önemlidir:</p>

<ul>
  <li>Hızlı ve güvenilir internet bağlantısı</li>
  <li>Yedek internet seçeneği (örn. mobil hotspot)</li>
  <li>Kulaklık ve mikrofon (video konferanslar için)</li>
  <li>Gerekirse UPS (kesintisiz güç kaynağı)</li>
</ul>

<blockquote>
  <p>Evden çalışmanın en büyük zorluklarından biri iş ve özel yaşam arasındaki sınırları korumaktır. Çalışma saatlerinizi belirleyin ve onlara sadık kalın.</p>
</blockquote>

<h2>Sınırları Belirleyin</h2>
<p>Evden çalışırken, aile üyeleri veya ev arkadaşları ile sınırları belirlemek önemlidir:</p>

<ul>
  <li>Çalışma saatlerinizi paylaşın</li>
  <li>Rahatsız edilmek istemediğiniz zamanları belirtin</li>
  <li>Mümkünse çalışma alanınız için bir kapı veya bölme kullanın</li>
</ul>

<h2>Düzenli Molalar Verin</h2>
<p>Evden çalışırken, ofisteyken olduğundan daha uzun süre ara vermeden çalışma eğiliminde olabilirsiniz. Düzenli molalar verin:</p>

<ul>
  <li>Pomodoro Tekniği gibi zaman yönetimi teknikleri kullanın</li>
  <li>Her saat başı en az 5 dakika ayağa kalkın ve hareket edin</li>
  <li>Öğle arası için bilgisayardan uzaklaşın</li>
</ul>

<h2>İş Saatlerini Net Bir Şekilde Belirleyin</h2>
<p>Evden çalışmanın dezavantajlarından biri, iş ve kişisel yaşam arasındaki çizginin bulanıklaşmasıdır. Net çalışma saatleri belirleyin ve gün sonunda iş modundan çıkın:</p>

<ul>
  <li>İş e-postalarını mesai saatleri dışında kontrol etmeyin</li>
  <li>Günün sonunda çalışma alanınızı temizleyip toplayın</li>
  <li>İş günü sonunda bir ritüel oluşturun (örn. kısa bir yürüyüş)</li>
</ul>

<p>Evden çalışmak, doğru düzenleme ve alışkanlıklarla çok verimli olabilir. Kendi ihtiyaçlarınıza göre bu önerileri uyarlayın ve size en uygun çalışma ortamını oluşturun.</p>",
                        CategoryId = yasam.Id,
                        AuthorId = yazarUser.Id,
                        CreatedAt = DateTime.Now.AddDays(-5),
                        ViewCount = 65,
                        IsFeatured = false,
                        IsActive = true,
                        MediaUrlsJson = JsonSerializer.Serialize(new List<string> { "/img/blog/homeoffice.jpg" })
                    };
                    context.Articles.Add(article3);
                    
                    var article4 = new Article
                    {
                        Title = "Online Eğitim Platformları",
                        Slug = "online-egitim-platformlari",
                        Summary = "En iyi online eğitim platformları ve kurslar hakkında detaylı bilgiler",
                        Content = @"<p>Online eğitim platformları, her yaştan ve seviyeden insana öğrenme fırsatı sunuyor. Bu makalede, en popüler online eğitim platformlarını ve sundukları kursları inceliyoruz.</p>

<h2>Udemy</h2>
<p>Udemy, binlerce eğitmen tarafından hazırlanan çeşitli konularda on binlerce kursa erişim sağlayan bir platformdur. Teknoloji, iş, kişisel gelişim, sanat, dil öğrenimi ve daha pek çok kategoride kurs bulabilirsiniz.</p>

<h3>Udemy'nin Avantajları</h3>
<ul>
  <li>Geniş kurs yelpazesi</li>
  <li>Uygun fiyatlı kurslar (özellikle indirim dönemlerinde)</li>
  <li>Ömür boyu erişim</li>
  <li>Mobil uygulama ile offline öğrenme imkanı</li>
</ul>

<h3>Udemy'nin Dezavantajları</h3>
<ul>
  <li>Kurs kalitesi değişkenlik gösterebilir</li>
  <li>Sertifikalar genellikle endüstri tarafından tanınmaz</li>
</ul>

<h2>Coursera</h2>
<p>Coursera, dünya çapında tanınmış üniversiteler ve şirketler tarafından hazırlanan kurslar sunar. Stanford, Yale, Google ve IBM gibi kuruluşlardan sertifikalı kurslar alabilirsiniz.</p>

<h3>Coursera'nın Avantajları</h3>
<ul>
  <li>Prestijli kurumlardan alınan sertifikalar</li>
  <li>Akademik derinliğe sahip kurslar</li>
  <li>Tam derece programları</li>
  <li>Ücretsiz denetleme seçeneği</li>
</ul>

<h3>Coursera'nın Dezavantajları</h3>
<ul>
  <li>Bazı kurslar ve dereceler pahalı olabilir</li>
  <li>Daha akademik odaklı, bazen teorik olabilir</li>
</ul>

<blockquote>
  <p>Online öğrenmenin en büyük avantajı, kendi hızınızda ve programınıza göre öğrenme imkanı sunmasıdır.</p>
</blockquote>

<h2>LinkedIn Learning (Eski adıyla Lynda.com)</h2>
<p>LinkedIn Learning, iş dünyasına yönelik beceriler geliştirmek için ideal bir platformdur. Kurslar genellikle profesyonel gelişim, iş becerileri ve teknoloji üzerine odaklanır.</p>

<h3>LinkedIn Learning'in Avantajları</h3>
<ul>
  <li>Profesyonel olarak üretilmiş yüksek kaliteli içerik</li>
  <li>LinkedIn profilinize direkt sertifika ekleme</li>
  <li>Aylık abonelik ile sınırsız erişim</li>
</ul>

<h3>LinkedIn Learning'in Dezavantajları</h3>
<ul>
  <li>Aylık abonelik maliyeti yüksek olabilir</li>
  <li>Udemy veya Coursera kadar geniş konu yelpazesi yok</li>
</ul>

<h2>edX</h2>
<p>edX, Harvard ve MIT tarafından kurulan, kâr amacı gütmeyen bir eğitim platformudur. Dünyanın en iyi üniversitelerinden kurslar sunar.</p>

<h3>edX'in Avantajları</h3>
<ul>
  <li>Üst düzey akademik kurslar</li>
  <li>Mikro lisans ve yüksek lisans programları</li>
  <li>Ücretsiz denetleme seçeneği</li>
</ul>

<h3>edX'in Dezavantajları</h3>
<ul>
  <li>Sertifikalar için ödeme gerekiyor</li>
  <li>Kurslar genellikle belirli tarihlerde başlıyor</li>
</ul>

<h2>Khan Academy</h2>
<p>Khan Academy, tamamen ücretsiz, kâr amacı gütmeyen bir eğitim platformudur. Özellikle matematik, fen bilimleri ve temel eğitim için mükemmeldir.</p>

<h3>Khan Academy'nin Avantajları</h3>
<ul>
  <li>Tamamen ücretsiz</li>
  <li>K-12 eğitimi için mükemmel</li>
  <li>Kişiselleştirilmiş öğrenme deneyimi</li>
</ul>

<h2>Nasıl Başlamalı?</h2>
<p>Online eğitim platformlarında başarılı olmak için birkaç öneri:</p>

<ol>
  <li>Belirli öğrenme hedefleri belirleyin</li>
  <li>Düzenli çalışma alışkanlığı geliştirin</li>
  <li>Not alın ve aktif öğrenme tekniklerini kullanın</li>
  <li>Öğrendiklerinizi pratikte uygulayın</li>
  <li>Forum ve tartışma alanlarına katılın</li>
</ol>

<p>Sonuç olarak, online eğitim platformları hayat boyu öğrenme için harika kaynaklardır. Kendi ihtiyaçlarınıza ve öğrenme stilinize en uygun platformu seçerek bilgi ve becerilerinizi geliştirmeye başlayabilirsiniz.</p>",
                        CategoryId = egitim.Id,
                        AuthorId = adminUser.Id,
                        CreatedAt = DateTime.Now.AddDays(-3),
                        ViewCount = 45,
                        IsFeatured = false,
                        IsActive = true,
                        MediaUrlsJson = JsonSerializer.Serialize(new List<string> { "/img/blog/education.jpg" })
                    };
                    context.Articles.Add(article4);
                    
                    var article5 = new Article
                    {
                        Title = "Mobil Uygulama Geliştirmeye Giriş",
                        Slug = "mobil-uygulama-gelistirmeye-giris",
                        Summary = "Android ve iOS için mobil uygulama geliştirme sürecini öğrenin",
                        Content = @"<p>Mobil uygulama geliştirme, günümüzde en popüler yazılım geliştirme alanlarından biridir. Bu makalede, mobil uygulama geliştirmeye başlamak için temel bilgileri ele alacağız.</p>

<h2>Neden Mobil Uygulama Geliştirmelisiniz?</h2>
<p>Mobil uygulamalar, kullanıcılarla etkileşim kurmanın en etkili yollarından biridir. İşte mobil uygulama geliştirmenin bazı avantajları:</p>

<ul>
  <li>Geniş kullanıcı kitlesi</li>
  <li>Doğrudan kullanıcı etkileşimi</li>
  <li>Pazarlama ve gelir fırsatları</li>
  <li>Çeşitli cihaz özelliklerine erişim (kamera, GPS, vb.)</li>
</ul>

<h2>Mobil Platformlar</h2>
<p>Mobil uygulama geliştirirken, hedef platformu belirlemeniz önemlidir. İki ana mobil platform vardır:</p>

<h3>Android</h3>
<p>Android, Google tarafından geliştirilen ve dünya çapında en yaygın kullanılan mobil işletim sistemidir. Android uygulamaları genellikle Java veya Kotlin ile geliştirilir.</p>

<h3>iOS</h3>
<p>iOS, Apple'ın iPhone ve iPad gibi cihazlar için geliştirdiği işletim sistemidir. iOS uygulamaları Swift veya Objective-C ile geliştirilir.</p>

<h2>Geliştirme Yaklaşımları</h2>
<p>Mobil uygulama geliştirmek için birkaç farklı yaklaşım vardır:</p>

<h3>Native Uygulama Geliştirme</h3>
<p>Native uygulama geliştirme, platformun kendi programlama dili ve araçlarını kullanarak geliştirme anlamına gelir:</p>

<ul>
  <li><strong>Android için:</strong> Java veya Kotlin, Android Studio</li>
  <li><strong>iOS için:</strong> Swift veya Objective-C, Xcode</li>
</ul>

<p><strong>Avantajları:</strong> En iyi performans, tüm cihaz özelliklerine erişim, platform özelliklerine tam uyum.</p>
<p><strong>Dezavantajları:</strong> Her platform için ayrı kod tabanı gerektirir, daha yüksek geliştirme maliyeti.</p>

<h3>Cross-Platform Uygulama Geliştirme</h3>
<p>Cross-platform geliştirme, tek bir kod tabanı ile birden fazla platform için uygulama geliştirmenizi sağlar. Popüler cross-platform çözümleri:</p>

<ul>
  <li><strong>React Native:</strong> JavaScript kullanarak native benzeri uygulamalar geliştirme</li>
  <li><strong>Flutter:</strong> Dart diliyle UI odaklı uygulama geliştirme</li>
  <li><strong>Xamarin:</strong> C# ile native performans sağlayan uygulamalar</li>
</ul>

<p><strong>Avantajları:</strong> Daha hızlı geliştirme süreci, tek kod tabanı, daha düşük maliyet.</p>
<p><strong>Dezavantajları:</strong> Bazı platform özelliklerine erişimde sınırlamalar, bazen performans sorunları.</p>

<blockquote>
  <p>En iyi geliştirme yaklaşımı, projenizin ihtiyaçlarına ve ekibinizin yeteneklerine bağlıdır. Basit uygulamalar için cross-platform çözümler yeterli olabilirken, karmaşık uygulamalar için native geliştirme daha uygun olabilir.</p>
</blockquote>

<h2>Temel Mobil Uygulama Bileşenleri</h2>
<p>Bir mobil uygulamanın temel bileşenleri şunlardır:</p>

<h3>Kullanıcı Arayüzü (UI)</h3>
<p>Kullanıcı arayüzü, kullanıcının uygulamayla etkileşime girdiği görsel alandır. İyi bir UI tasarımı, kullanıcı deneyimini büyük ölçüde etkiler.</p>

<h3>İş Mantığı</h3>
<p>Uygulamanın temel işlevselliğini sağlayan kodlama katmanıdır.</p>

<h3>Veri Depolama</h3>
<p>Uygulamanın verileri nasıl sakladığı ve yönettiği ile ilgilidir. Yerel depolama, veritabanları veya bulut çözümleri kullanılabilir.</p>

<h3>API Entegrasyonu</h3>
<p>Çoğu modern uygulama, harici hizmetlerle iletişim kurmak için API'lar (Uygulama Programlama Arayüzleri) kullanır.</p>

<h2>Geliştirme Süreci</h2>
<p>Mobil uygulama geliştirme süreci genellikle şu adımları içerir:</p>

<ol>
  <li><strong>Fikir ve Planlama:</strong> Uygulama fikrinizi tanımlayın ve hedef kitlenizi belirleyin</li>
  <li><strong>Pazar Araştırması:</strong> Rakip uygulamaları analiz edin ve pazar boşluklarını belirleyin</li>
  <li><strong>UI/UX Tasarımı:</strong> Kullanıcı deneyimi ve arayüz tasarımını oluşturun</li>
  <li><strong>Geliştirme:</strong> Uygulamanın kodlamasını yapın</li>
  <li><strong>Test Etme:</strong> Uygulamanızı farklı cihazlarda ve senaryolarda test edin</li>
  <li><strong>Yayınlama:</strong> Uygulamanızı app store'larda yayınlayın</li>
  <li><strong>Pazarlama ve Bakım:</strong> Uygulamanızı tanıtın ve düzenli güncellemeler yapın</li>
</ol>

<h2>Başlangıç İçin Kaynaklar</h2>
<p>Mobil uygulama geliştirmeye başlamak için bazı kaynaklar:</p>

<ul>
  <li>Android Developers (Google'ın resmi dokümanları)</li>
  <li>Apple Developer (Apple'ın resmi dokümanları)</li>
  <li>Udemy, Coursera gibi platformlardaki mobil uygulama geliştirme kursları</li>
  <li>GitHub'daki açık kaynak projeler</li>
  <li>Stack Overflow gibi soru-cevap platformları</li>
</ul>

<p>Mobil uygulama geliştirme, sürekli öğrenme ve pratik gerektiren bir alandır. Küçük projelerle başlayıp, zamanla daha karmaşık uygulamalar geliştirerek becerilerinizi geliştirebilirsiniz.</p>",
                        CategoryId = teknoloji.Id,
                        AuthorId = yazarUser.Id,
                        CreatedAt = DateTime.Now.AddDays(-2),
                        ViewCount = 55,
                        IsFeatured = true,
                        IsActive = true,
                        MediaUrlsJson = JsonSerializer.Serialize(new List<string> { "/img/blog/mobile-app.jpg" })
                    };
                    context.Articles.Add(article5);
                    
                    await context.SaveChangesAsync();
                    
                    // Add tags to articles
                    var yazilimTag = context.Tags.FirstOrDefault(t => t.Slug == "yazilim");
                    var webTag = context.Tags.FirstOrDefault(t => t.Slug == "web-gelistirme");
                    var mobilTag = context.Tags.FirstOrDefault(t => t.Slug == "mobil");
                    var sporTag = context.Tags.FirstOrDefault(t => t.Slug == "spor");
                    var beslenmeTag = context.Tags.FirstOrDefault(t => t.Slug == "beslenme");
                    
                    context.ArticleTags.AddRange(
                        new ArticleTag { ArticleId = article1.Id, TagId = yazilimTag.Id },
                        new ArticleTag { ArticleId = article1.Id, TagId = webTag.Id },
                        new ArticleTag { ArticleId = article2.Id, TagId = beslenmeTag.Id },
                        new ArticleTag { ArticleId = article2.Id, TagId = sporTag.Id },
                        new ArticleTag { ArticleId = article3.Id, TagId = yazilimTag.Id },
                        new ArticleTag { ArticleId = article4.Id, TagId = webTag.Id },
                        new ArticleTag { ArticleId = article5.Id, TagId = mobilTag.Id },
                        new ArticleTag { ArticleId = article5.Id, TagId = yazilimTag.Id }
                    );
                    
                    await context.SaveChangesAsync();
                    
                    // Add comments to articles
                    var uye = await userManager.FindByEmailAsync("uye@mecmua.com");
                    
                    context.Comments.AddRange(
                        new Comment
                        {
                            ArticleId = article1.Id,
                            UserId = uye.Id,
                            Content = "Çok bilgilendirici bir makale olmuş, teşekkürler!",
                            CreatedAt = DateTime.Now.AddDays(-9),
                            IsApproved = true,
                            IsActive = true
                        },
                        new Comment
                        {
                            ArticleId = article1.Id,
                            UserId = editorUser.Id,
                            Content = "ASP.NET Core hakkında daha fazla makale görmek isterim.",
                            CreatedAt = DateTime.Now.AddDays(-8),
                            IsApproved = true,
                            IsActive = true
                        },
                        new Comment
                        {
                            ArticleId = article2.Id,
                            GuestName = "Sağlık Sever",
                            Content = "Bu ipuçlarını uygulamaya başladım ve gerçekten fark yaratıyor!",
                            CreatedAt = DateTime.Now.AddDays(-7),
                            IsApproved = true,
                            IsActive = true
                        },
                        new Comment
                        {
                            ArticleId = article3.Id,
                            UserId = uye.Id,
                            Content = "Pandemi sürecinde evden çalışmaya başladım ve bu öneriler çok işime yaradı.",
                            CreatedAt = DateTime.Now.AddDays(-4),
                            IsApproved = true,
                            IsActive = true
                        },
                        new Comment
                        {
                            ArticleId = article4.Id,
                            UserId = yazarUser.Id,
                            Content = "Udemy dışında Coursera da harika bir platform, oradaki kursları da değerlendirebilirsiniz.",
                            CreatedAt = DateTime.Now.AddDays(-2),
                            IsApproved = true,
                            IsActive = true
                        },
                        new Comment
                        {
                            ArticleId = article5.Id,
                            UserId = uye.Id,
                            Content = "Mobil uygulama geliştirmeye ilgi duyuyorum ve bu makale işimi kolaylaştırdı.",
                            CreatedAt = DateTime.Now.AddDays(-1),
                            IsApproved = true,
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                    
                    // Add likes to articles
                    context.Likes.AddRange(
                        new Like
                        {
                            ArticleId = article1.Id,
                            UserId = uye.Id,
                            CreatedAt = DateTime.Now.AddDays(-9),
                            IsActive = true
                        },
                        new Like
                        {
                            ArticleId = article1.Id,
                            UserId = editorUser.Id,
                            CreatedAt = DateTime.Now.AddDays(-8),
                            IsActive = true
                        },
                        new Like
                        {
                            ArticleId = article2.Id,
                            UserId = adminUser.Id,
                            CreatedAt = DateTime.Now.AddDays(-7),
                            IsActive = true
                        },
                        new Like
                        {
                            ArticleId = article2.Id,
                            UserId = yazarUser.Id,
                            CreatedAt = DateTime.Now.AddDays(-6),
                            IsActive = true
                        },
                        new Like
                        {
                            ArticleId = article3.Id,
                            UserId = uye.Id,
                            CreatedAt = DateTime.Now.AddDays(-4),
                            IsActive = true
                        },
                        new Like
                        {
                            ArticleId = article5.Id,
                            UserId = uye.Id,
                            CreatedAt = DateTime.Now.AddDays(-1),
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                    
                    // Add notifications
                    context.Notifications.AddRange(
                        new Notification
                        {
                            UserId = adminUser.Id,
                            Type = NotificationType.ArticlePublished,
                            Message = "Makaleniz başarıyla yayınlandı: ASP.NET Core ile Web Geliştirme",
                            RedirectUrl = "/Article/Details/asp-net-core-ile-web-gelistirme",
                            CreatedAt = DateTime.Now.AddDays(-10),
                            IsRead = true,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = editorUser.Id,
                            Type = NotificationType.ArticlePublished,
                            Message = "Makaleniz başarıyla yayınlandı: Sağlıklı Beslenme İpuçları",
                            RedirectUrl = "/Article/Details/saglikli-beslenme-ipuclari",
                            CreatedAt = DateTime.Now.AddDays(-8),
                            IsRead = true,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = adminUser.Id,
                            Type = NotificationType.NewComment,
                            Message = "Makalenize yeni bir yorum yapıldı: ASP.NET Core ile Web Geliştirme",
                            RedirectUrl = "/Article/Details/asp-net-core-ile-web-gelistirme",
                            CreatedAt = DateTime.Now.AddDays(-9),
                            IsRead = false,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = yazarUser.Id,
                            Type = NotificationType.ArticlePublished,
                            Message = "Makaleniz başarıyla yayınlandı: Ev Çalışma Ortamını Düzenleme",
                            RedirectUrl = "/Article/Details/ev-calisma-ortamini-duzenleme",
                            CreatedAt = DateTime.Now.AddDays(-5),
                            IsRead = false,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = adminUser.Id,
                            Type = NotificationType.ArticleLiked,
                            Message = "Makaleniz beğenildi: ASP.NET Core ile Web Geliştirme",
                            RedirectUrl = "/Article/Details/asp-net-core-ile-web-gelistirme",
                            CreatedAt = DateTime.Now.AddDays(-8),
                            IsRead = false,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = adminUser.Id,
                            Type = NotificationType.NewComment,
                            Message = "Makalenize yeni bir yorum yapıldı: Mobil Uygulama Geliştirmeye Giriş",
                            RedirectUrl = "/Article/Details/mobil-uygulama-gelistirmeye-giris",
                            CreatedAt = DateTime.Now.AddDays(-1),
                            IsRead = false,
                            IsActive = true
                        },
                        new Notification
                        {
                            UserId = yazarUser.Id,
                            Type = NotificationType.ArticleLiked,
                            Message = "Makaleniz beğenildi: Mobil Uygulama Geliştirmeye Giriş",
                            RedirectUrl = "/Article/Details/mobil-uygulama-gelistirmeye-giris",
                            CreatedAt = DateTime.Now.AddDays(-1),
                            IsRead = false,
                            IsActive = true
                        }
                    );
                    
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}