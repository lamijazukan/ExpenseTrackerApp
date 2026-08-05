# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**ExpenseTrackerApp** is an ASP.NET Core 8.0 Web API for personal expense tracking. The frontend lives in a separate repo: https://github.com/lamijazukan/ExpenseTrackerAppUI (React/Vite, runs on port 5173).

## Commands

### Running with Docker (recommended)

```bash
# Create /docker/compose/.env with:
# POSTGRES_DB=ExpenseTracker
# POSTGRES_USER=postgres
# POSTGRES_PASSWORD=postgres

cd docker/compose
docker-compose up --build   # start API + PostgreSQL
docker-compose down          # stop
```

API: http://localhost:8080 | Swagger: http://localhost:8080/swagger/index.html

### Running locally (dotnet CLI)

```bash
cd ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi
dotnet build
dotnet run
```

### EF Core Migrations

Run from the `ExpenseTrackerApp.Infrastructure` project directory:

```bash
dotnet ef migrations add <MigrationName> --startup-project ../ExpenseTrackerApp.WebApi
dotnet ef database update --startup-project ../ExpenseTrackerApp.WebApi
```

Migrations are applied automatically on startup via `db.Database.Migrate()` in `Program.cs`. The database is also seeded with demo data (`demo@demo.com / Demo123!`, `test@test.com / Test123!`).

## Architecture

Clean Architecture with 5 layers:

```
Contracts   →  Request/Response DTOs (no domain types leak out)
Domain      →  Entities, value objects, domain errors, enums, domain events
Application →  Use-case services, business logic, repository interfaces
Infrastructure → EF Core, repositories, JWT generation, domain event dispatcher
WebApi      →  Controllers, DI wiring, middleware, Swagger
```

**Dependency direction:** WebApi → Application → Domain; Infrastructure implements Application interfaces.

### Error handling

The codebase uses `ErrorOr<T>` for functional error propagation. Domain errors are defined in `Domain/Errors/`. Controllers call `.Match(onValue, onError)` — avoid throwing exceptions for business rule failures.

### Controllers

All controllers inherit `ApiControllerBase` and are versioned under `/api/v1/`. Routes follow `/api/v1/{resource}`. The `[Authorize]` attribute is applied at controller level; `/health` and `/errors` are unprotected.

### Dependency injection

Each layer registers its own services via extension methods (e.g., `AddUsersApplication()`, `AddInfrastructure()`). New services belong in the extension method of the layer that owns them, not in `Program.cs` directly.

### Database

PostgreSQL 16 via EF Core 8. `AppDbContext` (sealed) in `Infrastructure/Database/` auto-loads entity configurations from the assembly. Entity relationships: `User → Category (parent/child hierarchy) → Budget, Expense → Transaction`; `User → UserProfile`.

### Authentication

JWT Bearer tokens. Config lives in `appsettings.json` under `JwtSettings` (Key, Issuer, Audience). Development key is hardcoded in `appsettings.development.json`.

## Key Configuration Files

| File | Purpose |
|------|---------|
| `ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi/appsettings.json` | Serilog, JWT, connection string |
| `ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi/appsettings.development.json` | Local dev overrides |
| `docker/compose/docker-compose.yml` | Service orchestration |
| `docker/compose/.env` | DB credentials (not committed) |
| `Directory.Build.props` | Global: .NET 8, nullable enabled, implicit usings |

## API Documentation

Full endpoint reference is in `ExpenseTrackerApp/docs/API_DOCS.md`. Swagger UI is available at runtime and requires a JWT Bearer token (obtained from `/api/v1/auth/login`) to test protected endpoints.
