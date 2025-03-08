# Sezione 1: Creazione dell'applicazione .NET
# Usa l'immagine base di .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5266

# Costruisci e pubblica l'app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . . 
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Fase finale: Copia l'applicazione nel contenitore base
FROM base AS final
WORKDIR /app
COPY --from=build /app . 
ENTRYPOINT ["dotnet", "MiniApi.dll"]

# Sezione 2: Configurazione di Jenkins e installazione del .NET SDK
# Usa l'immagine base di Jenkins
FROM jenkins/jenkins:lts

# Installa .NET SDK 8.0
USER root
RUN apt-get update && \
    apt-get install -y wget && \
    wget https://download.visualstudio.microsoft.com/download/pr/d2abdb4c-a96e-4123-9351-e4dd2ea20905/e8010ae2688786ffc1ebca4ebb52f41b/dotnet-sdk-8.0.406-linux-x64.tar.gz && \
    mkdir -p /usr/share/dotnet && \
    tar -xvf dotnet-sdk-8.0.100-linux-x64.tar.gz -C /usr/share/dotnet && \
    ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet

# Torna all'utente Jenkins
USER jenkins
