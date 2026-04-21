# VivaAssesment

This project is for my assignment/assessment at Viva.com

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Database](#database)
- [Configuration](#configuration)
- [Testing](#testing)

## Features

- **Countries API**: Fetches all countries with their capitals and borders from the REST Countries API with built-in caching
- **Integer Collection Processing**: Computes the second largest number from an integer collection

## Tech Stack

- **.NET**: .NET Core / ASP.NET Core (Latest version)
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Validation**: FluentValidation
- **Mediator Pattern**: MediatR
- **API Documentation**: Swagger/OpenAPI
- **Caching**: In-memory caching

## Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
API Layer
    ├── Controllers
    └── Validators

Core Layer
    ├── Domain Entities
    ├── DTOs
    |── Contracts
    |── Commands
    └── Queries

Infrastructure Layer
    ├── Database Context
    ├── Services
    ├── Migrations
    └── Options
```

## Project Structure

```
Integrated.VivaAssesment/
├── Api/                          # REST API endpoints
│   ├── Controllers/              # API controllers
│   ├── Validators/               # Request validation
│   ├── Program.cs                # Application entry point
│   └── appsettings.json          # Configuration

├── Core/                         # Domain logic
│   ├── Domain/                   # Entity definitions
│   ├── Contracts/                # Service interfaces
│   ├── Commands/                 # Command definitions
│   |── Queries/                  # Query definitions
│   └── Dtos/                     # Data transfer objects

├── Infrastructure/               # Data access layer
│   ├── DbContexts/               # EF Core context
│   ├── Services/                 # Infrastructure services
│   └── Migrations/               # Database migrations
```

## Getting Started

### Prerequisites

- **.NET SDK**: .NET 8.0
- **SQL Server**: SQL Server 2019 or higher
- **Visual Studio**: 2022 or higher (or VS Code)
- **Git**: For cloning the repository

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/VivaAssesment.git
   cd VivaAssesment
   ```

2. **Open the solution**
   ```bash
   cd Integrated.VivaAssesment
   ```

3. **Update the connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Your-SQL-Server-Connection-String"
   }
   ```

4. **Install dependencies**
   ```bash
   dotnet restore
   ```

5. **Apply database migrations**
   ```bash
   you can use the script I created from EF Core -> db/migration.sql and run it on you sql server for convenience
   ```

6. **Build the solution**
   ```bash
   dotnet build
   ```

7. **Run the application**
   ```bash
   dotnet run --project Api
   ```

The API will be available at the configured port

## API Endpoints

### 1. Get All Countries

Retrieves a list of all countries with their capitals and borders.

**Endpoint**: `GET /api/countries/getAllCountries`

**Description**: Fetches country data from the REST Countries API and caches it for 30 minutes for improved performance.

**Response**:
```json
{
  "countries": [
    {
      "name": "Country Name",
      "capital": "Capital City",
      "borders": ["Border1", "Border2"]
    }
  ]
}
```

**Status Codes**:
- `200 OK`: Successfully retrieved countries
- `500 Internal Server Error`: Server error occurred

---

### 2. Compute Second Largest Number

Finds the second largest number in an integer collection.

**Endpoint**: `POST /api/computeintegercollection/getSecondLargestNumber`

**Request Body**:
```json
{
  "requestArrayObj": [10, 5, 25, 15, 3, 8]
}
```

**Response**:
```json
{
  "secondLargestValue": 15
}
```

**Status Codes**:
- `200 OK`: Successfully computed the second largest number
- `400 Bad Request`: Invalid input validation error
- `500 Internal Server Error`: Server error occurred

---

## Database

### Connection String Configuration

The database connection is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=<your host>;Database=VivaDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### Migrations

Database migrations are stored in the `Infrastructure/Migrations` folder. To create a new migration:

```bash
dotnet ef migrations add MigrationName --project Infrastructure --startup-project Api
```

To update the database:

```bash
dotnet ef database update --project Infrastructure --startup-project Api
```

## Configuration

### appsettings.json

Key configuration sections:

**Logging**:
```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}
```

**Countries API Configuration**:
```json
"CountriesHttpClientConfiguration": {
  "BaseUrl": "https://restcountries.com",
  "AllCountriesPath": "/v3.1/all?fields=name,capital,borders" // the current version of this API requires for the required fields to be included in the query params
}
```

**Caching Configuration**:
```json
"CountriesCacheConfiguration": {
  "AbsoluteExpirationMinutes": 30,
  "CacheKey": "countries:all"
}
```

## Key Dependencies

| Package | Purpose |
|---------|---------|
| MediatR | CQRS pattern implementation |
| FluentValidation | Request validation |
| Entity Framework Core | ORM and database access |
| Swashbuckle | Swagger/OpenAPI integration |

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check the connection string in `appsettings.json`
- Ensure the database user has proper permissions

### Migration Issues
- Clear the EF Core cache: `dotnet ef dbcontext optimize --project Infrastructure`
- Delete existing migrations and start fresh if needed

### API Not Starting
- Run `dotnet clean` to clear build artifacts
- Run `dotnet restore` to reinstall packages
- Check for port conflicts on the configured port

## License

This project is open source and available under the MIT License.

## Authors

Created as part of the Viva Assessment project.

---