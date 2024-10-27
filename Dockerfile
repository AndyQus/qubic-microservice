# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY . .

# Restore dependencies and build project
RUN dotnet restore && dotnet publish -c Release -o /app/publish

# Stage 2: Run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the app will run on
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "QubicMicroservice.dll"]
