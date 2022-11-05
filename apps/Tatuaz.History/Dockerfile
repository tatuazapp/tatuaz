FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Tatuaz.History/Tatuaz.History.csproj", "Tatuaz.History/"]
COPY ["libs/Tatuaz.History.Queue/Tatuaz.History.Queue.csproj", "Tatuaz.History.Queue/"]
COPY ["libs/Tatuaz.History.Infrastructure/Tatuaz.History.Infrastructure.csproj", "Tatuaz.History.Infrastructure/"]
COPY ["libs/Tatuaz.Shared.Infrastructure.Abstractions/Tatuaz.Shared.Infrastructure.Abstractions.csproj", "Tatuaz.Shared.Infrastructure.Abstractions/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities/Tatuaz.Shared.Domain.Entities.csproj", "Tatuaz.Shared.Domain.Entities/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities.Hist/Tatuaz.Shared.Domain.Entities.Hist.csproj", "Tatuaz.Shared.Domain.Entities.Hist/"]
COPY ["libs/Tatuaz.Shared.Helpers/Tatuaz.Shared.Helpers.csproj", "Tatuaz.Shared.Helpers/"]
COPY ["libs/Tatuaz.Shared.Infrastructure/Tatuaz.Shared.Infrastructure.csproj", "Tatuaz.Shared.Infrastructure/"]
COPY ["libs/Tatuaz.Shared.Pipeline/Tatuaz.Shared.Pipeline.csproj", "Tatuaz.Shared.Pipeline/"]
RUN dotnet restore "apps/Tatuaz.History/Tatuaz.History.csproj"
COPY . .
WORKDIR "/src/Tatuaz.History"
RUN dotnet build "Tatuaz.History.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.History.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.History.dll"]
