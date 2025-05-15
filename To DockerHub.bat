@echo off

echo Docker login...
docker login

cd "WebFileServer"

echo Building Docker image api...
docker build -t p22-asp-api .

echo Tagging Docker image api...
docker tag p22-asp-api:latest novakvova/p22-asp-api:latest

echo Pushing Docker image api to repository...
docker push novakvova/p22-asp-api:latest

echo Done ---api---!
pause

