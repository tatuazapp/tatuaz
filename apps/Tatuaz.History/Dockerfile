﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "apps/Tatuaz.History/Tatuaz.History.csproj"
WORKDIR "/src/apps/Tatuaz.History"
RUN dotnet build "Tatuaz.History.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.History.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.History.dll"]
