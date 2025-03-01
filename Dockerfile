# Usa l'immagine base di .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5266  # Esporre la porta 5266 per il tuo progetto

# Costruisci e pubblica l'app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . . 
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Avvia l'app
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MiniApi.dll"]  # Modifica "MiniApi.dll" con il nome corretto del tuo progetto
