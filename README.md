# Store Website - Clean Architecture (ASP.NET Core MVC)

A simple but production-minded sample for **Clean Architecture** using ASP.NET Core MVC, Entity Framework Core, and SQL Server.

## Persian Documentation

- For full Persian guide, see [README.fa.md](README.fa.md)

---

## Architecture Overview

This solution follows layered Clean Architecture:

- **Domain Layer** (`StoreWebsite.Domain`)
  - Core business entities and rules.
  - No dependency on external frameworks.
  - Contains: `Product`, `IProductRepository`, `IUnitOfWork`.

- **Application Layer** (`StoreWebsite.Application`)
  - Use cases and application orchestration.
  - Depends only on Domain abstractions.
  - Contains DTOs and `IProductUseCaseService` + implementation.

- **Infrastructure Layer** (`StoreWebsite.Infrastructure`)
  - EF Core, SQL Server, repository implementations, `DbContext`.
  - Implements Domain contracts.
  - Registers infrastructure services via DI extension methods.

- **Presentation Layer** (`StoreWebsite.Web`)
  - ASP.NET Core MVC UI.
  - Controllers + Views + ViewModels.
  - Calls Application use cases (never directly talks to EF Core).

### Key Clean Architecture Practices Applied

- Dependency Rule (outer layers depend inward)
- Repository Pattern
- Unit of Work Pattern
- Dependency Injection composition root
- DTO boundary between UI and Use Cases
- Domain validation and encapsulation inside entity methods
- Persistence isolation in Infrastructure

---

## Tech Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server Provider for EF Core
- Bootstrap (default MVC template styling)

---

## Solution Structure

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

## Database Configuration

Connection string is already configured in `src/StoreWebsite.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=ALI\\ALINEWSQL;Initial Catalog=CleanDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
}
```

The application applies EF Core migrations at startup using `MigrateAsync()` and seeds sample products automatically when the table is empty.

---

## How to Run

1. Restore and build:

```bash
dotnet restore
dotnet build StoreWebsiteCleanArchitecture.slnx
```

2. Run web app:

```bash
dotnet run --project src/StoreWebsite.Web
```

3. Open browser:

- `https://localhost:xxxx` (port shown in terminal)

---

## Implemented Feature

### Product Management (CRUD)

- List products
- Create product
- Edit product
- Delete product
- Basic validation in both UI and domain
- User feedback with success/error alerts

---

## Required NuGet Packages

Installed packages include:

- `Microsoft.EntityFrameworkCore` (8.0.12)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.12)
- `Microsoft.EntityFrameworkCore.Design` (8.0.12)
- `Microsoft.Extensions.DependencyInjection.Abstractions` (8.0.2)

---

## Notes for GitHub Showcase

This project is intentionally simple in business scope, but complete in architecture style so it can be used as:

- Portfolio sample
- Starter template for layered ASP.NET MVC projects
- Reference implementation for Clean Architecture in small-to-medium apps

---

## License

Use freely for learning and portfolio purposes.
