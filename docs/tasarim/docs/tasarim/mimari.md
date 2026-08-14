M İ M A R İ
Bu proje için  Clean Architecture kullanmak istiyorum.  Kullanacağım katmanlar: Domain, Application,  Infrastructure, API, Tests.
Bu yapıyı seçmemin sebebi, gönderi durumları ile ilgili iş kurallarını veritabanı ve API kodlarından ayrı tutmak istemem.
Özellikle bu projede önemli olan kısım gönderinin hangi durumdan hangi duruma geçebileceği. Bu kurallar controller içinde yazılırsa proje büyüdükçe karışabilir. Bu yüzden kuralları domain katmanında tutmayı planlıyorum.
* Domain: İş kurallarını içerecek. Shipment, ShipmentStatus, ShipmentStatusHistory gibi yapışardan bahsediyorum. Gönderinin durumunun değişip değişemeyeceğine bu katman karar verecek. Örneğin teslim edilmiş bir gönderi tekrar Hazırlanıyor durumuna getirilmeyecek. Domain katmanı EF Core, ASP.NET Core veya başka bir dış teknolojiye bağlı olmayacak.
*Application: Bu katmanda uygulamanın yaptığı işlemler olacak. Örneğin gönderi oluştrkma gönderi listeleme takip numarasıyla gönderi bulma. Application katmanı veritabanının nasıl çalıştığını bilmeyecek. Veritabanı işlemleri için repository kullanacak.
*Infrastructure: Veritabanı gibi teknik işleri yapacak. EF Core veritabanı ayarları, DbContext gibi. Application kat katmanında kullanılan repository interface'lerinin gerçek implementasyonları burada olacak.
*API: Controller'lar isteği alacak ve gerekli Application işlemini çağıracak. Controller içinde gönderi durumları ile ilgili iş kuralları yazmamaya çalışacağım.
*Tests: Özellikle Domain katmanındaki iş kurallarını test edeceğim.
*****Neden Clean Architecture?
Gönderi durumlara kurallara bağlı olduğu için karışıklığa açık bir sistem.
İlk tasarımda şu geçişleri kullanmayı planlıyorum:
* Preparing → Shipped      * Shipped → InTransit    * InTransit → OutForDelivery  * OutForDelivery → Delivered    * OutForDelivery → DeliveryFailed     * DeliveryFailed → OutForDelivery    * Preparing → Cancelled           * Delivered → ReturnRequested      * ReturnRequested → Returning  * Returning → Returned
Bu geçişlerin dışında bir durum değişikliği yapılmaya çalışılırsa sistem bunu reddedecek.
Veritabanı işlemleri için repository yapısı kullanacağım.  EF Core ile çalışan gerçek repository ise Infrastructure katmanında olacak. Dependency Injection kullanarak interface ile gerçek implementasyonu birbirine bağlayacağım. Bu şekilde Application katmanı doğrudan EF Core'a bağlı olmayacak.
** Bu Mimarinin Bana Kazandırdığı Şeyler: İş kuralları tek yerde toplanacak,  veritabanı kodları domaine karışmayacak,  domain kurallarını test etmek daha kolay olacak, ileride veritabanı değişirse bütün projeyi değiştirmek gerekmeyecek.

**Bu Mimarinin Dezavantajları: Bu proje çok büyük olmadığı için Clean Architecture biraz fazla dosya ve klasör oluşturacak. Ama ödevde katman ayrımı ve iş kurallarının doğru yerde olması önemli olduğu için ve her şeyi daha rahat görmek açısından bu yapıyı kullanmayı tercih ettim.
**Sistem 10 Kat Büyürse Nerede Sorun Çıkabilir? Sistem büyürse en çok gönderi durumları kısmının karışabileceğini düşünüyorum. Şu an durum sayısı az ama ileride farklı teslimat tipleri,  farklı kargo firmaları, farklı kurallar eklenirse tek bir durum geçiş yapısı yeterli olmayabilir.
** Veritabanı Değişirse: Repository yapısı kullandığım için veritabanı değiştiğinde en çok Infrastructure katmanı etkilenecek.
Örneğin SQL Server yerine PostgreSQL kullanılmak istenirse Domain katmanındaki iş kurallarının değişmesine gerek kalmamasını hedefliyorum.
**Sistem İkiye Bölünürse:  İleride sistem büyüyüp gönderi takibi ve iade yönetimi iki farklı bölüme ayrılırsa mevcut yapı buna bir başlangıç sağlayabilir. Ama şu anda gönderi ve iade durumlarını aynı Shipment yapısı içinde tutacağım. Bu yüzden sistem ileride gerçekten iki ayrı servise bölünmek istenirse bu kısmı tekrar tasarlamak gerekebilir.
VARSAYIMLARIM: 
İlk aşamada  3 4 kişilik ekip kullanacağı için karmaşık yapıya gerek olmadığını düşünüyorum. İlk sürümde tek bir uygulama içinde katmanlı bir yapı kullanmanın yeterli olduğunu düşünüyorum.
