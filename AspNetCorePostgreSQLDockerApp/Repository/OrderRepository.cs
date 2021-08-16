using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(CustomersDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Order>> GetOrdersAsync(int customerId, bool trackChanges = false)
        {
            var orders = await FindByCondition(o => o.CustomerId.Equals(customerId), trackChanges)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> InsertOrderAsync(Order order)
        {
            Create(order);
            await SaveAsync();
            return order;
        }
    }
}