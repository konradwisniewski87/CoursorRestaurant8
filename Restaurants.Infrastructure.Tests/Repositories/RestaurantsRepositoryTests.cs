using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Tests.Repositories;

public class RestaurantsRepositoryTests : IDisposable
{
    private readonly RestaurantsDbContext _context;
    private readonly RestaurantsRepository _repository;

    public RestaurantsRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RestaurantsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RestaurantsDbContext(options);
        _repository = new RestaurantsRepository(_context);

        // Seed initial data
        SeedTestData();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllRestaurants()
    {
        // Act
        var restaurants = await _repository.GetAllAsync();

        // Assert
        restaurants.Should().NotBeNull();
        restaurants.Should().HaveCount(2);
        restaurants.All(r => r.Dishes != null).Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnRestaurant()
    {
        // Act
        var restaurant = await _repository.GetByIdAsync(1);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant!.Id.Should().Be(1);
        restaurant.Name.Should().Be("Pizza Palace");
        restaurant.Dishes.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var restaurant = await _repository.GetByIdAsync(999);

        // Assert
        restaurant.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldAddRestaurantAndReturnId()
    {
        // Arrange
        var newRestaurant = new Restaurant
        {
            Name = "New Restaurant",
            Description = "A brand new restaurant",
            Category = "French",
            HasDelivery = true,
            ContactEmail = "new@restaurant.com",
            ContactNumber = "+1555123456",
            Address = new Address
            {
                Street = "789 New St",
                City = "New City",
                PostalCode = "54321"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "French Fries",
                    Description = "Crispy golden fries",
                    Price = 5.99m,
                    KiloCalories = 365
                }
            }
        };

        // Act
        var createdId = await _repository.CreateAsync(newRestaurant);

        // Assert
        createdId.Should().BeGreaterThan(0);
        newRestaurant.Id.Should().Be(createdId);

        var savedRestaurant = await _context.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == createdId);

        savedRestaurant.Should().NotBeNull();
        savedRestaurant!.Name.Should().Be("New Restaurant");
        savedRestaurant.Dishes.Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyExistingRestaurant()
    {
        // Arrange
        var restaurant = await _repository.GetByIdAsync(1);
        restaurant!.Name = "Updated Pizza Palace";
        restaurant.Description = "Updated description";
        restaurant.HasDelivery = false;

        // Act
        await _repository.UpdateAsync(restaurant);

        // Assert
        var updatedRestaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Id == 1);

        updatedRestaurant.Should().NotBeNull();
        updatedRestaurant!.Name.Should().Be("Updated Pizza Palace");
        updatedRestaurant.Description.Should().Be("Updated description");
        updatedRestaurant.HasDelivery.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveRestaurant()
    {
        // Arrange
        var restaurant = await _repository.GetByIdAsync(2);
        restaurant.Should().NotBeNull();

        // Act
        await _repository.DeleteAsync(restaurant!);

        // Assert
        var deletedRestaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Id == 2);

        deletedRestaurant.Should().BeNull();

        // Verify dishes are also deleted (cascade delete)
        var dishes = await _context.Dishes
            .Where(d => d.RestaurantId == 2)
            .ToListAsync();

        dishes.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldIncludeRelatedDishes()
    {
        // Act
        var restaurants = await _repository.GetAllAsync();

        // Assert
        restaurants.Should().NotBeEmpty();
        restaurants.All(r => r.Dishes != null).Should().BeTrue();
        restaurants.SelectMany(r => r.Dishes).Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateAsync_WithComplexAddress_ShouldPersistCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Name = "Address Test Restaurant",
            Description = "Testing address persistence",
            Category = "Test",
            HasDelivery = true,
            Address = new Address
            {
                Street = "123 Complex Address Street",
                City = "Complex City",
                PostalCode = "ABC-123"
            }
        };

        // Act
        var createdId = await _repository.CreateAsync(restaurant);

        // Assert
        var savedRestaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Id == createdId);

        savedRestaurant.Should().NotBeNull();
        savedRestaurant!.Address.Should().NotBeNull();
        savedRestaurant.Address!.Street.Should().Be("123 Complex Address Street");
        savedRestaurant.Address.City.Should().Be("Complex City");
        savedRestaurant.Address.PostalCode.Should().Be("ABC-123");
    }

    private void SeedTestData()
    {
        var restaurants = new List<Restaurant>
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
                    new Dish
                    {
                        Id = 1,
                        Name = "Margherita Pizza",
                        Description = "Classic pizza with tomato sauce, mozzarella, and basil",
                        Price = 12.99m,
                        KiloCalories = 850,
                        RestaurantId = 1
                    },
                    new Dish
                    {
                        Id = 2,
                        Name = "Pepperoni Pizza",
                        Description = "Pizza with pepperoni and mozzarella cheese",
                        Price = 15.99m,
                        KiloCalories = 950,
                        RestaurantId = 1
                    }
                }
            },
            new Restaurant
            {
                Id = 2,
                Name = "Burger Town",
                Description = "Premium burgers made with quality beef",
                Category = "American",
                HasDelivery = true,
                ContactEmail = "contact@burgertown.com",
                ContactNumber = "+1987654321",
                Address = new Address
                {
                    Street = "456 Oak Ave",
                    City = "Los Angeles",
                    PostalCode = "90210"
                },
                Dishes = new List<Dish>
                {
                    new Dish
                    {
                        Id = 3,
                        Name = "Classic Cheeseburger",
                        Description = "Beef patty with cheese, lettuce, tomato, and pickles",
                        Price = 9.99m,
                        KiloCalories = 650,
                        RestaurantId = 2
                    }
                }
            }
        };

        _context.Restaurants.AddRange(restaurants);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
} 