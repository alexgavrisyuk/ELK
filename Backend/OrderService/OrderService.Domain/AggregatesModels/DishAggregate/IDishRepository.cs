using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregatesModels.DishAggregate
{
    public interface IDishRepository
    {
        Task<Dish> GetByIdAsync(long id);
        Task<ICollection<Dish>> GetAsync();
        Task<Dish> CreateAsync(Dish dish);
        Task<Dish> Update(Dish dish);
        Task Delete(Dish dish);
        Task<bool> SaveChangesAsync();
    }
}