#!/usr/bin/env bash

resourceGroupName="Art-Store-RG"
cosmosAccountName="art-store-cosmos-account"
databaseName="ArtDatabase"
containerName="ArtContainer"
partitionKeyPath="/Id"

az extension add --name cosmosdb-preview

az group create \
    --name $resourceGroupName \
    --location centralus

az cosmosdb create --name $cosmosAccountName \
    --resource-group $resourceGroupName \
    --kind GlobalDocumentDB \
    --default-consistency-level "Session" \
    --enable-free-tier true

if [[ -z $(az cosmosdb sql database list --resource-group $resourceGroupName --account-name $cosmosAccountName) ]];
then
    echo "Creating database, '$databaseName'..."
    az cosmosdb sql database create \
        --account-name $cosmosAccountName \
        --resource-group $resourceGroupName \
        --name $databaseName
else
    echo 'Database, '$databaseName', already exists.'
    az cosmosdb sql database show \
        --resource-group $resourceGroupName \
        --account-name $cosmosAccountName \
        --name $databaseName
fi

if [[ -z $(az cosmosdb sql container list --resource-group $resourceGroupName --account-name $cosmosAccountName --database-name $databaseName) ]];
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
    az cosmosdb sql container show \
        --resource-group $resourceGroupName \
        --account-name $cosmosAccountName \
        --database-name $databaseName \
        --name $containerName
fi
