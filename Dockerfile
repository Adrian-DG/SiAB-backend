# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SiAB.Core/SiAB.Core.csproj", "SiAB.Core/"]
COPY ["SiAB.Application/SiAB.Application.csproj", "SiAB.Application/"]
COPY ["SiAB.Infrastructure/SiAB.Infrastructure.csproj", "SiAB.Infrastructure/"]
COPY ["SiAB.API/SiAB.API.csproj", "SiAB.API/"]

RUN dotnet restore "SiAB.API/SiAB.API.csproj"

COPY . .

WORKDIR "/src/SiAB.API"

ARG BUILD_CONFIGURATION=Release
RUN dotnet build "SiAB.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "SiAB.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SiAB.API.dll"]