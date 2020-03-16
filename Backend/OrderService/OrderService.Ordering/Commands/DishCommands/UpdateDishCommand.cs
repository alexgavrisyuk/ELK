using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Commands.DishCommands
{
    public class UpdateDishCommand : IRequest<DishResponseModel>
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string PhotoPath { get; set; }
    }
}