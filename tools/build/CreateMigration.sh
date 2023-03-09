#!/usr/bin/env bash
if [ -z "$1" ]; then
  echo "Usage: $0 <migration-name>"
  exit 1
fi

dotnet ef migrations add "$1" -s apps/Tatuaz.Dashboard/Tatuaz.Dashboard.csproj -p libs/Tatuaz.Shared.Infrastructure/Tatuaz.Shared.Infrastructure.csproj
dotnet ef migrations add "$1" -s apps/Tatuaz.History/Tatuaz.History.csproj -p libs/Tatuaz.History.Infrastructure/Tatuaz.History.Infrastructure.csproj
