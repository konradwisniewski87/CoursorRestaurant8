using FluentAssertions;
using FluentValidation.TestHelper;
using Restaurants.Application.DTOs;
using Restaurants.Application.Validators;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Tests.Validators;

public class CreateRestaurantDtoValidatorTests
{
    private readonly CreateRestaurantDtoValidator _validator;

    public CreateRestaurantDtoValidatorTests()
    {
        _validator = new CreateRestaurantDtoValidator();
    }

    [Fact]
    public void Validate_WithValidDto_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var dto = new CreateRestaurantDto
        {
            Name = "Test Restaurant",
            Description = "A great test restaurant",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "+1234567890",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Test City",
                PostalCode = "12345"
            }
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_WithInvalidName_ShouldHaveValidationError(string name)
    {
        // Arrange
        var dto = GetValidDto();
        dto.Name = name;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("Restaurant name is required");
    }

    [Fact]
    public void Validate_WithNameTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var dto = GetValidDto();
        dto.Name = new string('a', 101); // 101 characters, exceeds 100 limit

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("Restaurant name cannot exceed 100 characters");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_WithInvalidDescription_ShouldHaveValidationError(string description)
    {
        // Arrange
        var dto = GetValidDto();
        dto.Description = description;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorMessage("Description is required");
    }

    [Fact]
    public void Validate_WithDescriptionTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var dto = GetValidDto();
        dto.Description = new string('a', 501); // 501 characters, exceeds 500 limit

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorMessage("Description cannot exceed 500 characters");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_WithInvalidCategory_ShouldHaveValidationError(string category)
    {
        // Arrange
        var dto = GetValidDto();
        dto.Category = category;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category)
              .WithErrorMessage("Category is required");
    }

    [Fact]
    public void Validate_WithCategoryTooLong_ShouldHaveValidationError()
    {
        // Arrange
        var dto = GetValidDto();
        dto.Category = new string('a', 51); // 51 characters, exceeds 50 limit

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category)
              .WithErrorMessage("Category cannot exceed 50 characters");
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("test@")]
    [InlineData("@domain.com")]
    [InlineData("test.domain.com")]
    public void Validate_WithInvalidEmail_ShouldHaveValidationError(string email)
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactEmail = email;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail)
              .WithErrorMessage("Please provide a valid email address");
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user+tag@domain.co.uk")]
    [InlineData("firstname.lastname@company.org")]
    public void Validate_WithValidEmail_ShouldNotHaveValidationError(string email)
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactEmail = email;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
    }

    [Fact]
    public void Validate_WithNullEmail_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactEmail = null;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
    }

    [Theory]
    [InlineData("invalid-phone")]
    [InlineData("123")]
    [InlineData("abc123")]
    [InlineData("+")]
    public void Validate_WithInvalidPhoneNumber_ShouldHaveValidationError(string phoneNumber)
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactNumber = phoneNumber;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContactNumber)
              .WithErrorMessage("Please provide a valid phone number");
    }

    [Theory]
    [InlineData("+1234567890")]
    [InlineData("1234567890")]
    [InlineData("+12345678901234")]
    public void Validate_WithValidPhoneNumber_ShouldNotHaveValidationError(string phoneNumber)
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactNumber = phoneNumber;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ContactNumber);
    }

    [Fact]
    public void Validate_WithNullPhoneNumber_ShouldNotHaveValidationError()
    {
        // Arrange
        var dto = GetValidDto();
        dto.ContactNumber = null;

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ContactNumber);
    }

    private static CreateRestaurantDto GetValidDto()
    {
        return new CreateRestaurantDto
        {
            Name = "Test Restaurant",
            Description = "A great test restaurant",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@restaurant.com",
            ContactNumber = "+1234567890",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Test City",
                PostalCode = "12345"
            }
        };
    }
} 