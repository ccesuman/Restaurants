using MediatR;

namespace Restaurants.Application.Restaurants.Users.Commands;

public class UpdateUserDetailsCommand: IRequest
{
    public DateOnly? DateOfBirth { get; set; }

    public string? Nationality { get; set; }
}