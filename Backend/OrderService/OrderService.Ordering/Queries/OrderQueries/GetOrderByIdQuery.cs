using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Queries.OrderQueries
{
    public class GetOrderByIdQuery : IRequest<OrderResponseModel>
    {
        public long Id { get; set; }
    }
}