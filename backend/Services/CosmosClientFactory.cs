using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.Services
{
    public static class CosmosClientFactory
    {
        public static void AddCosmosClient(this IServiceCollection services, IConfiguration configuration)
        {
            TokenCredential credential = new DefaultAzureCredential();
            CosmosClient client = new CosmosClient(
                accountEndpoint: configuration["CosmosDB:Endpoint"],
                tokenCredential: credential
            );
            services.AddSingleton<CosmosClient>(client);
        }
    }
}
