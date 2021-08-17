using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int customerId, bool trackChanges = false);
        Task<Order> GetOrderAsync(int orderId, bool trackChanges = false);
        Task<List<Order>> CreateOrdersAsync(int customerId, List<Order> orders);
        Task<Order> CancelOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
    }
}