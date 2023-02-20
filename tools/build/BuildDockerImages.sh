docker build -f apps/Tatuaz.Gateway/Dockerfile -t tatuazmainacr.azurecr.io/gateway:latest .
docker build -f apps/Tatuaz.Dashboard/Dockerfile -t tatuazmainacr.azurecr.io/dashboard:latest .
docker build -f apps/Tatuaz.History/Dockerfile -t tatuazmainacr.azurecr.io/history:latest .
docker build -f apps/Tatuaz.Scheduler/Dockerfile -t tatuazmainacr.azurecr.io/scheduler:latest .
