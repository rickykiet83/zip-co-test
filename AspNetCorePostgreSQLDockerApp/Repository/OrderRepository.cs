using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ILogger _logger;
        public OrderRepository(CustomersDbContext context, ILoggerFactory loggerFactory) : base(context)
        {
            _logger = loggerFactory.CreateLogger("OrderRepository");
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
            try
            {
                await SaveAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(InsertOrderAsync)}: " + exp.Message);
            }

            return order;
        }

        public async Task<Order> CancelOrderAsync(Order order)
        {
            order.Status = EOrderStatus.Cancelled;
            try
            {
                await UpdateOrderAsync(order);
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(CancelOrderAsync)}: " + exp.Message);
            }
            
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            try
            {
                Update(order);
                await SaveAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(UpdateOrderAsync)}: " + exp.Message);
            }
            
            return order;
        }
    }
}