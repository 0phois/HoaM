# HoaM.Domain

Welcome to the `HoaM.Domain` project, the core domain layer of the HoaM (Homeowners Association Management) framework. This project is structured around domain features, each encapsulating the necessary entities, value objects, and domain logic.

## Overview

The `HoaM.Domain` project is designed following the principles of Domain-Driven Design (DDD). It serves as the foundation upon which the framework's functionality is built, ensuring that the business logic is well encapsulated and separated from the application's other concerns.

### Structure

- **EntityBase Class Hierarchy**: Provides a base class for all entities in the domain, ensuring consistent behavior and properties.
- **Repository Interfaces**: Defines the contracts for repository implementations that handle data persistence.
- **Domain Events**: Implements the mechanism for domain event handling, allowing for decoupled communication between different parts of the application.
- **Value Objects**: Encapsulates value object logic and behaviors, ensuring they are immutable and properly validated.
- **Result Objects**: Standardizes the way methods return results, including success or failure information.

### Features
The project is organized by feature, each representing a distinct area of the homeowners association management domain. This organization aligns with feature-based development practices, ensuring that all related domain classes and logic are co-located.
Features include:

- **Article**: Handles the creation and management of articles.
- **AuditLog**: Provides a system for tracking changes and operations.
- **Committee**: Represents committees within the association.
- **Community**: Encapsulates the community aspects of the association.
- **Document**: Manages documents related to the community/association.
- **Event**: Handles event scheduling and information.
- **Meeting**: Manages meeting details and records.  
    - *Meeting Minutes*: Keeps a record of the meeting discussions.  
    - *ActionItem*: Manages tasks assigned during the meeting.
- **Member**: Represents the members of the association.
- **Notifications**: Manages the sending and receiving of notifications.
- **Property**: Represents properties within the association.
- **Transactions**: Handles financial transactions.

Each feature directory contains all the necessary domain classes, including entities, value objects, and any domain events.  

Explore the features within the Features directory to understand the domain model and how the various aspects of the homeowners association are represented.
