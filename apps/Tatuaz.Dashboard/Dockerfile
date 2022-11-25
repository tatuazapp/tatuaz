FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["apps/Tatuaz.Dashboard/Tatuaz.Dashboard.csproj", "apps/Tatuaz.Dashboard/"]
COPY ["libs/Tatuaz.Shared.Infrastructure/Tatuaz.Shared.Infrastructure.csproj", "libs/Tatuaz.Shared.Infrastructure/"]
COPY ["libs/Tatuaz.History.Queue/Tatuaz.History.Queue.csproj", "libs/Tatuaz.History.Queue/"]
COPY ["libs/Tatuaz.History.Infrastructure/Tatuaz.History.Infrastructure.csproj", "libs/Tatuaz.History.Infrastructure/"]
COPY ["libs/Tatuaz.Shared.Infrastructure.Abstractions/Tatuaz.Shared.Infrastructure.Abstractions.csproj", "libs/Tatuaz.Shared.Infrastructure.Abstractions/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities/Tatuaz.Shared.Domain.Entities.csproj", "libs/Tatuaz.Shared.Domain.Entities/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities.Hist/Tatuaz.Shared.Domain.Entities.Hist.csproj", "libs/Tatuaz.Shared.Domain.Entities.Hist/"]
COPY ["libs/Tatuaz.Shared.Helpers/Tatuaz.Shared.Helpers.csproj", "libs/Tatuaz.Shared.Helpers/"]
COPY ["libs/Tatuaz.Shared.Pipeline/Tatuaz.Shared.Pipeline.csproj", "libs/Tatuaz.Shared.Pipeline/"]
COPY ["libs/Tatuaz.Dashboard.Queue/Tatuaz.Dashboard.Queue.csproj", "libs/Tatuaz.Dashboard.Queue/"]
RUN dotnet restore "apps/Tatuaz.Dashboard/Tatuaz.Dashboard.csproj"
COPY . .
WORKDIR "/src/apps/Tatuaz.Dashboard"
RUN dotnet build "Tatuaz.Dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.Dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.Dashboard.dll"]
