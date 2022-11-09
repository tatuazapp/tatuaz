FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Tatuaz.Gateway/Tatuaz.Gateway.csproj", "apps/Tatuaz.Gateway/"]
COPY ["libs/Tatuaz.Gateway.Handlers/Tatuaz.Gateway.Handlers.csproj", "libs/Tatuaz.Gateway.Handlers/"]
COPY ["libs/Tatuaz.Gateway.Infrastructure/Tatuaz.Gateway.Infrastructure.csproj", "libs/Tatuaz.Gateway.Infrastructure/"]
COPY ["libs/Tatuaz.Shared.Infrastructure.Abstractions/Tatuaz.Shared.Infrastructure.Abstractions.csproj", "libs/Tatuaz.Shared.Infrastructure.Abstractions/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities/Tatuaz.Shared.Domain.Entities.csproj", "libs/Tatuaz.Shared.Domain.Entities/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities.Hist/Tatuaz.Shared.Domain.Entities.Hist.csproj", "libs/Tatuaz.Shared.Domain.Entities.Hist/"]
COPY ["libs/Tatuaz.Gateway.Queue/Tatuaz.Gateway.Queue.csproj", "libs/Tatuaz.Gateway.Queue/"]
COPY ["libs/Tatuaz.Shared.Domain.Dtos/Tatuaz.Shared.Domain.Dtos.csproj", "libs/Tatuaz.Shared.Domain.Dtos/"]
COPY ["libs/Tatuaz.Shared.Helpers/Tatuaz.Shared.Helpers.csproj", "libs/Tatuaz.Shared.Helpers/"]
COPY ["libs/Tatuaz.Shared.Pipeline/Tatuaz.Shared.Pipeline.csproj", "libs/Tatuaz.Shared.Pipeline/"]
COPY ["libs/Tatuaz.Gateway.Requests/Tatuaz.Gateway.Requests.csproj", "libs/Tatuaz.Gateway.Requests/"]
COPY ["libs/Tatuaz.Shared.Infrastructure/Tatuaz.Shared.Infrastructure.csproj", "libs/Tatuaz.Shared.Infrastructure/"]
COPY ["libs/Tatuaz.History.Queue/Tatuaz.History.Queue.csproj", "libs/Tatuaz.History.Queue/"]
COPY ["libs/Tatuaz.History.Infrastructure/Tatuaz.History.Infrastructure.csproj", "libs/Tatuaz.History.Infrastructure/"]
RUN dotnet restore "apps/Tatuaz.Gateway/Tatuaz.Gateway.csproj"
COPY . .
WORKDIR "/src/apps/Tatuaz.Gateway"
RUN dotnet build "Tatuaz.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.Gateway.dll"]
