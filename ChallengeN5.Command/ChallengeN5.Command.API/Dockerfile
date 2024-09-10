# Establece la imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Imagen para compilar y restaurar dependencias
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChallengeN5.Command/ChallengeN5.Command.API/ChallengeN5.Command.API.csproj", "ChallengeN5.Command/"]
RUN dotnet restore "ChallengeN5.Command/ChallengeN5.Command.API/ChallengeN5.Command.API.csproj"
COPY . .
WORKDIR "/src/ChallengeN5.Command/ChallengeN5.Command.API"
RUN dotnet build "ChallengeN5.Command.API.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "ChallengeN5.Command.API.csproj" -c Release -o /app/publish

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChallengeN5.Command.API.dll"]
