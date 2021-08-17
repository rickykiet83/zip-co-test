using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<Order> InsertOrderAsync(Order order)
        {
            try
            {
                Create(order);
                await SaveAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(InsertOrderAsync)}, {order}: " + exp.Message);
            }

            return order;
        }

        public async Task<List<Order>> CreateOrdersAsync(int customerId, List<Order> orders)
        {
            var resultOrders = new List<Order>();
            foreach (var order in orders)
            {
                order.CustomerId = customerId;
                await InsertOrderAsync(order);
                resultOrders.Add(order);
            }

            return resultOrders;
        }

        public async Task<Order> CancelOrderAsync(Order order)
        {
            try
            {
                order.Status = EOrderStatus.Cancelled;
                await UpdateOrderAsync(order);
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(CancelOrderAsync)}: " + exp.Message);
            }
            
            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId, bool trackChanges = false)
        {
            return await FindByCondition(o => o.Id.Equals(orderId), false).SingleOrDefaultAsync();
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