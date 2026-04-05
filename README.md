# SportMap
The map-first sports social network

## Database Setup

This project uses PostgreSQL with credentials stored securely via .NET User Secrets.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Configure Credentials

1. **Initialize user secrets** in the AppHost project:
```bash
   cd src/SportMap.AppHost
   dotnet user-secrets init
```

2. **Set PostgreSQL credentials**:
```bash
   dotnet user-secrets set "Parameters:postgres-username" "your_username"
   dotnet user-secrets set "Parameters:postgres-password" "your_secure_password"
   dotnet user-secrets set "Parameters:postgres-host" "localhost"
   dotnet user-secrets set "Parameters:postgres-port" "5432"
   dotnet user-secrets set "Parameters:postgres-database" "sportmapdb"
```

3. **Set JWT and Google credentials for authentication**:
```bash
   dotnet user-secrets set "Parameters:jwt-secret" "your_jwt_secret"
   dotnet user-secrets set "Parameters:jwt-issuer" "SportMap"
   dotnet user-secrets set "Parameters:jwt-audience" "SportMapUsers"
   dotnet user-secrets set "Parameters:google-client-id" "your_google_client_id"
   dotnet user-secrets set "Parameters:google-client-secret" "your_google_client_secret"
   dotnet user-secrets set "Parameters:google-redirect-uri" "http://localhost:3000"
```
   Connection String could be added here as well for migrations.

4. **Verify secrets** (optional):
```bash
   dotnet user-secrets list
```

   You should see:
```
   Parameters:postgres-username = your_username
   Parameters:postgres-password = your_secure_password
```
   Parameters are visible on Aspire Dashboard.