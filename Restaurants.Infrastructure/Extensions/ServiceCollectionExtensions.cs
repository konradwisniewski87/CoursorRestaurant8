using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<RestaurantsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionDb")));

        // Register Repositories
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();

        // Register Seeders
        services.AddScoped<RestaurantSeeder>();
    }
} 