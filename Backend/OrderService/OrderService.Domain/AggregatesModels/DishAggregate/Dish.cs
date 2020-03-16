namespace OrderService.Domain.AggregatesModels.DishAggregate
{
    public class Dish
    {
        public long Id { get; set; }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public string PhotoPath { get; private set; }


        public Dish(string name, string description, string photoPath)
        {
            Name = name;
            Description = description;
            PhotoPath = photoPath;
        }
        
        public void Update(string name, string description, string photoPath)
        {
            Name = name;
            Description = description;
            PhotoPath = photoPath;
        }
    }
}