# Fullstack Clean Template

A full-stack template built with Clean Architecture principles, designed as a reusable foundation for other projects.

## Stack

- **API** — ASP.NET Core
- **ORM** — Entity Framework Core 10
- **Auth** — ASP.NET Identity
- **CQRS** — MediatR

## Structure

Domain
Application
Infrastructure
API

## Features
- Auth — register and login via ASP.NET Identity+ full crud for user 
- Todo — full CRUD example to demonstrate the CQRS workflow end to end

  
## Database Migrations

Navigate to the solution root first:
**Add Migration:**
```bash
dotnet ef migrations add InitialCreate `
  --project Persistence/Persistence.csproj `
  --startup-project Api/Api.csproj `
```

**Apply Migration:**
```bash
dotnet ef database update `
  --project Persistence/Persistence.csproj `
  --startup-project Api/Api.csproj
```

> Make sure your connection string in `API/appsettings.json` is configured before running the update.
