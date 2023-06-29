using Microsoft.AspNetCore.Builder;

namespace api
{
    class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.CreateBuilder(args)
                .RegisterServices()
                .Build()
                .SetupMiddleware()
                .Run();
        }
    }
}
