using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Queries.DishQueries
{
    public class GetDishByIdQuery : IRequest<DishResponseModel>
    {
        public long Id { get; set; }
    }
}