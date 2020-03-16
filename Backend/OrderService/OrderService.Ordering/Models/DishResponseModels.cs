namespace OrderService.Ordering.Models
{
    public class DishResponseModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string PhotoPath { get; set; }
    }
}