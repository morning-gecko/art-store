#!/usr/bin/env bash

location="westus"

resourceGroupName="rg-art-store"

appServicePlanName="app-plan-art"
appServiceName="web-app-art"

pgServerName="{{PgServerName}}"

artDbUser="{{ArtDbUser}}"
artDbPassword="{{ArtDbPassword}}"
artDbName="{{ArtDbName}}"

idServerName="{{IdServerName}}"
idDbUser="{{IdDbUser}}"
idDbPassword="{{IdDbPassword}}"
idDbName="{{IdDbName}}"

az group create \
    --name $resourceGroupName \
    --location $location

if [[ -z $(az appservice plan show \
        --name $appServicePlanName \
        --resource-group $resourceGroupName \
        --query "name" \
        --output tsv 2> /dev/null) ]];
then
    echo "Creating App Service Plan, '$appServicePlanName'..."
    az appservice plan create \
        --name $appServicePlanName \
        --resource-group $resourceGroupName \
        --location $location \
        --is-linux \
        --sku F1
else
    echo "App Service Plan, '$appServicePlanName', already exists."
fi

if [[ -z $(az webapp show \
        --name $appServiceName \
        --resource-group $resourceGroupName \
        --query "name" \
        --output tsv 2> /dev/null) ]];
then
    echo "Creating App Service, '$appServiceName'..."
    az webapp create \
        --resource-group $resourceGroupName \
        --plan $appServicePlanName \
        --name $appServiceName \
        --runtime "DOTNETCORE:7.0"
else
    echo "App Service, '$appServiceName', already exists."
fi

echo "Setting environment variables for '$appServiceName'..."
az webapp config appsettings set \
    --resource-group $resourceGroupName \
    --name $appServiceName \
    --settings ASPNETCORE_ENVIRONMENT=Production

az webapp config connection-string set \
    --resource-group $resourceGroupName \
    --name $appServiceName \
    --connection-string-type Custom \
    --settings DefaultConnection="Server={{PgServerName}}.postgres.database.azure.com;Port=5432;User Id={{ArtDbUser}}@{{PgServerName}};Password={{ArtDbPassword}};Database={{ArtDbName}};Ssl Mode=VerifyFull;"

az postgres server create \
    --name $pgServerName \
    --resource-group $resourceGroupName \
    --location $location \
    --admin-user $artDbUser \
    --admin-password $artDbPassword \
    --sku-name B_Gen5_1 \
    --version 11

az postgres server firewall-rule create \
    --resource-group $resourceGroupName \
    --server-name $pgServerName \
    --name AllowAllAzureIPs \
    --start-ip-address '0.0.0.0' \
    --end-ip-address '0.0.0.0'

az postgres db create \
    --resource-group $resourceGroupName \
    --server-name $pgServerName \
    --name $artDbName

az postgres flexible-server create \
    --name $idServerName \
    --resource-group $resourceGroupName \
    --location $location \
    --admin-user $idDbUser \
    --admin-password $idDbPassword \
    --sku-name Standard_B1ms \
    --tier Burstable \
    --version 13

az postgres server firewall-rule create \
    --resource-group $resourceGroupName \
    --server-name $pgServerName \
    --name AllowAllAzureIPs \
    --start-ip-address '0.0.0.0' \
    --end-ip-address '0.0.0.0'

az postgres db create \
    --resource-group $resourceGroupName \
    --server-name $pgServerName \
    --name $idDbName
