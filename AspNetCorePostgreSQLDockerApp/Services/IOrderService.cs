using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public interface IOrderService : IApplicationService<Order, OrderDto, OrderForCreationDto, OrderForUpdateDto, int>
    {
        Task<CustomerOrdersDto> GetOrdersAsync(int customerId, bool trackChange = false);
        Task<OrderDto> GetOrderAsync(int orderId, bool trackChange = false);
        Task<CustomerOrdersDto> CreateOrdersAsync(int customerId, List<Order> orders);
        Task<OrderDto> CancelOrderAsync(int orderId);
        Task<OrderDto> UpdateOrderAsync(OrderForUpdateDto order);
        Task<CustomerDto> GetCustomerAsync(int id, bool trackChange = false);
    }
}