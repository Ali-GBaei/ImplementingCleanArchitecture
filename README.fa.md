# فروشگاه ساده با معماری کلین (ASP.NET Core MVC)

این پروژه یک نمونه ساده ولی اصولی برای پیاده‌سازی **Clean Architecture** در ASP.NET Core MVC است تا بتوانید با خیال راحت در GitHub قرار دهید.

---

## معرفی سریع

هدف پروژه:

- موضوع ساده: مدیریت محصولات فروشگاه (CRUD)
- ساختار حرفه‌ای: پیاده‌سازی کامل لایه‌های مهم Clean Architecture
- آماده ارائه در GitHub با مستندات کامل فارسی و انگلیسی

---

## لایه‌های معماری (Clean Architecture)

### 1) Domain (`StoreWebsite.Domain`)

- شامل هسته دامنه و قوانین کسب‌وکار
- بدون وابستگی به EF Core، MVC یا هر فریم‌ورک خارجی
- شامل:
  - Entity اصلی: `Product`
  - قراردادها: `IProductRepository` و `IUnitOfWork`

### 2) Application (`StoreWebsite.Application`)

- شامل Use Caseها و منطق اپلیکیشن
- فقط به Domain وابسته است
- شامل:
  - DTOها (`ProductDto`, `ProductInputDto`)
  - سرویس Use Case: `IProductUseCaseService` و پیاده‌سازی آن
  - مدیریت نتیجه عملیات با `OperationResult`

### 3) Infrastructure (`StoreWebsite.Infrastructure`)

- شامل دسترسی به دیتابیس و پیاده‌سازی قراردادها
- شامل:
  - `ApplicationDbContext`
  - پیکربندی Entityها
  - `ProductRepository`
  - `UnitOfWork`
  - `DatabaseInitializer` برای ساخت DB و Seed اولیه

### 4) Presentation (`StoreWebsite.Web`)

- رابط کاربری با ASP.NET Core MVC
- شامل:
  - `ProductsController`
  - ViewModelها
  - Viewهای CRUD
- این لایه فقط با Application کار می‌کند و مستقیم به EF Core وصل نیست

---

## الگوها و اصول مهمی که رعایت شده

- قانون وابستگی Clean Architecture (Dependency Rule)
- Repository Pattern
- Unit of Work Pattern
- Dependency Injection
- جداسازی DTO از ViewModel
- اعتبارسنجی دامنه داخل Entity
- جداسازی کامل زیرساخت (Persistence Isolation)

---

## تکنولوژی‌ها

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server
- Bootstrap (ظاهر تمیز و ساده)

---

## ساختار پروژه

```text
src/
  StoreWebsite.Domain/
  StoreWebsite.Application/
  StoreWebsite.Infrastructure/
  StoreWebsite.Web/
StoreWebsiteCleanArchitecture.slnx
README.md
README.fa.md
```

---

## تنظیم دیتابیس

کانکشن‌استرینگ طبق درخواست شما تنظیم شده است در:

- `src/StoreWebsite.Web/appsettings.json`

مقدار:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=ServerName;Initial Catalog=CleanDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
}
```

در شروع برنامه:

- مایگریشن‌ها با `MigrateAsync()` در شروع برنامه اعمال می‌شوند
- اگر جدول خالی باشد، چند محصول نمونه seed می‌شود

---

## نحوه اجرا

### 1) Build

```bash
dotnet restore
dotnet build StoreWebsiteCleanArchitecture.slnx
```

### 2) Run

```bash
dotnet run --project src/StoreWebsite.Web
```

### 3) مشاهده

- در مرورگر به آدرس نمایش‌داده‌شده در ترمینال بروید (`https://localhost:xxxx`)

---

## بخش‌های پیاده‌سازی‌شده

### مدیریت محصولات (CRUD)

- نمایش لیست محصولات
- افزودن محصول
- ویرایش محصول
- حذف محصول
- اعتبارسنجی ورودی‌ها در UI + Domain
- پیام‌های موفقیت/خطا برای تجربه کاربری بهتر

---

## پکیج‌های نصب‌شده

- `Microsoft.EntityFrameworkCore` نسخه `8.0.12`
- `Microsoft.EntityFrameworkCore.SqlServer` نسخه `8.0.12`
- `Microsoft.EntityFrameworkCore.Design` نسخه `8.0.12`
- `Microsoft.Extensions.DependencyInjection.Abstractions` نسخه `8.0.2`

---

## نکات برای ارائه در GitHub

این پروژه برای نمایش نمونه کار مناسب است چون:

- موضوع ساده و قابل فهم دارد
- معماری تمیز و لایه‌بندی استاندارد دارد
- قابلیت توسعه برای سناریوهای واقعی را دارد
- مستندات دوزبانه کامل دارد

---
