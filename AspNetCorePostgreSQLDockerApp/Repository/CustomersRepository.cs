using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class CustomersRepository : RepositoryBase<Customer, int>, ICustomersRepository
    {
        private readonly ILogger _logger;
        private readonly IStateRepository _stateRepository;

        public CustomersRepository(CustomersDbContext context, ILoggerFactory loggerFactory, IStateRepository stateRepository) : base(context)
        {
            _stateRepository = stateRepository;
            _logger = loggerFactory.CreateLogger("CustomersRepository");
        }

        public async Task<List<Customer>> GetCustomersAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.LastName)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id, bool trackChanges = false)
        {
            return await FindByIdAsync(id);
        }

        public Task<Customer> GetCustomerOrdersAsync(int id, bool trackChanges = false)
        {
            return FindByIdAsync(id, x => x.Orders);
        }

        public async Task<List<State>> GetStatesAsync(bool trackChanges = false)
        {
            return await _stateRepository.FindAll(trackChanges)
                .OrderBy(s => s.Abbreviation)
                .ToListAsync();
        }

        public async Task<List<Customer>> SearchCustomerByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return await FindAll(false).ToListAsync();
            
            return await FindByCondition(c => c.Email.ToLower().Contains(email.ToLower())).ToListAsync();
        }

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            Create(customer);
            try
            {
                await SaveAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(InsertCustomerAsync)}: " + exp.Message);
            }

            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            //Will update all properties of the Customer
            Update(customer);
            try
            {
                return await SaveAsync() > 0;
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
            }

            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            //Extra hop to the database but keeps it nice and simple for this demo
            var customer = await GetCustomerAsync(id);
            Delete(customer);
            try
            {
                return await SaveAsync() > 0;
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(DeleteCustomerAsync)}: " + exp.Message);
            }

            return false;
        }
    }
}