# Record Collection API

RESTful API built with .NET, Dapper, and PostgreSQL for managing a personal record collection.

## Features

- Get all records
- Get record by ID
- Add new records
- PostgreSQL database integration
- Lightweight data access using Dapper
- Dockerized development environment

## Tech Stack

- .NET Web API
- Dapper
- PostgreSQL
- Docker

## Endpoints

| Method | Endpoint                          | Description            |
|--------|----------------------------------|------------------------|
| GET    | /api/recordcollection            | Get all records        |
| GET    | /api/recordcollection/{id}       | Get record by ID       |
| POST   | /api/recordcollection            | Create new record      |

## Getting Started

### Prerequisites
- Docker
- .NET SDK

### Run locally

```bash
docker-compose up --build
