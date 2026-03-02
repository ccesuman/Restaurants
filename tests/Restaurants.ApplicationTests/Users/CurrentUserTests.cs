using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.ApplicationTests.Users;

public class CurrentUserTests
{
    // TestMethod_Scenario_ExpectResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var result = user.IsInRole(roleName);

        // Assert
        result.Should().BeTrue();

    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var result = user.IsInRole(UserRoles.Admin.ToLower());

        // Assert
        result.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnTrue()
    {
        // Arrange
        var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var result = user.IsInRole(UserRoles.Owner);

        // Assert
        result.Should().BeFalse();
    }
}