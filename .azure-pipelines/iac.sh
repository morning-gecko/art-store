#!/usr/bin/env bash

location="eastus"

resourceGroupName="rg-art-store"

cosmosAccountName="csmo-acct-art"
databaseName="csmo-db-art"
containerName="csmo-cont-art"
partitionKeyPath="/Id"

appServicePlanName="app-plan-art"
appServiceName="web-app-art"

az extension add --name cosmosdb-preview

az group create \
    --name $resourceGroupName \
    --location $location

az cosmosdb create --name $cosmosAccountName \
    --resource-group $resourceGroupName \
    --kind GlobalDocumentDB \
    --default-consistency-level "Session" \
    --enable-free-tier true

if [[ -z $(az cosmosdb sql database show \
        --resource-group $resourceGroupName \
        --account-name $cosmosAccountName \
        --name $databaseName 2> /dev/null) ]];
then
    echo "Creating database, '$databaseName'..."
    az cosmosdb sql database create \
        --account-name $cosmosAccountName \
        --resource-group $resourceGroupName \
        --name $databaseName
else
    echo 'Database, '$databaseName', already exists.'
fi

if [[ -z $(az cosmosdb sql container show \
        --resource-group $resourceGroupName \
        --account-name $cosmosAccountName \
        --database-name $databaseName \
        --name $containerName 2> /dev/null) ]];
then
    echo "Creating container, '$containerName'..."
    az cosmosdb sql container create \
        --account-name $cosmosAccountName \
        --database-name $databaseName \
        --name $containerName \
        --resource-group $resourceGroupName \
        --partition-key-path $partitionKeyPath \
        --throughput 400
else 
    echo 'Container, '$containerName', already exists.'
fi

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
