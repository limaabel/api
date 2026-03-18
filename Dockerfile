# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["../APIApplication/APIApplication.csproj", "APIApplication/"]
COPY ["../EngineApplication/EngineApplication.csproj", "EngineApplication/"]
COPY ["../InfrastructureApplication/InfrastructureApplication.csproj", "InfrastructureApplication/"]

# Restore
RUN dotnet restore "APIApplication/APIApplication.csproj"

# Copiar tudo
COPY .. .

# Publicar
WORKDIR "/src/APIApplication"
RUN dotnet publish "APIApplication.csproj" -c Release -o /app/publish

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "APIApplication.dll"]