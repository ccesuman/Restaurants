using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Query.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQuery(int restaurantId): IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get;  } = restaurantId;
    }
}
