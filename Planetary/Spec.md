Planetary Project - System Specification Document
1. Introduction
The Planetary Project is a .NET 8 based system designed to manage and catalog celestial bodies, with a focus on planets. It follows a clean architecture pattern with domain-driven design principles, separating concerns into distinct layers.
2. System Architecture
2.1 Overall Architecture
The system follows a layered architecture:
1.	Domain Layer (Planetary.Domain): Contains business entities, domain logic, and core business rules.
2.	Application Layer (Planetary.Application): Contains application services, interfaces, DTOs, and command/query handlers following CQRS pattern with MediatR.
3.	Infrastructure Layer (Planetary.Infrastructure): Provides implementations for persistence, external services, and cross-cutting concerns.
4.	Web API Layer (Planetary.WebApi): Exposes the functionality through REST API endpoints.
2.2 Technology Stack
•	.NET 8.0
•	C# 12.0
•	Entity Framework Core 9.0 for data access
•	MediatR for CQRS implementation
•	ASP.NET Core for API endpoints
•	SQL Server for data persistence
•	BCrypt.Net for password hashing
3. Domain Model
3.1 Core Entities
3.1.1 Planet
The central entity representing celestial bodies with properties like:
•	Physical properties: Mass, Radius, Surface temperature
•	Orbital data: Distance from Earth
•	Atmospheric details: Composition, Pressure
•	Water data: Coverage percentage
•	Classification data: Planet type
•	Discovery metadata: Discovery date, Discoverer
3.1.2 Criteria
Represents evaluation criteria for planets with:
•	Name and Description
•	Category classification
•	Threshold values (minimum and maximum)
•	Measurement unit
•	Weight (importance factor)
•	Required flag
3.1.3 PlanetCriteria
A junction entity connecting planets with evaluation criteria:
•	Links a Planet to a Criteria
•	Contains evaluation data: Value, Score, Met status
•	Notes for additional information
•	Evaluation timestamp
3.1.4 User
Represents system users who can manage planets:
•	Authentication credentials
•	Profile information
•	Authorization level
3.2 Domain Rules
•	Planets must have a unique identifier
•	Each planet has zero or more evaluation criteria
•	Criteria can be added or removed from planets
•	Planets belong to users who created them
•	Evaluation of planets against criteria produces scores and met/not-met status
4. Application Layer
4.1 Commands
4.1.1 UpdatePlanetCommand
Updates a planet's details and associated criteria:
•	Updates basic planet information
•	Clears existing criteria associations
•	Adds new criteria associations from the provided list
•	Returns the updated planet entity
4.2 Queries
•	Get all planets
•	Get planet by ID
•	Get planets by user ID
•	Get criteria details
4.3 DTOs
•	PlanetDto: Basic planet information
•	PlanetWithCriteriaDto: Complete planet information with criteria details
•	PlanetCriteriaDto: Information about a planet's evaluation against criteria
5. Infrastructure Layer
5.1 Repositories
5.1.1 PlanetRepository
Provides CRUD operations for planets:
•	GetAllAsync: Retrieves all planets with their criteria
•	GetByIdAsync: Retrieves a planet by ID with its criteria
•	AddAsync/UpdateAsync: Persists planet changes
•	AddCriteria: Adds criteria relationships directly to the database
5.1.2 CriteriaRepository
Manages criteria entities:
•	Standard CRUD operations for criteria
5.2 Data Context
A central DbContext using Entity Framework Core that:
•	Defines entity mappings
•	Manages relationships between entities
•	Handles database transactions
6. API Layer
6.1 Controllers
6.1.1 PlanetController
Exposes endpoints for planet management:
•	GET: Retrieve planet information
•	PUT: Update planet details and criteria
•	Other CRUD operations
6.1.2 CriteriaController
Manages evaluation criteria:
•	Standard CRUD operations for criteria
6.2 Authentication and Authorization
•	JWT-based authentication
•	Role-based authorization with policies:
•	Viewer: Read-only access
•	EditPlanet: Can modify planet data
•	EditCriteria: Can modify criteria definitions
7. Key Workflows
7.1 Planet Update with Criteria
1.	Client sends a PUT request with planet details and criteria list
2.	Server validates the request
3.	Planet basic information is updated
4.	Existing criteria relationships are cleared
5.	New criteria relationships are created in the database
6.	Updated planet is returned to the client
7.2 Criteria-based Planet Evaluation
1.	Criteria are defined with thresholds
2.	Planets are evaluated against criteria
3.	Scores and met/not-met statuses are recorded
4.	Evaluation results can be retrieved with planet data
8. Technical Considerations
8.1 Entity Relationships
•	One-to-many relationship between User and Planet
•	Many-to-many relationship between Planet and Criteria through PlanetCriteria
8.2 Data Persistence Strategy
•	Entity Framework Core with SQL Server
•	Navigation properties for efficient loading of related data
•	Eager loading of related entities where appropriate
8.3 Performance Considerations
•	Includes are used to optimize database queries
•	Separate operations for removing and adding criteria to improve performance
•	Direct database operations for bulk criteria operations
9. Future Enhancements
•	Implementation of caching for frequently accessed data
•	Pagination for large result sets
•	Advanced search and filtering capabilities
•	Performance optimizations for criteria evaluation
•	Support for additional celestial body types beyond planets