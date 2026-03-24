# Business Logic Layer (BLL)

## Purpose
Hold the core business logic and rules. Perform high-level domain operations that enforce business correctness.

## Responsibilities
- Contain business workflows and business rules.
- Manipulate Domain entities.
- Provide reusable business operations for AL.
- Validate business invariants.

## Allowed to Contain
- Business services(UserManager, OrderProcessor)
- Domain-oriented operations.
- Domain service implementations that do not require DAL.
- Business workflow classes
- Domain entities (used, not owned)
- Domain value objects
- Rule evaluators (e.g., PriceCalculator, EligibilityChecker)
- Complex business operations

## Forbidden
- Direct access to DAL or repositories.
- EF Core or database logic.
- Application orchestration (belongs to AL).
- API or presentation logic.