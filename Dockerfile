FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ./API /API
COPY ./client /client
COPY ./Core /Core
COPY ./Infrastructure /Infrastructure

RUN dotnet restore /API/API.csproj
RUN dotnet publish /API/API.csproj --configuration "Release" --output "out"

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
RUN ls -al /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]
