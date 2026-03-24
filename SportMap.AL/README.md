# Application Layer (AL)

## Purpose
Coordinate system operations. Serve as the central layer that communicates with both the BLL and DAL.

## Responsibilities
- Execute system use cases.
- Coordinate calls to BLL services.
- Directly call DAL (via DAL.Abstractions) when needed.
- Map between API DTOs and Domain models.
- Handle application-level validation and transaction control.
- Orchestrate multi-step processes.

## Allowed to Contain
- Application services.
- DTOs (commands, queries, response models).
- Handlers (command/query handlers).
- Mappers.
- Use case orchestration logic.
- Calls to both BLL and DAL abstractions.

## Forbidden
- Pure business rules (should be in BLL).
- EF Core or concrete DAL implementations.
- API controllers (PL concern).