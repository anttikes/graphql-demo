# Demo: GraphQL and Entity Framework
This demonstration presents a simple movie catalog with a GraphQL-based API.

## Technology stack
This demo uses the following technologies:
- Docker & Docker Compose
- SQL Server on Linux
- .NET Core 6.0
- ASP.NET Core Web API
- Hot Chocolate GraphQL framework
- Entity Framework Core & EF Core Migrations
 
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
4. Copy the connection string from `docker-compose.yml`
5. Open a shell prompt, and
   - Set the SQL_CONNECTION_STRING environment variable's value to the connection string from step #4
   - Issue `docker compose --project-directory src up -d`
   - Issue `dotnet ef database update --startup-project "src/MovieCatalog.API/MovieCatalog.API.csproj" --project "src/MovieCatalog.Persistence/MovieCatalog.Persistence.csproj"`
6. (If on Linux) Make the `deploy/deployInitialData.sh` executable, and issue `./deploy/deployInitialData.sh`
7. (If on Windows) Use any suitable tool to send a similar network request as depicted in the file
8. Open a browser window, navigate to `localhost:32741` and start running GraphQL queries

## Notes
The GraphQL API does not support modifying the actors participating in a movie or setting the director for a movie. The root cause is the use of constructor-based entity classes without property setters. These are not properly supported by the Hot Chocolate framework. See https://github.com/ChilliCream/hotchocolate/issues/4387 for more details.
     
