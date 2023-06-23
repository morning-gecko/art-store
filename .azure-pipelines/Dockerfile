# Multi-stage Docker build

# Build Angular Frontend
FROM node:18 as angular-build
WORKDIR /app
COPY ArtStore/ClientApp/package*.json ./
RUN npm ci
COPY ArtStore/ClientApp/ ./
RUN npm run build --prod

# Build .NET Backend
FROM mcr.microsoft.com/dotnet/sdk:7.0 as dotnet-build
WORKDIR /app
COPY ArtStore/ ./
RUN dotnet restore
RUN dotnet publish --configuration Release --output out --verbosity detailed

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=dotnet-build /app/out ./
COPY --from=angular-build /app/dist/ArtStore wwwroot/
EXPOSE 80
ENTRYPOINT ["dotnet", "ArtStore.dll"]
