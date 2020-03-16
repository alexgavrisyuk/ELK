using System.Collections.Generic;
using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Commands.OrderCommands
{
    public class UpdateOrderCommand : IRequest<OrderResponseModel>
    {
        public long Id { get; set; }
  
        public string Description { get; set; }
        
        public string OrderTime { get; set; }
    
        public ICollection<OrderItemRequestModel> Items { get; set; }
    }
}