using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregatesModels.DishAggregate;

namespace OrderService.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Dish> GetByIdAsync(long id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            return dish;
        }

        public async Task<ICollection<Dish>> GetAsync()
        {
            return await _context.Dishes.ToListAsync();
        }

        public async Task<Dish> CreateAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task<Dish> Update(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task Delete(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}