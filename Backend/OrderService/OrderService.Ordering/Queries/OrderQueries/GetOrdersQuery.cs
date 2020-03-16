using System.Collections.Generic;
using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Queries.OrderQueries
{
    public class GetOrdersQuery : IRequest<ICollection<OrderResponseModel>>
    {
        
    }
}