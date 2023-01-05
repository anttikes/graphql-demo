# Demo: GraphQL and Entity Framework 
This demonstration presents a simple movie catalog with a GraphQL-based API.

## Build status
[![Build and test](https://github.com/anttikes/graphql-demo/actions/workflows/coverage.yml/badge.svg)](https://github.com/anttikes/graphql-demo/actions/workflows/coverage.yml)

## Technology stack
This demo uses the following technologies:
- Docker & Docker Compose
- SQL Server on Linux
- .NET Core 6.0
- ASP.NET Core Web API
- Hot Chocolate GraphQL framework
- Entity Framework Core (with Migrations and Compiled Models)
- MediatR
- FluentValidations

## Running the demo
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
4. Copy the connection string from `docker-compose.yml`, replacing server name with `localhost`
5. Open a shell prompt, and
   - Set the SQL_CONNECTION_STRING environment variable's value to the connection string from step #4
   - Issue `docker compose --project-directory "src" up -d`
   - Issue `dotnet ef database update --startup-project "src/MovieCatalog.API/MovieCatalog.API.csproj" --project "src/MovieCatalog.Persistence/MovieCatalog.Persistence.csproj"`
6. (If on Linux) Make the `deploy/deployInitialData.sh` executable, and issue `./deploy/deployInitialData.sh`
7. (If on Windows) Use any suitable tool to send a similar network request as depicted in the file
8. Open a browser window, navigate to `localhost:32741` and start running GraphQL queries (filters & sorting also supported)
9. Issue `docker compose --project-directory "src" stop` to stop the containers

## Notes
Filtering movies by genre during a query does not work. There's likely something wrong in the value comparer implementation, and likely a different kind of persistence mechanism is required in order for it to work correctly.
