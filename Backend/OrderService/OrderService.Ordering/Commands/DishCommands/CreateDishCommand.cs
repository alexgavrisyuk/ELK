using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Commands.DishCommands
{
    public class CreateDishCommand : IRequest<DishResponseModel>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string PhotoPath { get; set; }
    }
}