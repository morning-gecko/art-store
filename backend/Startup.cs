using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Services;
using Microsoft.Azure.Cosmos;

namespace api
{
    public static class Startup
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // ******* Access the configuration *******
            var config = builder.Configuration;

            builder.Services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ArtworkContext>(opt =>
                opt.UseSqlServer(config["ConnectionString"]));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });


            builder.Services.AddCosmosClient(config);

            return builder;
        }
    }
}

