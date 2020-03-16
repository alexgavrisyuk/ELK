using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Commands.DishCommands
{
    public class DeleteDishCommand : IRequest<DishResponseModel>
    {
        public long Id { get; set; }
    }
}