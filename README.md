# Fullstack Clean Template

A full-stack template built with Clean Architecture principles, designed as a reusable foundation for other projects.

## Stack

- **API** — ASP.NET Core
- **ORM** — Entity Framework Core 10
- **Auth** — ASP.NET Identity
- **CQRS** — MediatR

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
  
  
# API EndPoints
## Auth

### Register
`POST /api/Auth/register`

#### Request Body
```json
{
  "userName": "string",
  "email": "string",
  "password": "string",
  "country": "string"
}
```

---

### Login
`POST /api/Auth/login`

#### Request Body
```json
{
  "email": "string",
  "password": "string"
}
```

---

### Logout
`POST /api/Auth/logout`

---

## Todo

### Create Todo
`POST /api/Todo`

#### Request Body
```json
{
  "title": "string"
}
```

---

### Get Todos
`GET /api/Todo`

#### Query Params
```txt
StartDate?: datetime
Page?: number
PageSize?: number
```
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
