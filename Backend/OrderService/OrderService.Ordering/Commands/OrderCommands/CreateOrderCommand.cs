using System.Collections.Generic;
using MediatR;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<OrderResponseModel>
    {
        public string Description { get; set; }
        
        public long CustomerId { get; set; }
        
        public ICollection<OrderItemRequestModel> Items { get; set; }
        
        public string OrderTime { get; set; }
    }
    
    public class OrderItemRequestModel
    {
        public long DishId { get; set; }
        
        public int Quantity { get; set; }
    }
}