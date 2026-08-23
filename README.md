Shipment Tracking System

Bu proje, gönderilerin daha düzenli takip edilebilmesi için geliştirilmiş bir .NET Web API projesidir.

Sistem ile:

Yeni gönderi oluşturulabilir.
Otomatik takip numarası üretilir.
Gönderiler listelenebilir ve takip numarasıyla aranabilir.
Gönderi durumları belirlenen kurallara göre değiştirilebilir.
Durum değişikliklerinin geçmişi tutulur.
Müşteri takip numarasıyla gönderisini görüntüleyebilir.
Mimari

Projede Clean Architecture kullanılmıştır.

Katmanlar:

Domain
Application
Infrastructure
API
Tests

Gönderi durum geçişleri gibi temel iş kuralları Domain katmanında tutulmaktadır. EF Core ve veritabanı işlemleri Infrastructure katmanındadır.

Durum Akışı

Normal akış:

Preparing → Shipped → InTransit → OutForDelivery → Delivered

Teslim edilemezse:

OutForDelivery → DeliveryFailed → OutForDelivery

İptal:

Preparing → Cancelled

İade:

Delivered → ReturnRequested → Returning → Returned

Geçersiz durum geçişleri sistem tarafından reddedilir.

Çalıştırma
dotnet restore
dotnet build
dotnet run --project src/ShipmentTracking.API

API çalıştıktan sonra Swagger üzerinden endpointler test edilebilir.

Testleri çalıştırmak için:

dotnet test
Proje Kanıtları

Swagger ve test ekran görüntüleri:

docs/kanit/

klasöründe milestone bazında bulunmaktadır.

Proje üç milestone halinde tamamlanmıştır:

v1 – Gönderi oluşturma ve görüntüleme
v2 – Durum yönetimi
v3 – Müşteri takibi ve proje tamamlanması