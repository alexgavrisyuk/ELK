using OrderService.Domain.AggregatesModels.DishAggregate;

namespace OrderService.Domain.AggregatesModels.OrderAggregate
{
    public class OrderItem
    {
        public long Id { get; set; }
        
        public long OrderId { get; set; }   
        public Order Order { get; set; }
        
        public long DishId { get; set; }   
        public Dish Dish { get; set; }
        
        public int Quantity { get; set; }

        public OrderItem(long dishId, int quantity)
        {
            DishId = dishId;
            Quantity = quantity;
        }
    }
}