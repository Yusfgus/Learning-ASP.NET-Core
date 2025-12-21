# This Dockerfile builds the app using the .NET SDK, 
# then runs it in a lightweight ASP.NET runtime image using multi-stage builds.

# ================================
# Stage 1: Build the application
# ================================

# Use the full .NET SDK image (needed for restore & publish)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory inside the container
WORKDIR /source

# Copy only the project file first (enables Docker layer caching)
COPY OrderServiceApi/*.csproj OrderServiceApi/

# Restore NuGet dependencies
RUN dotnet restore OrderServiceApi/OrderServiceApi.csproj

# Copy the remaining source code
COPY OrderServiceApi/. ./OrderServiceApi/

# Move into the project directory
WORKDIR /source/OrderServiceApi

# Build and publish the app in Release mode
# Output will be placed in /app
RUN dotnet publish -c Release -o /app --no-restore


# ================================
# Stage 2: Runtime image
# ================================

# Use the lightweight ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app ./

# Define the command to run the application
ENTRYPOINT ["dotnet", "OrderServiceApi.dll"]
