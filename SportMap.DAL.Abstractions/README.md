# DAL.Abstractions

## Purpose
Define persistence interfaces used by AL.

## Responsibilities
- Provide repository and UnitOfWork interfaces for DAL.
- Abstract away persistence concerns.

## Allowed to Contain
- IRepository<T>.
- Entity-specific repository interfaces.
- DAL specific interfaces
- Persistence abstractions used by AL.

## Forbidden
- Concrete implementations.
- EF Core or SQL logic.
- BLL or Domain rules.
- Application or presentation DTOs.