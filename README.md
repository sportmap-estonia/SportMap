# SportMap
The map-first sports social network

## Database Setup

This project uses PostgreSQL with credentials stored securely via .NET User Secrets.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Configure environment variables

1. **Change directory to AppHost project:**
```bash
   cd SportMap.AppHost
```

2. **Create a copy of the example config:**
```bash
   cp appsettings.Development.example.json appsettings.Development.json
```

3. **Open `appsettings.Development.json` and fill in the parameters**

4. **Run the project - parameters are visible on Aspire dashboard**
```bash
   dotnet run
```