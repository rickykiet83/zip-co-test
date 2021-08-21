using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class OrdersRepository : RepositoryBase<Order, int>, IOrdersRepository
    {
        private readonly ILogger _logger;
        public OrdersRepository(CustomersDbContext context, ILoggerFactory loggerFactory) : base(context)
        {
            _logger = loggerFactory.CreateLogger("OrdersRepository");
        }
        
        public async Task<IEnumerable<Order>> GetOrdersAsync(int customerId, bool trackChange = false)
        {
            var orders = await FindByCondition(trackChange, 
                o => o.CustomerId.Equals(customerId))
                .ToListAsync();

            return orders;
        }

        public IEnumerable<Order> CreateOrders(int customerId, List<Order> orders)
        {
            var resultOrders = new List<Order>();
            foreach (var order in orders)
            {
                order.CustomerId = customerId;
                Create(order);
                resultOrders.Add(order);
            }

            return resultOrders;
        }

        public Order CancelOrder(Order order)
        {
            try
            {
                order.Status = EOrderStatus.Cancelled;
                UpdateOrder(order);
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(CancelOrder)}: " + exp.Message);
            }
            
            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId, bool trackChange = false)
        {
            return await FindByIdAsync(orderId, x => x.Customer);
        }

        public Order UpdateOrder(Order order)
        {
            try
            {
                Update(order);
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(UpdateOrder)}: " + exp.Message);
            }
            
            return order;
        }
    }
}