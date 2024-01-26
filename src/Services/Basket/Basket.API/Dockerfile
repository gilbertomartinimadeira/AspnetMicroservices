# Use the official .NET SDK image as a build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o out

# Use the official ASP.NET runtime image as a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port the app will run on
EXPOSE 80
EXPOSE 8080

#Set environment
ENV ASPNETCORE_ENVIRONMENT=Development

# Command to run the application
CMD ["dotnet", "Basket.API.dll"]