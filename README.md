# Demo: GraphQL and Entity Framework

This demonstration presents a simple movie catalog with a GraphQL-based API.

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
4. Open a shell prompt, and issue `docker compose --project-directory "src" up -d`
   - The application auto-creates the database if it doesn't exist yet
5. (If on Linux) Make the `deploy/deployInitialData.sh` executable, and issue `./deploy/deployInitialData.sh`
6. (If on Windows) Import the collection from `deploy/Postman_collection.json`, and use the "Upload initial data" request to send `deploy/movies-compact.json` to the server
7. Open a browser window, navigate to `localhost:32741` and start running GraphQL queries (filters & sorting also supported)
8. Issue `docker compose --project-directory "src" stop` to stop the containers

## Notes

Filtering movies by genre during a query does not work. There's likely something wrong in the value comparer implementation, and likely a different kind of persistence mechanism is required in order for it to work correctly.

Validation errors trapped by FluentValidation do not appear in the console output.
