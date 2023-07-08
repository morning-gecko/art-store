using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace api
{
    public static class Middleware
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            var config = app.Configuration;
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseExceptionHandler("/Error");
            }
            
            app.MapControllers();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () =>
            {
                return "hello world";
            });
            return app;
        }
    }
}

