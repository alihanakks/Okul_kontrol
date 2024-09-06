# Okul_sistemi
 Okul Sistemi projesi, bir okulun öğrenci ve personelin  kayıt, güncelleme, silme işlemlerinin yapıldığı, ders düzenlemelerinin yapılabildiği ve öğrencilerin ders programını ve derslerini görebildiği bir projedir. Kullanıcılar TC kimlik kartı bilgileri ve şifreleriyle sisteme giriş yapabilirler. Projenin videosu için (buraya)[https://www.youtube.com/] göz atabilirsiniz.
 
## Giriş
 Bu projede .NET 4.8 kullanılarak Visual Basic ile hazırlanmıştır. İçerisinde Giriş, Personel ve Öğrenci olmak üzere 3 farklı konuda sayfa bulunmaktadır. Ayrıca MySQL veritabanı kullanılarak veritabanı bağlantısı eklenmiştir. Veritabanı bilgileri aşağıda bulunmaktadır.     

### Gereksinimler

[Visual Basic](https://learn.microsoft.com/en-us/dotnet/visual-basic/)

[Guna.UI2.WinForms](https://www.nuget.org/packages/Guna.UI2.WinForms/2.0.4.6?_src=template)

[MYSql](https://www.w3schools.com/MySQL/default.asp)

[C#](https://www.w3schools.com/cs/index.php)

### Kurulum

#### NuGet Paket Yöneticisi Kullanarak Eklemek:

Visual Studio’da projenizi açın.

Solution Explorer’da projenizin üzerine sağ tıklayın ve “Manage NuGet Packages” seçeneğini seçin.

Açılan pencerede “Browse” sekmesine geçin.

Arama kutusuna Guna.UI2.WinForms yazın ve arayın.

paketi seçin ve “Install” butonuna tıklayarak projenize ekleyin.

### MYSql Tablo Oluşturma

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
CREATE TABLE [dbo].[Ders] (
    [id]    INT           IDENTITY (1, 1) NOT NULL,
    [d_adı] NVARCHAR (50) NULL,
    [p_id]  INT           NULL,
    CONSTRAINT [PK_Ders] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Ders_Personel] FOREIGN KEY ([p_id]) REFERENCES [dbo].[Personel] ([id])
);
```
```
CREATE TABLE [dbo].[Ogrenci] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [isim]          NVARCHAR (50)  NULL,
    [soyisim]       NVARCHAR (50)  NULL,
    [veli_ismi]     NVARCHAR (50)  NULL,
    [tc]            NVARCHAR (11)  NULL,
    [s_id]          INT            NULL,
    [adress]        NVARCHAR (250) NULL,
    [sifre]         NVARCHAR (25)  NULL,
    [veli_iletisim] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Ogrenci] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Ogrenci_Sınıf1] FOREIGN KEY ([s_id]) REFERENCES [dbo].[Sınıf] ([id])
);
```
```
CREATE TABLE [dbo].[Personel] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [tc]       NVARCHAR (11) NULL,
    [isim]     NVARCHAR (50) NULL,
    [soyisim]  NVARCHAR (50) NULL,
    [rütbe_id] INT           NULL,
    [alan]     NVARCHAR (50) NULL,
    [sifre]    NVARCHAR (25) NULL,
    [iletisim] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Personel] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Personel_Rutbe1] FOREIGN KEY ([rütbe_id]) REFERENCES [dbo].[Rutbe] ([id])
);
```
```
CREATE TABLE [dbo].[Rutbe] (
    [id]    INT           IDENTITY (1, 1) NOT NULL,
    [rütbe] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Rutbe] PRIMARY KEY CLUSTERED ([id] ASC)
);

```
```
CREATE TABLE [dbo].[Sınıf] (
    [id]    INT           IDENTITY (1, 1) NOT NULL,
    [sınıf] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Sınıf] PRIMARY KEY CLUSTERED ([id] ASC)
);
```
p)
