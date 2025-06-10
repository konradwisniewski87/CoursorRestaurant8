using AutoMapper;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => new List<Dish>()));
            
        CreateMap<Dish, DishDto>();
    }
} 