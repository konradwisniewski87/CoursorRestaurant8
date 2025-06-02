using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        
        // Register FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        
        // Register Services
        services.AddScoped<IRestaurantsService, RestaurantsService>();
    }
} 