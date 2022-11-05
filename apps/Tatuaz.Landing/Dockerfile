FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Tatuaz.Landing/Tatuaz.Landing.csproj", "Tatuaz.Landing/"]
COPY ["libs/Tatuaz.Shared.Infrastructure/Tatuaz.Shared.Infrastructure.csproj", "Tatuaz.Shared.Infrastructure/"]
COPY ["libs/Tatuaz.History.Queue/Tatuaz.History.Queue.csproj", "Tatuaz.History.Queue/"]
COPY ["libs/Tatuaz.History.Infrastructure/Tatuaz.History.Infrastructure.csproj", "Tatuaz.History.Infrastructure/"]
COPY ["libs/Tatuaz.Shared.Infrastructure.Abstractions/Tatuaz.Shared.Infrastructure.Abstractions.csproj", "Tatuaz.Shared.Infrastructure.Abstractions/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities/Tatuaz.Shared.Domain.Entities.csproj", "Tatuaz.Shared.Domain.Entities/"]
COPY ["libs/Tatuaz.Shared.Domain.Entities.Hist/Tatuaz.Shared.Domain.Entities.Hist.csproj", "Tatuaz.Shared.Domain.Entities.Hist/"]
COPY ["libs/Tatuaz.Shared.Helpers/Tatuaz.Shared.Helpers.csproj", "Tatuaz.Shared.Helpers/"]
COPY ["libs/Tatuaz.Shared.Pipeline/Tatuaz.Shared.Pipeline.csproj", "Tatuaz.Shared.Pipeline/"]
COPY ["libs/Tatuaz.Landing.Queue/Tatuaz.Landing.Queue.csproj", "Tatuaz.Landing.Queue/"]
COPY ["libs/Tatuaz.Gateway.Queue/Tatuaz.Gateway.Queue.csproj", "Tatuaz.Gateway.Queue/"]
COPY ["libs/Tatuaz.Shared.Domain.Dtos/Tatuaz.Shared.Domain.Dtos.csproj", "Tatuaz.Shared.Domain.Dtos/"]
RUN dotnet restore "apps/Tatuaz.Landing/Tatuaz.Landing.csproj"
COPY . .
WORKDIR "/src/Tatuaz.Landing"
RUN dotnet build "Tatuaz.Landing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tatuaz.Landing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tatuaz.Landing.dll"]
