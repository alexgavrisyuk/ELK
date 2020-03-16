using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<long, Customer>
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}