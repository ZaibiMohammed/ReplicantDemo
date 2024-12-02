# ReplicantDemo

A .NET 8 Web API demonstration using Replicant for HTTP caching.

## Features

- Clean Architecture
- Efficient HTTP caching using Replicant
- Background service for cache maintenance
- Cache metrics tracking
- Rate limiting
- Health checks
- Swagger documentation
- Logging with Serilog
- Docker support

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker (optional)

### Running Locally

1. Clone the repository
```bash
git clone https://github.com/YourUsername/ReplicantDemo.git
cd ReplicantDemo
```

2. Run the application
```bash
dotnet run --project src/ReplicantDemo.Api
```

3. Open browser at `https://localhost:5001/swagger`

### Running with Docker

```bash
docker-compose up --build
```

## Project Structure

- `ReplicantDemo.Api`: Web API project
- `ReplicantDemo.Core`: Core business logic
- `ReplicantDemo.Infrastructure`: Infrastructure concerns
- `ReplicantDemo.Tests`: Test project

## API Endpoints

- `GET /WeatherForecast`: Get weather forecast data
- `GET /metrics`: Get cache metrics
- `GET /health`: Get application health status

## Configuration

The application can be configured through `appsettings.json` and environment variables:

```json
{
  "CacheSettings": {
    "MaxEntries": 1000,
    "CacheDirectory": ".replicant"
  },
  "ExternalApiSettings": {
    "BaseUrl": "https://api.example.com",
    "Timeout": 30
  }
}
```

## Contributing

Pull requests are welcome!