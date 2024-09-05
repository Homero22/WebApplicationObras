# Fase base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# No exponemos puertos estáticos. Railway asigna el puerto.
EXPOSE 80 

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApplicationObras.csproj", "."]
RUN dotnet restore "./WebApplicationObras.csproj"
COPY . .
RUN dotnet build "./WebApplicationObras.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApplicationObras.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Usa el puerto proporcionado por Railway
ENV ASPNETCORE_URLS=http://+:${PORT}
ENTRYPOINT ["dotnet", "WebApplicationObras.dll"]
