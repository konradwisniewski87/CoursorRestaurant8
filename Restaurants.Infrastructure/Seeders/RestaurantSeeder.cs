using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

public class RestaurantSeeder
{
    private readonly RestaurantsDbContext _context;

    public RestaurantSeeder(RestaurantsDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (await _context.Database.CanConnectAsync())
        {
            if (!await _context.Restaurants.AnyAsync())
            {
                var restaurants = GetSampleRestaurants();
                _context.Restaurants.AddRange(restaurants);
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetSampleRestaurants()
    {
        return new List<Restaurant>
        {
            new Restaurant
            {
                Name = "Pizza Palace",
                Description = "Authentic Italian pizza with fresh ingredients",
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
                        Name = "Margherita Pizza",
                        Description = "Classic pizza with tomato sauce, mozzarella, and basil",
                        Price = 12.99m,
                        KiloCalories = 850
                    },
                    new Dish
                    {
                        Name = "Pepperoni Pizza",
                        Description = "Pizza with pepperoni and mozzarella cheese",
                        Price = 15.99m,
                        KiloCalories = 950
                    }
                }
            },
            new Restaurant
            {
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
                        Name = "Classic Cheeseburger",
                        Description = "Beef patty with cheese, lettuce, tomato, and pickles",
                        Price = 9.99m,
                        KiloCalories = 650
                    },
                    new Dish
                    {
                        Name = "BBQ Bacon Burger",
                        Description = "Beef patty with BBQ sauce, bacon, and onion rings",
                        Price = 13.99m,
                        KiloCalories = 800
                    }
                }
            },
            new Restaurant
            {
                Name = "Sushi Zen",
                Description = "Fresh sushi and Japanese cuisine",
                Category = "Japanese",
                HasDelivery = false,
                ContactEmail = "info@sushizen.com",
                ContactNumber = "+1555123456",
                Address = new Address
                {
                    Street = "789 Cherry Blvd",
                    City = "San Francisco",
                    PostalCode = "94102"
                },
                Dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name = "California Roll",
                        Description = "Crab, avocado, and cucumber roll",
                        Price = 8.99m,
                        KiloCalories = 300
                    },
                    new Dish
                    {
                        Name = "Salmon Nigiri",
                        Description = "Fresh salmon over seasoned rice",
                        Price = 4.99m,
                        KiloCalories = 120
                    }
                }
            }
        };
    }
} 