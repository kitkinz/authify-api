## Setup

### Database Configuration

1. Initialize user secrets:
```bash
   dotnet user-secrets init
```

2. Set your database connection string:
```bash
   dotnet user-secrets set "ConnectionStrings:DbConnectionString" "YOUR_CONNECTION_STRING"
```

3. Run migrations (if applicable)

4. Start the application:
```bash
   dotnet run
```