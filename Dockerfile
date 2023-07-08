# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /backend/api.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY /backend ./
RUN dotnet publish -c Release -o out


# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "api.dll"]
