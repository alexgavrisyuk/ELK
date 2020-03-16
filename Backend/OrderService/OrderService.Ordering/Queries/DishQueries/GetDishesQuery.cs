using System.Collections.Generic;
using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Queries.DishQueries
{
    public class GetDishesQuery : IRequest<ICollection<DishResponseModel>>
    {
        
    }
}