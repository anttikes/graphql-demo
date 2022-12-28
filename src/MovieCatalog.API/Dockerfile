FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieCatalog.API/MovieCatalog.API.csproj", "MovieCatalog.API/"]
COPY ["MovieCatalog.Domain/MovieCatalog.Domain.csproj", "MovieCatalog.Domain/"]
COPY ["MovieCatalog.Persistence/MovieCatalog.Persistence.csproj", "MovieCatalog.Persistence/"]
COPY ["MovieCatalog.sln", "."]
COPY ["MovieCatalog_Publish.slnf", "."]
RUN dotnet restore "MovieCatalog_Publish.slnf"
COPY . .
WORKDIR "/src/MovieCatalog.API"
RUN dotnet publish "MovieCatalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MovieCatalog.API.dll"]
