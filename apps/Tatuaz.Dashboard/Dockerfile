FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "apps/Tatuaz.Dashboard/Tatuaz.Dashboard.csproj"
WORKDIR "/src/apps/Tatuaz.Dashboard"
RUN dotnet build "Tatuaz.Dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.Dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.Dashboard.dll"]
