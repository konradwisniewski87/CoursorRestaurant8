using AutoMapper;
using FluentAssertions;
using Moq;
using Restaurants.Application.DTOs;
using Restaurants.Application.Profiles;
using Restaurants.Application.Services;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Application.Tests.Services;

public class RestaurantsServiceTests
{
    private readonly Mock<IRestaurantsRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly RestaurantsService _service;

    public RestaurantsServiceTests()
    {
        _mockRepository = new Mock<IRestaurantsRepository>();
        
        // Configure AutoMapper
        var config = new MapperConfiguration(cfg => cfg.AddProfile<RestaurantProfile>());
        _mapper = config.CreateMapper();
        
        _service = new RestaurantsService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllRestaurants()
    {
        // Arrange
        var restaurants = GetSampleRestaurants();
        _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(restaurants);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Pizza Palace");
        result.Last().Name.Should().Be("Burger Town");
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnRestaurant()
    {
        // Arrange
        var restaurant = GetSampleRestaurants().First();
        _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(restaurant);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Pizza Palace");
        result.Dishes.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Restaurant?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_WithValidDto_ShouldReturnCreatedId()
    {
        // Arrange
        var createDto = new CreateRestaurantDto
        {
            Name = "New Restaurant",
            Description = "Great food",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "+1234567890"
        };

        _mockRepository.Setup(x => x.CreateAsync(It.IsAny<Restaurant>())).ReturnsAsync(5);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        result.Should().Be(5);
        _mockRepository.Verify(x => x.CreateAsync(It.Is<Restaurant>(r => 
            r.Name == createDto.Name && 
            r.Description == createDto.Description &&
            r.Category == createDto.Category)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithValidIdAndDto_ShouldUpdateRestaurant()
    {
        // Arrange
        var existingRestaurant = GetSampleRestaurants().First();
        var updateDto = new CreateRestaurantDto
        {
            Name = "Updated Restaurant",
            Description = "Updated description",
            Category = "Updated Category",
            HasDelivery = false
        };

        _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(existingRestaurant);
        _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Restaurant>())).Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(1, updateDto);

        // Assert
        _mockRepository.Verify(x => x.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.Is<Restaurant>(r => 
            r.Name == updateDto.Name && 
            r.Description == updateDto.Description)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var updateDto = new CreateRestaurantDto
        {
            Name = "Updated Restaurant",
            Description = "Updated description",
            Category = "Updated Category",
            HasDelivery = false
        };

        _mockRepository.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Restaurant?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(999, updateDto));
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Restaurant>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldDeleteRestaurant()
    {
        // Arrange
        var restaurant = GetSampleRestaurants().First();
        _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(restaurant);
        _mockRepository.Setup(x => x.DeleteAsync(It.IsAny<Restaurant>())).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _mockRepository.Verify(x => x.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(x => x.DeleteAsync(restaurant), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Restaurant?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(999));
        _mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Restaurant>()), Times.Never);
    }

    private static List<Restaurant> GetSampleRestaurants()
    {
        return new List<Restaurant>
        {
            new Restaurant
            {
                Id = 1,
                Name = "Pizza Palace",
                Description = "Authentic Italian pizza",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "info@pizzapalace.com",
                ContactNumber = "+1234567890",
                Address = new Address
                {
                    Street = "123 Main St",
                    City = "New York",
                    PostalCode = "10001"
                },
                Dishes = new List<Dish>
                {
                    new Dish { Id = 1, Name = "Margherita", Description = "Classic pizza", Price = 12.99m, RestaurantId = 1 },
                    new Dish { Id = 2, Name = "Pepperoni", Description = "Pepperoni pizza", Price = 15.99m, RestaurantId = 1 }
                }
            },
            new Restaurant
            {
                Id = 2,
                Name = "Burger Town",
                Description = "Premium burgers",
                Category = "American",
                HasDelivery = true,
                ContactEmail = "contact@burgertown.com",
                ContactNumber = "+1987654321",
                Dishes = new List<Dish>
                {
                    new Dish { Id = 3, Name = "Cheeseburger", Description = "Classic cheeseburger", Price = 9.99m, RestaurantId = 2 }
                }
            }
        };
    }
} 