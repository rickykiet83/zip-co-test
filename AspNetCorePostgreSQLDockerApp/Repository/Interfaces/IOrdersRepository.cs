using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface IOrdersRepository : IRepositoryBase<Order, int>
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int customerId, bool trackChange = false);
        Task<Order> GetOrderAsync(int orderId, bool trackChange = false);
        IEnumerable<Order> CreateOrders(int customerId, List<Order> orders);
        Order CancelOrder(Order order);
        Order UpdateOrder(Order order);
    }
}