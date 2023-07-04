using System;
using api.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class ArtworkItemRepository : IArtworkItemRepository
    {

        private Container _container;

        public ArtworkItemRepository(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("ArtStore", "ArtworkItems");
        }

        public async Task<IEnumerable<ArtworkItem>> GetAllArtworks()
        {
            var query = _container.GetItemQueryIterator<ArtworkItem>();
            List<ArtworkItem> results = new List<ArtworkItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<ArtworkItem?> GetArtworkItem(long id)
        {
            var query = _container.GetItemQueryIterator<ArtworkItem>(
                queryText: $"SELECT * FROM ArtworkItems WHERE Id = {id}"
                );
            List<ArtworkItem> results = new List<ArtworkItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results.FirstOrDefault();
        }

        public async Task<ArtworkItem> UpsertArtwork(ArtworkItem artworkItem)
        {
            var response = await _container.UpsertItemAsync(artworkItem, new PartitionKey(artworkItem.Id));
            return response.Resource;
        }

        public async Task DeleteArtwork(long id)
        {
            await _container.DeleteItemAsync<ArtworkItem>(id.ToString(), new PartitionKey(id.ToString()));
        }

    }
}

