using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace api
{
    public static class Middleware
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            var config = app.Configuration;
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () =>
            {
                return "hello world";
            }).RequireAuthorization();

            return app;
        }
    }
}

