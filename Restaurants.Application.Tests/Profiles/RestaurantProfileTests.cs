using AutoMapper;
using FluentAssertions;
using Restaurants.Application.DTOs;
using Restaurants.Application.Profiles;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Tests.Profiles;

public class RestaurantProfileTests
{
    private readonly IMapper _mapper;

    public RestaurantProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<RestaurantProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_ShouldBeValid()
    {
        // Act & Assert
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_RestaurantToRestaurantDto_ShouldMapCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "+1234567890",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Test City",
                PostalCode = "12345"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Id = 1,
                    Name = "Test Dish",
                    Description = "Test Dish Description",
                    Price = 12.99m,
                    KiloCalories = 500,
                    RestaurantId = 1
                }
            }
        };

        // Act
        var dto = _mapper.Map<RestaurantDto>(restaurant);

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(restaurant.Id);
        dto.Name.Should().Be(restaurant.Name);
        dto.Description.Should().Be(restaurant.Description);
        dto.Category.Should().Be(restaurant.Category);
        dto.HasDelivery.Should().Be(restaurant.HasDelivery);
        dto.ContactEmail.Should().Be(restaurant.ContactEmail);
        dto.ContactNumber.Should().Be(restaurant.ContactNumber);
        dto.Address.Should().NotBeNull();
        dto.Address!.Street.Should().Be(restaurant.Address.Street);
        dto.Address.City.Should().Be(restaurant.Address.City);
        dto.Address.PostalCode.Should().Be(restaurant.Address.PostalCode);
        dto.Dishes.Should().HaveCount(1);
        dto.Dishes.First().Id.Should().Be(restaurant.Dishes.First().Id);
        dto.Dishes.First().Name.Should().Be(restaurant.Dishes.First().Name);
    }

    [Fact]
    public void Map_CreateRestaurantDtoToRestaurant_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new CreateRestaurantDto
        {
            Name = "New Restaurant",
            Description = "New Description",
            Category = "Mexican",
            HasDelivery = false,
            ContactEmail = "new@restaurant.com",
            ContactNumber = "+0987654321",
            Address = new Address
            {
                Street = "456 New St",
                City = "New City",
                PostalCode = "54321"
            }
        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(createDto);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(0); // Default value for int
        restaurant.Name.Should().Be(createDto.Name);
        restaurant.Description.Should().Be(createDto.Description);
        restaurant.Category.Should().Be(createDto.Category);
        restaurant.HasDelivery.Should().Be(createDto.HasDelivery);
        restaurant.ContactEmail.Should().Be(createDto.ContactEmail);
        restaurant.ContactNumber.Should().Be(createDto.ContactNumber);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address!.Street.Should().Be(createDto.Address!.Street);
        restaurant.Address.City.Should().Be(createDto.Address.City);
        restaurant.Address.PostalCode.Should().Be(createDto.Address.PostalCode);
        restaurant.Dishes.Should().NotBeNull();
        restaurant.Dishes.Should().BeEmpty();
    }

    [Fact]
    public void Map_DishToDishDto_ShouldMapCorrectly()
    {
        // Arrange
        var dish = new Dish
        {
            Id = 1,
            Name = "Test Dish",
            Description = "Test Dish Description",
            Price = 15.99m,
            KiloCalories = 750,
            RestaurantId = 1
        };

        // Act
        var dto = _mapper.Map<DishDto>(dish);

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(dish.Id);
        dto.Name.Should().Be(dish.Name);
        dto.Description.Should().Be(dish.Description);
        dto.Price.Should().Be(dish.Price);
        dto.KiloCalories.Should().Be(dish.KiloCalories);
    }

    [Fact]
    public void Map_RestaurantWithNullAddress_ShouldHandleGracefully()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Italian",
            HasDelivery = true,
            Address = null,
            Dishes = new List<Dish>()
        };

        // Act
        var dto = _mapper.Map<RestaurantDto>(restaurant);

        // Assert
        dto.Should().NotBeNull();
        dto.Address.Should().BeNull();
        dto.Dishes.Should().NotBeNull();
        dto.Dishes.Should().BeEmpty();
    }

    [Fact]
    public void Map_CreateRestaurantDtoWithNullAddress_ShouldHandleGracefully()
    {
        // Arrange
        var createDto = new CreateRestaurantDto
        {
            Name = "New Restaurant",
            Description = "New Description",
            Category = "Mexican",
            HasDelivery = false,
            Address = null
        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(createDto);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Address.Should().BeNull();
    }
} 