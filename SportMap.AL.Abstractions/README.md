# Application Layer Abstractions (AL.Abstractions)

## Purpose
Define the public-facing **interfaces** of the Application Layer.  
These abstractions are implemented in the Application Layer (AL) and consumed by upper layers (e.g., Presentation Layer).

This ensures Presentation Layer depends only on **interfaces**, not on the concrete implementation details of AL.

## Responsibilities
- Describe application-level operations as interfaces.
- Provide contracts that Application Layer must implement.
- Expose interfaces for handling use cases (e.g., IUserAppService).
- Allow PL to use AL functionality without direct reference to AL implementation.
- Enable clean dependency inversion between PL → AL.

## Allowed to Contain
- **Application service interfaces**  
  Examples:  
  - `IUserAppService`  
  - `IAuthAppService`  
  - `IOrderAppService`
- **Command/query contracts** (if they need to be exposed outside AL)
- **Small data contracts required by AL APIs**  
  (e.g., request/response models used only at the AL boundary)
- **Interfaces for cross-layer access** (e.g., notifications triggered by AL)

## Forbidden
- Implementations of any interface.
- Business logic or workflow logic.
- Domain entities (should exist only in Domain layer).
- Repository interfaces (belong in DAL.Abstractions).
- EF Core or any persistence logic.
- DTOs intended only for API → AL communication.
- Controllers or presentation logic.

## Notes
- AL implements these interfaces.
- PL depends on AL.Abstractions, NOT on AL directly.
- AL.Abstractions must be stable, containing only public-facing AL contracts.
- AL must remain swappable or mockable using AL.Abstractions.