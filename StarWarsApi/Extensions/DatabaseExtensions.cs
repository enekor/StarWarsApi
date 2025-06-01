using BDCADAO.BDModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StarWarsApi.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseInitialization(this IServiceCollection services)
        {
            // Add an initializer that runs after the service provider is built
            services.AddTransient<DatabaseInitializer>();
            return services;
        }
    }

    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync()
        {
            // Create a new scope to get scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ModelContext>();

                // Ensure database is created
                await context.Database.EnsureCreatedAsync();
            }
        }
    }
}
