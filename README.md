# Record Collection API

RESTful API built with .NET, Dapper, and PostgreSQL for managing a personal record collection.

This project demonstrates building a clean, layered backend architecture with dependency injection, async data access, and Dockerized infrastructure.

The API provides endpoints for creating, retrieving, updating, and deleting records in a PostgreSQL database.

## Why I Built This

I built this project to demonstrate my backend development skills, specifically:

- Designing RESTful APIs in ASP.NET Core
- Using Dapper for lightweight data access
- Structuring applications with controllers, services, and interfaces
- Working with PostgreSQL in a containerized environment

The goal was to go beyond basic CRUD operations and implement patterns commonly used in production systems, including layered architecture, DTO-based design, and clean separation of concerns.

## Design Decisions

- Introduced DTOs to separate API contracts from domain models and prevent tight coupling
- Used a service layer to isolate application logic from HTTP concerns
- Chose Dapper over Entity Framework for greater control and performance
- Implemented extension methods for mapping to keep controllers clean and reduce repetition
- Used Docker to ensure consistent local database setup

## Key Features

- Full CRUD API with proper HTTP status codes
- Asynchronous database access using Dapper
- Layered architecture with separation of concerns
- DTO-based request/response design to isolate API contracts
- Input validation using data annotations
- Mapping between DTOs and domain models via extension methods
- Dockerized PostgreSQL database for local development

## Architecture

The project follows a layered structure:

- **Controllers**: Handle HTTP requests, model binding, and response formatting
- **Services**: Contain application logic and coordinate operations
- **Repositories**: Handle data access using Dapper
- **Models**: Represent domain entities
- **DTOs**: Define request and response contracts for the API

Dependency Injection is used throughout to decouple layers and improve testability.

## Tech Stack

- ASP.NET Core Web API
- Dapper
- PostgreSQL
- Docker

## Endpoints

| Method | Endpoint                         | Description         |
|--------|----------------------------------|---------------------|
| GET    | /api/recordcollection            | Get all records     |
| GET    | /api/recordcollection/{id}       | Get record by ID    |
| POST   | /api/recordcollection            | Create new record   |
| PUT    | /api/recordcollection/{id}       | Update record       |
| DELETE | /api/recordcollection/{id}       | Delete record       |

## Example Request

POST /api/recordcollection

```json
{
  "artistName": "Pink Floyd",
  "albumTitle": "The Dark Side of the Moon",
  "releaseYear": 1973,
  "discogsId": 857532321
}
```

## Example Response

```json
{
  "id": 1,
  "artistName": "Pink Floyd",
  "albumTitle": "The Dark Side of the Moon",
  "releaseYear": 1973,
  "discogsId": 857532321
}
```

### Prerequisites
- Docker
- .NET SDK

### Run locally

Start the database:

```bash
docker-compose up --build
```

Then run the API:
```bash
cd api
dotnet run
```

The API will be available at a local URL (e.g. http://localhost:5000).

The exact port will be displayed in the console when the application starts.

Once running, you can test the API using Postman, curl, or the included .http file.

## Future Improvements

- Add frontend UI (React)
- Implement authentication/authorization
- Add integration tests
- Implement global exception handling middleware and structured logging
- Add search, filtering, and sorting capabilities
