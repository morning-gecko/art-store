#!/usr/bin/env bash

resourceGroupName="Art-Store-RG"
cosmosAccountName="art-store-cosmos-account"
databaseName="ArtDatabase"
containerName="ArtContainer"
partitionKeyPath="/Id"

echo "Installing cosmosdb-preview Azure CLI extension..."
az extension add --name cosmosdb-preview

echo "Creating resource group, '$resourceGroupName'..."
az group create --name $resourceGroupName --location centralus

echo "Creating Cosmos DB account, '$cosmosAccountName'..."
az cosmosdb create --name $cosmosAccountName --resource-group $resourceGroupName --kind GlobalDocumentDB --default-consistency-level "Session"

echo "Creating database, '$databaseName'..."
az cosmosdb sql database create --account-name $cosmosAccountName --resource-group $resourceGroupName --name $databaseName

echo "Creating container, '$containerName'..."
az cosmosdb sql container create --account-name $cosmosAccountName --database-name $databaseName --name $containerName --resource-group $resourceGroupName --partition-key-path $partitionKeyPath --throughput 400
