# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# COPIA EL ARCHIVO .CSPROJ: Como el Dockerfile y el .csproj están en el mismo directorio,
# copiamos el .csproj a la raíz del WORKDIR (/src) para restaurar dependencias.
# Esto usa solo el nombre del archivo, eliminando la carpeta 'Ventas/' que causaba el error.
COPY ["Ventas.csproj", "."]

# RESTAURAR: El WORKDIR es /src y el .csproj está ahí, la ruta es simple.
RUN dotnet restore "Ventas.csproj"

# COPIA EL RESTO DEL CÓDIGO: Copia todos los archivos restantes del contexto de compilación a /src.
COPY . .

# CONSTRUIR: Compila el proyecto.
RUN dotnet build "Ventas.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# PUBLICAR: Publica el proyecto.
RUN dotnet publish "Ventas.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ventas.dll"]