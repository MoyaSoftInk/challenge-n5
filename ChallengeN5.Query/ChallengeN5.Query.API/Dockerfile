# Establece la imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6000

# Imagen para compilar y restaurar dependencias
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChallengeN5.Query/ChallengeN5.Query.API/ChallengeN5.Query.API.csproj", "ChallengeN5.Query/"]
RUN dotnet restore "ChallengeN5.Query/ChallengeN5.Query.API/ChallengeN5.Query.API.csproj"
COPY . .
WORKDIR "/src/ChallengeN5.Query/ChallengeN5.Query.API"
RUN dotnet build "ChallengeN5.Query.API.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "ChallengeN5.Query.API.csproj" -c Release -o /app/publish

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChallengeN5.Query.API.dll"]
