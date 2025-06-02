using System.ComponentModel.DataAnnotations;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public class CreateRestaurantDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = default!;
    
    [Required]
    [StringLength(50)]
    public string Category { get; set; } = default!;
    
    public bool HasDelivery { get; set; }
    
    [EmailAddress]
    public string? ContactEmail { get; set; }
    
    [Phone]
    public string? ContactNumber { get; set; }
    
    public Address? Address { get; set; }
} 