using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements;
using Xunit;

namespace Restaurants.InfrastructureTests.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                                OwnerId = currentUser.Id
            },
            new()
                {
                OwnerId = "2"
            }
            
        };

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(rr => rr.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);

        var handler = new CreateMultipleRestaurantsRequirementHandler( restaurantRepositoryMock.Object, userContextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
       await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            }

        };

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(rr => rr.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);

        var handler = new CreateMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasFailed.Should().BeTrue();
    }

}