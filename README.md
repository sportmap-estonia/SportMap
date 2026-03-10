# SportMap
The map-first sports social network

## Database Setup

This project uses PostgreSQL with credentials stored securely via .NET User Secrets.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Configure Database Credentials

1. **Initialize user secrets** in the AppHost project:
```bash
   cd src/SportMap.AppHost
   dotnet user-secrets init
```

2. **Set PostgreSQL credentials**:
```bash
   dotnet user-secrets set "Parameters:postgres-username" "your_username"
   dotnet user-secrets set "Parameters:postgres-password" "your_secure_password"
```

3. **Verify secrets** (optional):
```bash
   dotnet user-secrets list
```

   You should see:
```
   Parameters:postgres-username = your_username
   Parameters:postgres-password = your_secure_password
```
