# Base stage - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:${PORT}

# Build stage - SDK
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["APICatalog/APICatalog/APICatalog.csproj", "APICatalog/"]
RUN dotnet restore "APICatalog/APICatalog/APICatalog.csproj"

# Copy the rest of the code
COPY . .

# Build
WORKDIR "/src/APICatalog/APICatalog"
RUN dotnet build "APICatalog.csproj" -c $configuration -o /app/build

# Publish
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "APICatalog.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final stage - runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APICatalog.dll"]
