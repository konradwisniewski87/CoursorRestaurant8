using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> CreateAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);
} 