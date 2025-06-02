using Restaurants.Application.DTOs;

namespace Restaurants.Application.Services;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
    Task<RestaurantDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateRestaurantDto dto);
    Task UpdateAsync(int id, CreateRestaurantDto dto);
    Task DeleteAsync(int id);
} 