using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.Azure.Cosmos;
using api.Services;

namespace api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

            services.AddControllers();

            services.AddDbContext<ArtworkContext>(opt =>
                opt.UseSqlServer(connectionString: Configuration.GetConnectionString("ConnectionString")));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Register Cosmos DB service
            services.AddSingleton<CosmosClient>(sp =>
            {
                var cosmosEndpoint = Configuration["CosmosDB:Endpoint"];
                var cosmosKey = Configuration["CosmosDB:Key"];

                return new CosmosClient(cosmosEndpoint, cosmosKey);
            });

            services.AddScoped<ICosmosService, CosmosService>();
        }
    }
}
