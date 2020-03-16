using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Domain;

namespace CustomerService.Logic.Interfaces
{
    public interface ICustomerService
    {
        Task<ICollection<Customer>> GetAsync();
        Task<Customer> GetAsync(long id);
        Task<Customer> CreateAsync(string firstName, string lastName);
        Task<Customer> UpdateAsync(long id, string firstName, string lastName);
        Task<bool> DeleteAsync(long id);
    }
}