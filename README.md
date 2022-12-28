# Demo: GraphQL and code-first Entity Framework database
This demonstration presents a simple movie catalog with a GraphQL-based API.

## Technology stack
This demo uses the following technologies:
- Docker & Docker Compose
- SQL Server on Linux
- .NET Core 6.0
- ASP.NET Core Web API
- Hot Chocolate GraphQL framework
- Entity Framework Core & EF Core Migrations
 
## Running the application
To run the application, the following prerequirements should be met:
- Docker Desktop (or a Linux equivalent with Docker host, client and Docker Compose plugin)
- .NET Core SDK 6.0 or later
- Entity Framework Core .NET Command-line Tools ([link](https://learn.microsoft.com/en-us/ef/core/cli/dotnet))
- ASP.NET Core runtime 6.0
- Visual Studio Code
- cUrl (or an equivalent tool)

After installing the prerequirements, follow these instructions:
1. Checkout the source
2. Open the folder in VS Code
3. Install the recommended extensions
4. Copy the connection string from `docker-compose.yml`
5. Open a shell prompt, and
   - Set the SQL_CONNECTION_STRING environment variable's value to the connection string from step #4
   - Issue `docker compose --project-directory src up -d`
   - Issue `dotnet ef database update --startup-project "src/MovieCatalog.API/MovieCatalog.API.csproj" --project "src/MovieCatalog.Persistence/MovieCatalog.Persistence.csproj"`
6. (If on Linux) Make the `deploy/deployInitialData.sh` executable, and issue `./deploy/deployInitialData.sh`
7. (If on Windows) Use any suitable tool to send a similar network request as depicted in the file
     
