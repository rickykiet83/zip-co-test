using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface IOrderRepository : IRepositoryBase<Order, int>
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int customerId, bool trackChanges = false);
        Task<Order> GetOrderAsync(int orderId, bool trackChanges = false);
        IEnumerable<Order> CreateOrders(int customerId, List<Order> orders);
        Task<Order> CancelOrderAsync(Order order);
        Order UpdateOrder(Order order);
    }
}