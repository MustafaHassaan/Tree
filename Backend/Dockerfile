# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ["Tradeflow.sln", "./"]
COPY ["Tradeflow.Domain/Tradeflow.Domain.csproj", "Tradeflow.Domain/"]
COPY ["Tradeflow.Application/Tradeflow.Application.csproj", "Tradeflow.Application/"]
COPY ["Tradeflow.Infrastructure/Tradeflow.Infrastructure.csproj", "Tradeflow.Infrastructure/"]
COPY ["Tradeflow.API/Tradeflow.API.csproj", "Tradeflow.API/"]

# Restore dependencies
RUN dotnet restore "Tradeflow.sln"

# Copy all project files
COPY Tradeflow.Domain/. Tradeflow.Domain/
COPY Tradeflow.Application/. Tradeflow.Application/
COPY Tradeflow.Infrastructure/. Tradeflow.Infrastructure/
COPY Tradeflow.API/. Tradeflow.API/

# Build the project
WORKDIR "/src/Tradeflow.API"
RUN dotnet build "Tradeflow.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Tradeflow.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Create logs directory
RUN mkdir -p /app/logs

COPY --from=publish /app/publish .

# Wait for SQL Server to be ready
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Tradeflow.API.dll"]
