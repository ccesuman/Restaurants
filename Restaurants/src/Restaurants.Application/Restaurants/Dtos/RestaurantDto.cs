using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; }


    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }

    public List<DishDto> Dishes { get; set; } = new List<DishDto>();


    //public static RestaurantDto? FromEntity(Restaurant? restaurant)
    //{
    //    if (restaurant == null)
    //    {
    //        return null;
    //    } 

    //    return new RestaurantDto()
    //    {
    //        Name = restaurant.Name,
    //        Description = restaurant.Description,
    //        Category = restaurant.Category,
    //        HasDelivery = restaurant.HasDelivery,
    //        City = restaurant.Address?.City,
    //        Street = restaurant.Address?.Street,
    //        PostalCode = restaurant.Address?.PostalCode,
    //        Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
    //    };
    //}
}