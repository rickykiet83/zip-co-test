using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface ICustomersRepository : IRepositoryBase<Customer, int>
    {
        Task<List<Customer>> GetCustomersAsync(bool trackChanges = false);

        Task<Customer> GetCustomerAsync(int id, bool trackChanges = false);

        Task<Customer> InsertCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<List<State>> GetStatesAsync(bool trackChanges = false);
        Task<List<Customer>> SearchCustomerByEmail(string email);
    }
}