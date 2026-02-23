
namespace Restaurants.Application.Restaurants.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}