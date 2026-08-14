Genel Plan
Projeyi  3 milestone  şeklinde yapmayı planlıyorum.
*** Milestone 1 – Gönderi Oluşturma ve Görüntüleme
İlk olarak projenin temel yapısını kuracağım:  
* .NET solution oluşturacağım.
* Clean Architecture katmanlarını oluşturacağım.
* Domain, Application, Infrastructure ve API projelerini ayıracağım.
* Shipment modelini oluşturacağım.
* Gönderi oluşturma işlemini yapacağım.
* Zorunlu alan kontrollerini ekleyeceğim.
* Her gönderi için otomatik takip numarası oluşturacağım.
* Repository yapısını kuracağım.
* EF Core bağlantısını yapacağım.
* Gönderileri veritabanına kaydedeceğim.
* Gönderileri listeleme işlemini yapacağım.
* Takip numarasıyla gönderi arama işlemini yapacağım.
* Swagger üzerinden işlemleri test edeceğim.
***Ne Zaman Bitmiş Sayacağım?
* Yeni bir gönderi oluşturabiliyorum.
* Alıcı adı, adres, telefon veya paket bilgisi eksikse sistem hata veriyor.
* Yeni gönderiye otomatik olarak takip numarası veriliyor.
* Gönderi veritabanına kaydediliyor.
* Kaydedilen gönderileri listeleyebiliyorum.
* Takip numarasıyla belirli bir gönderiyi bulabiliyorum.
* Olmayan bir takip numarası arandığında uygun hata dönüyor.
* Swagger üzerinden işlemlerin çalıştığını gösterebiliyorum.
* Milestone sonunda `v1` tag oluşturdum.
*** Tahmini Süre: 5 saatten fazla, hatalara göre değişebilir.
***Milestone 2 – Gönderi Durumlarının Yönetilmesi
Gönderinin hangi durumdan hangi duruma geçebileceğini kontrol edeceğim.
Kullanmayı planladığım durumlar:  Hazırlanıyor, Kargoya Verildi,  Yolda,  Kuryeye Çıktı, Teslim Edilemedi, Teslim Edildi,  İptal Edildi, İade Talebi Oluşturuldu,  İade Yolda, İade Tamamlandı
Normal akış:`Hazırlanıyor → Kargoya Verildi → Yolda → Kuryeye Çıktı → Teslim Edildi`
Teslim edilememe durumunda: `Kuryeye Çıktı → Teslim Edilemedi`
Daha sonra tekrar teslimat denenirse: `Teslim Edilemedi → Kuryeye Çıktı`
Gönderi daha yola çıkmadan iptal edilirse:`Hazırlanıyor → İptal Edildi`
Teslim edilmiş bir gönderi iade edilirse: `Teslim Edildi → İade Talebi Oluşturuldu → İade Yolda → İade Tamamlandı`
Ayrıca geçersiz durum değişikliklerini engelleyeceğim.
Bu kurallar controller içinde değil Domain katmanında olacak. Her durum değişikliğinde geçmiş kaydı da oluşturacağım. Ayrıca gönderileri durumlarına göre filtreleme özelliğini ekleyeceğim.
***Ne Zaman Bitmiş Sayacağım?
* Normal gönderi akışı çalışıyor.
* Gönderi teslim edilemedi durumuna alınabiliyor.
* Teslim edilemeyen gönderi tekrar kuryeye çıkarılabiliyor.
* Gönderi yola çıkmadan iptal edilebiliyor.
* Teslim edilmiş bir gönderi eski durumlara döndürülemiyor.
* İptal edilmiş gönderi tekrar kargoya verilemiyor.
* Teslim edilmiş gönderi için iade süreci başlatılabiliyor.
* Her durum değişikliğinde geçmiş kaydı oluşuyor.
* Gönderileri durumlarına göre listeleyebiliyorum.
* Önemli durum geçişlerini testlerle kontrol ettim.
* Swagger ekran görüntülerini `docs/kanit/` klasörüne ekledim.
* Milestone sonunda `v2` tag oluşturdum.
***Tahmini Süre: 6 – 8 saat arası

***Milestone 3 – Müşteri Takibi ve Projenin Tamamlanması
Bu bölümde:
* Müşterinin takip numarası ile gönderisini görüntülemesini sağlayacağım.
* Müşterinin gönderi durumunu değiştirememesini sağlayacağım.
* Durum geçmişini görüntüleme işlemini tamamlayacağım.
* Hata yönetimini düzenleyeceğim.
* Validation kontrollerini gözden geçireceğim.
* Gerekli testleri tamamlayacağım.
* README dosyasını hazırlayacağım.
* Swagger kanıtlarını tamamlayacağım.
* JOURNAL.md dosyasını kontrol edeceğim.
* `docs/kapanis.md` dosyasını hazırlayacağım.
***Ne Zaman Bitmiş Sayacağım?
* Müşteri takip numarasıyla gönderisini görebiliyor.
* Müşteri gönderi durumunu değiştiremiyor.
* Durum geçmişi görüntülenebiliyor.
* Hatalar uygun HTTP kodlarıyla dönüyor.
* Temel testler çalışıyor.
* Swagger kanıtları tamamlanmış.
* README dosyasında sistemin ne yaptığı ve nasıl çalıştırıldığı yazıyor.
* `docs/kapanis.md` hazırlanmış.
* Milestone sonunda `v3` tag oluşturulmuş.
***Tahmini Süre: 4 5 saat


