# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["projet-hopital.csproj", "."]
RUN dotnet restore "projet-hopital.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "projet-hopital.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "projet-hopital.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published output
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "projet-hopital.dll"]

