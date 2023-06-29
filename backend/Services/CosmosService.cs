using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using api.Models;

namespace api.Services
{
    public interface ICosmosService
    {
        Task<IEnumerable<ArtworkItem>> RetrieveAllProductsAsync();
        Task<IEnumerable<ArtworkItem>> RetrieveActiveProductsAsync();
    }

    public class CosmosService : ICosmosService
    {
        private readonly Container _container;

        public CosmosService(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("artworks", "products");
        }

        public async Task<IEnumerable<ArtworkItem>> RetrieveAllProductsAsync()
        {
            var queryable = _container.GetItemLinqQueryable<ArtworkItem>();

            using FeedIterator<ArtworkItem> feed = queryable
                .Where(p => p.Price < 2000)
                .OrderByDescending(p => p.Price)
                .ToFeedIterator();

            var results = new List<ArtworkItem>();

            while (feed.HasMoreResults)
            {
                var response = await feed.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<IEnumerable<ArtworkItem>> RetrieveActiveProductsAsync()
        {
            string sql = @"
                SELECT
                    p.id,
                    p.categoryId,
                    p.categoryName,
                    p.sku,
                    p.name,
                    p.description,
                    p.price,
                    p.tags
                FROM products p
                JOIN t IN p.tags
                WHERE t.name = @tagFilter";

            var query = new QueryDefinition(sql)
                .WithParameter("@tagFilter", "Tag-75");

            using FeedIterator<ArtworkItem> feed = _container.GetItemQueryIterator<ArtworkItem>(
                queryDefinition: query
            );

            var results = new List<ArtworkItem>();

            while (feed.HasMoreResults)
            {
                FeedResponse<ArtworkItem> response = await feed.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
    }
}
