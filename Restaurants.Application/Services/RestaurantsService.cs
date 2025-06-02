using AutoMapper;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Application.Services;

public class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepository _repository;
    private readonly IMapper _mapper;

    public RestaurantsService(IRestaurantsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        var restaurant = await _repository.GetByIdAsync(id);
        return restaurant == null ? null : _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<int> CreateAsync(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        return await _repository.CreateAsync(restaurant);
    }

    public async Task UpdateAsync(int id, CreateRestaurantDto dto)
    {
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with ID {id} not found");
        }

        _mapper.Map(dto, restaurant);
        await _repository.UpdateAsync(restaurant);
    }

    public async Task DeleteAsync(int id)
    {
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with ID {id} not found");
        }

        await _repository.DeleteAsync(restaurant);
    }
} 