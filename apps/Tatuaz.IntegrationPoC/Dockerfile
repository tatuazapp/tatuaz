FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY apps/Tatuaz.IntegrationPoc ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
<<<<<<< HEAD:apps/Tatuaz.IntegrationPoC/Dockerfile
ENTRYPOINT ["dotnet", "Tatuaz.IntegrationPoc.dll"]
=======
EXPOSE 80
ENTRYPOINT ["dotnet", "Tatuaz.Gateway.Api.dll"]
>>>>>>> main:apps/Tatuaz.Gateway.Api/Dockerfile
