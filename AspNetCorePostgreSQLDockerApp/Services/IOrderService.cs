using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync(int customerId, bool trackChanges = false);
        Task<OrderDto> GetOrderAsync(int orderId, bool trackChanges = false);
        Task<IEnumerable<OrderDto>> CreateOrdersAsync(int customerId, List<Order> orders);
        Task<OrderDto> CancelOrderAsync(int orderId);
        Task<OrderDto> UpdateOrderAsync(OrderForUpdateDto order);
    }
}