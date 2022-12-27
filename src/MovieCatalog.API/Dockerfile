FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieCatalog.API/MovieCatalog.API.csproj", "MovieCatalog.API/"]
RUN dotnet restore "MovieCatalog.API/MovieCatalog.API.csproj"
COPY . .
WORKDIR "/src/MovieCatalog.API"
RUN dotnet build "MovieCatalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieCatalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gofore.Demo.MovieCatalog.API.dll"]