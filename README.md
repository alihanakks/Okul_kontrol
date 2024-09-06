# Okul_sistemi
 Okul_sistemi projesinde bir okulun öğrenci ve personellerin kayıt, güncelleme, silme işlemlerinin yapıldığı, ders düzenlemelerinin  yapıla bildiği ve öğrencilerin ders programını ve derslerini görebildiği bir projedir. Uygulamaya girerken TC kimlik kartı bilgilerini ve şifresini kullanılabilir. 

## Giriş
 Bu projede .net 4.8 kullanarak Visual Basic kullanılarak hazırlanmıştır.İçerisinde Giriş, Personel, Öğrenci şeklinde 3 farklı konuda sayfa bulunmaktadır. Ek olarak MYSql veri tabanı kullanılarak veri tabanı bağlantısı eklenmiştir, veri tabanı bilgileri aşağıda bulunmaktadır.     

### Gereksinimler
Visual basic 
MYSql veri tabanı (tablo oluşturma kodları) 
```
CREATE TABLE [dbo].[D_programı] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [s_id] INT           NULL,
    [d_id] INT           NULL,
    [gün]  NVARCHAR (50) NULL,
    [saat] NVARCHAR (50) NULL,
    CONSTRAINT [PK_D_programı] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_D_programı_Ders] FOREIGN KEY ([d_id]) REFERENCES [dbo].[Ders] ([id]),
    CONSTRAINT [FK_D_programı_Sınıf] FOREIGN KEY ([s_id]) REFERENCES [dbo].[Sınıf] ([id])
);
```
```
a
```
