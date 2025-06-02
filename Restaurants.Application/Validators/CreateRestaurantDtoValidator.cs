using FluentValidation;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Restaurant name is required")
            .MaximumLength(100)
            .WithMessage("Restaurant name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required")
            .MaximumLength(50)
            .WithMessage("Category cannot exceed 50 characters");

        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.ContactEmail))
            .WithMessage("Please provide a valid email address");

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Please provide a valid phone number");
    }
} 