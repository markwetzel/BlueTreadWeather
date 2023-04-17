# Use the official .NET 6.0 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the .csproj files and restore the NuGet packages
COPY *.sln .
COPY API/*.csproj ./API/
COPY Common/*.csproj ./Common/
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the API project
WORKDIR /app/API
RUN dotnet publish -c Release -o out

# Use the official .NET 6.0 runtime image as the base image for the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory
WORKDIR /app

# Copy the published API files to the runtime container
COPY --from=build /app/API/out ./

# Expose the port the API will run on
EXPOSE 80

# Set the entrypoint for the runtime container
ENTRYPOINT ["dotnet", "API.dll"]
