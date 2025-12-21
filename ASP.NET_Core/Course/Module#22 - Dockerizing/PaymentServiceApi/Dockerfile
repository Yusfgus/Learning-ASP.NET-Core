# ================================
# Stage 1: Build stage
# ================================

# Use the .NET SDK image (contains compilers and build tools)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /source

# Copy only the project file first
# This enables Docker layer caching for faster rebuilds
COPY PaymentServiceApi/*.csproj PaymentServiceApi/

# Restore NuGet dependencies for the project
RUN dotnet restore PaymentServiceApi/PaymentServiceApi.csproj

# Copy the rest of the application source code
COPY PaymentServiceApi/. ./PaymentServiceApi/

# Change working directory to the project folder
WORKDIR /source/PaymentServiceApi

# Build and publish the application in Release mode
# Output will be placed in /app
# --no-restore avoids restoring packages again
RUN dotnet publish -c Release -o /app --no-restore


# ================================
# Stage 2: Final (runtime) stage
# ================================

# Use the lightweight ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Set the working directory for the running application
WORKDIR /app

# Copy only the published output from the build stage
# This keeps the final image small and secure
COPY --from=build /app ./

# Define the command that runs when the container starts
ENTRYPOINT ["dotnet", "PaymentServiceApi.dll"]
