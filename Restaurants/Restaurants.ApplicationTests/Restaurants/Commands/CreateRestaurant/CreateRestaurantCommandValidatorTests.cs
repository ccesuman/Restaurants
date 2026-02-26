using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Category = "Italian",
            ContactEmail = "Test@test.com",
            PostalCode = "848213"
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Te",
            Category = "Ita",
            ContactEmail = "@test.com",
            PostalCode = "048213"
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Category = category
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("012345")]
    [InlineData("12345")]
    [InlineData("1234567")]
    [InlineData("12A456")]
    [InlineData("123-56")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            PostalCode = postalCode
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }

    [Theory()]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(100, false)]
    [InlineData(101, true)]
    public void Validator_ForInvalidName_ShouldHaveValidationErrorsForNameProperty(int length, bool shouldFail)
    {
        // Arrange
        var name = new string('a', length);
        var command = new CreateRestaurantCommand
        {
            Name = name
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        if (shouldFail)
            result.ShouldHaveValidationErrorFor(x => x.Name);
        else
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory()]
    [InlineData("example.com", true)]
    [InlineData("user@", true)]
    [InlineData("@domain.com", true)]
    [InlineData("user@@domain.com", true)]
    [InlineData("user@domain.com", false)]
    public void Validator_ForInvalidEmail_ShouldHaveValidationErrorsForEmailProperty(string email, bool shouldFail)
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            ContactEmail = email
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        if (shouldFail)
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        else
            result.ShouldNotHaveValidationErrorFor(c => c.ContactEmail);
    }
}