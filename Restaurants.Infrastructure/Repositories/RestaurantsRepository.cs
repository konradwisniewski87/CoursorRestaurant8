using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository : IRestaurantsRepository
{
    private readonly RestaurantsDbContext _context;

    public RestaurantsRepository(RestaurantsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await _context.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }
} 