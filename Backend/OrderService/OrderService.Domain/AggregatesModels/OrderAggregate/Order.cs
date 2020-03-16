using System.Collections.Generic;

namespace OrderService.Domain.AggregatesModels.OrderAggregate
{
    public class Order
    {
        public long Id { get; set; }
        
        public string OrderTime { get; private set; }
        
        public string Description { get; private set; }
        
        public long CustomerId { get; private set; }
        
        public string NameOfCustomer { get; private set; }
        
        public string DateOfOrder { get; set; }
        
        public ICollection<OrderItem> Items { get; set; }

        public Order(string description, long customerId, string nameOfCustomer, string dateOfOrder)
        {
            OrderTime = dateOfOrder;
            Description = description;
            CustomerId = customerId;
            NameOfCustomer = nameOfCustomer;
            Items = new List<OrderItem>();
        }

        public void AddOrderItem(long dishId, int quantity)
        {
            Items.Add(new OrderItem(dishId, quantity));
        }
        
        public void Update(string description)
        {
            Description = description;
        }
    }
}