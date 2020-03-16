using System.Collections.Generic;

namespace OrderService.Ordering.Models
{
    public class OrderResponseModel
    {
        public long Id { get; set; }
        
        public string Description { get; private set; }

        public long CustomerId { get; private set; }
        
        public string NameOfCustomer { get; private set; }
        
        public string OrderTime { get; set; }
        
        public ICollection<OrderItemResponseModel> Items { get; set; }
    }

    public class OrderItemResponseModel
    {
        public int Quantity { get; set; }
        
        public long DishId { get; set; }
    }
}