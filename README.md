# Fullstack Clean Template

A full-stack template built with Clean Architecture principles, designed as a reusable foundation for other projects.

## Stack

- **UI** — Blazor + React
- **API** — ASP.NET Core
- **ORM** — Entity Framework Core 10
- **Auth** — ASP.NET Identity
- **CQRS** — MediatR

## Structure

Domain
Application
Infrastructure
API
UI => connects to API via HTTP

## Features
- Auth — register and login via ASP.NET Identity+ full crud for user 
- Todo — full CRUD example to demonstrate the CQRS workflow end to end

## UI
- Login and register pages with cookie-based authentication
- Navbar with theme toggle and drawer
  
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
