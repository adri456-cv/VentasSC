# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copia el archivo .csproj (Ya que el Dockerfile está en la misma carpeta que el .csproj)
# Copia el archivo Ventas.csproj a la raíz de /src en el contenedor
COPY ["Ventas.csproj", "."]

# 2. Restaura las dependencias
RUN dotnet restore "Ventas.csproj"

# 3. Copia el resto de los archivos fuente
COPY . .

# 4. Compila el proyecto (el WORKDIR ya es /src)
RUN dotnet build "Ventas.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ventas.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false