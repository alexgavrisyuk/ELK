using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregatesModels.OrderAggregate;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order> GetByIdAsync(long id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Dish)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<ICollection<Order>> GetAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Dish)
                .OrderBy(o => o.Id)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public Order Update(Order order)
        {
            _context.Orders.Update(order);
            return order;
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}