K A P S A M
Problem Tanımı: Bu projede gönderilerin Excel üzerinden takip ediliyor, hangi paketin nerede olduğu, teslim edilip edilmediği veya iade sürecine girip girmediği karışabiliyor; yerine daha düzenli bir sistem oluşturmak istiyorum.
Sistemi Kimler Kullanacak? Şirketteki ekip yani 3-4 kişi kullanacak. Ekiptekiler yeni gönderi oluşturabilecek, gönderi durumlarını değiltirebilecek ve değişen durumları yani geçmiş değişiklikleri görebilecek, gönderileri listeleyebilecek, takip numarasına göre gönderi arayabilecek. Gönderileri durumlarına göre filtreleyebilecek.
Müşteriler ise sadece takip numarası ile kendi gönderilerinin durumunu görebilecek. 
Gönderi Oluştururken Zorunlu Bilgiler: Alıcı adı, adres, telefon numarası, paket bilgisi. 
*E-posta adresi zorunlu olmayacak, isteğe bağlı.
*Bildirim çerez olayını ilk sürümde yapmayacağım.
Gönderi Durumları :Normal durumlarda: Hazırlanıyor → Kargoya Verildi → Yolda → Kuryeye Çıktı → Teslim Edildi.
Kurye paketi teslim edemezse: Kuryeye Çıktı → Teslim Edilemedi
Daha sonra tekrar teslimat şansı var: Teslim Edilemedi → Kuryeye Çıktı
Eğer gönderi daha yola çıkmadan iptal edilirse: Hazırlanıyor → İptal Edildi
VARSAYIYORUM teslim edilmiş kayıt sonradan değiştirilmeyecek, teslim edilen gönderi eski bir duruma geri alınmayacak.
İade Süreci: Teslim edilmiş bir paket iade edilmek istenirse teslim edilmiş bilgisi silinmeyecek. İade süreci olacak: Teslim Edildi → İade Talebi Oluşturuldu → İade Yolda → İade Tamamlandı..
Kapsam İçinde Olanlar (ilk sürüm için):  Gönderi oluşturma, otomatik takip numarası oluşturma, gönderileri listeleme, takip numarası ile gönderi arama, gönderi durumunu güncelleme, durum geçişlerini düzgün düzenlemek, gönderileri durumlarına göre filtreleme, durum değişikliklerinin geçmişini tutma, müşterinin takip numarasıyla gönderisini görüntülemesi, hatalı girişleri kontrol etme, temel iş kurallarını test etme
Kapsam Dışında Olanlar: Ödeme sistemi, fatura sistemi, gerçek kargo firması entegrasyonu, kargo takibi sistemi(gps gibi) ,SMS gönderme, e-posta gönderme, mobil uygulama, barkod veya QR kod sistem. Bunları kapsam dışında bırakmamın sebebi, ilk bakışta gönderi takibi ve durum geçişlerini düzgün şekilde tamamlamak istemem.
Temel İş Kuralları: Yeni oluşturulan bir gönderinin ilk durumu Hazırlanıyor olacak. Her gönderinin farklı takip numarası olacak. Teslim edilmiş bir gönderi eski durumlara geri alınamayacak. İptal edilmiş bir gönderi tekrar kargoya verilemeyecek. Gönderi sadece yola çıkmadan önce iptal edilebilecek. Teslim edilemeyen gönderi tekrar kuryeye çıkarılabilecek. Teslim edilen gönderinin iadesi ayrı bir süreç olacak. Her durum değişikliği geçmişe kaydedilecek. Geçersiz bir durum değişikliği yapılmaya çalışılırsa sistem buna izin vermeyecek. Müşteri sadece gönderiyi görüntüleyebilecek, durum değiştiremeyecek.
Projede bunlar düzgün çalışırsa bitti diyebilirim: Yeni gönderi oluşturulabiliyor. Gönderiye otomatik takip numarası veriliyor. Gönderiler listelenebiliyor. Takip numarası ile gönderi bulunabiliyor. Gönderiler durumlarına göre filtrelenebiliyor. Geçerli durum değişiklikleri yapılabiliyor. Geçersiz durum değişiklikleri engelleniyor. Teslim edilmiş gönderi eski duruma alınamıyor. İade süreci çalışıyor. Durum değişikliklerinin geçmişi tutuluyor. Müşteri takip numarası ile kendi gönderisini görebiliyor. Temel iş kuralları testlerle kontrol edilmiş oluyor.
Varsayımlarım::
Sistemi ilk başta sadece 3 – 4 kişilik ekip kullanacağı için çok detaylı bir yetkilendirme sistemi yapmayacağım.
Durum değişikliklerinde işlemi yapan kullanıcı, tarih bilgisi ve tam olarak ne yapıldığı sistemde tutulacak.
Takip numarasının nasıl üretileceği bana bırakıldığı için takip numarasını sistem otomatik ve benzersiz şekilde oluşturacak.
Müşteri sadece kendi paketini görmeli. Bu nedenle müşteri tarafında genel gönderi listesi olmayacak, sadece takip numarası ile sorgulama yapılacak. Her şeyi görebilen ekipteki çalışanlar olacak. 
