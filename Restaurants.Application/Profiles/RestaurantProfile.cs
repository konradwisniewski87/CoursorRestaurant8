using AutoMapper;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<Dish, DishDto>();
    }
} 