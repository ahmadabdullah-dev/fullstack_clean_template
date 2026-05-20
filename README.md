# Fullstack Clean Template

A full-stack template built with Clean Architecture principles, designed as a reusable foundation for other projects with Cookies based authentication.

## Stack

- **API** — ASP.NET Core
- **ORM** — Entity Framework Core 10
- **Auth** — ASP.NET Identity
- **CQRS** — MediatR
- **Validator** — FluentValidation

## Structure

- Domain
- Application
- Infrastructure
- API

## Features
- Auth — register and login via ASP.NET Identity+ full crud for user 
- Todo — full CRUD example to demonstrate the CQRS workflow end to end
- Result<T> pattern — returns function results as Success(T value) or Failure(string error, int code)
- PagedList<T> and pagination patterns — handle incoming queries with page number and page size
  
  ---
  
## API Endpoints

### Auth
- `POST /api/Auth/register` — Register a new user
- `POST /api/Auth/login` — Login
- `POST /api/Auth/logout` — Logout

### Todo
- `POST /api/Todo` — Create a todo
- `GET /api/Todo` — List todos *(query: `StartDate`, `Page`, `PageSize`)*
- `GET /api/Todo/todo/{id}` — Get todo by ID
- `PATCH /api/Todo/{id}` — Update todo
- `DELETE /api/Todo/{id}` — Delete todo
- `PATCH /api/Todo/{id}/complete` — Mark todo as complete

### User
- `GET /api/User/{id}` — Get user by ID
- `PUT /api/User` — Update user
- `DELETE /api/User` — Delete user

---

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
