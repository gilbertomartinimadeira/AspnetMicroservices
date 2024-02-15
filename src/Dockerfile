FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
#Set environment
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Services/Basket/Basket.API/Basket.API.csproj Services/Basket/Basket.API/
COPY BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj BuildingBlocks/EventBus.Messages/

RUN dotnet restore Services/Basket/Basket.API/Basket.API.csproj
# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/Services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

# Use the official ASP.NET runtime image as a runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]