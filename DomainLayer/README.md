# Domain Layer

## Purpose
Represent business concepts and rules that define the core of the system. Independent of all other layers.

## Responsibilities
- Define entities, value objects, and invariants.
- Express the core business model.
- Provide domain rules and domain events.

## Allowed to Contain
- Entities.
- Value objects.
- Domain events.
- Enums representing business meaning.
- Pure domain services (no external dependencies).

## Forbidden
- Dependencies on AL, BLL, or DAL.
- Infrastructure or EF Core code.
- Repositories.
- DTOs or use-case logic.