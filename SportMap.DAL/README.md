# Data Access Layer (DAL)

## Purpose
Implement persistence logic using EF Core or other storage technologies. Serve AL when it requires data access.

## Responsibilities
- Implement interfaces from DAL.Abstractions.
- Provide EF Core DbContext.
- Handle database queries and persistence.
- Configure EF Core mappings and migrations.

## Allowed to Contain
- EF Core DbContext.
- EF Core configurations.
- Repository implementations.
- Migrations.
- SQL scripts or stored-procedure logic.
- Infrastructure services (e.g., Redis, file storage).

## Forbidden
- Business rules.
- BLL logic.
- Application orchestration.
- API or presentation logic.