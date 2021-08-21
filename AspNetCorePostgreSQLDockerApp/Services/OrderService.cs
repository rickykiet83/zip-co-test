using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AutoMapper;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public class OrderService : ApplicationService<Order, OrderDto, OrderForCreationDto, OrderForUpdateDto, int>, IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;
        
        public OrderService(IRepositoryBase<Order, int> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrdersRepository ordersRepository, ICustomersRepository customersRepository) : base(repository, unitOfWork, mapper)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }
      
        
        public async Task<CustomerOrdersDto> GetOrdersAsync(int customerId, bool trackChange = false)
        {
            var customer = await _customersRepository.GetCustomerOrdersAsync(customerId, trackChange);
            if (customer == null) return null;
            var result = _mapper.Map<CustomerOrdersDto>(customer);
            return result;
        }

        public async Task<OrderDetailDto> GetOrderAsync(int orderId, bool trackChange = false)
        {
            var order = await _ordersRepository.GetOrderAsync(orderId, trackChange);
            if (order == null) return null;
            
            var result = _mapper.Map<OrderDetailDto>(order);
            return result;
        }

        public async Task<CustomerOrdersDto> CreateOrdersAsync(int customerId, List<Order> orders)
        {
            var addedOrders = _ordersRepository.CreateOrders(customerId, orders);
            await SaveAsync();
            var result = _mapper.Map<CustomerOrdersDto>(addedOrders);
            
            return result;
        }

        public async Task<OrderDto> CancelOrderAsync(int orderId)
        {
            var order = await _ordersRepository.GetOrderAsync(orderId);
            if (order == null) return null;
            
            var canceledOrder = _ordersRepository.CancelOrder(order);
            await SaveAsync();
            var result = _mapper.Map<OrderDto>(canceledOrder);
            
            return result; 
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderForUpdateDto orderDto)
        {
            var updateOrder = _mapper.Map<Order>(orderDto);
            var updatedOrder = _ordersRepository.UpdateOrder(updateOrder);
            await SaveAsync();
            var result = _mapper.Map<OrderDto>(updatedOrder);
            
            return result; 
        }

        public async Task<CustomerDto> GetCustomerAsync(int id, bool trackChange = false)
        {
            var customer = await _customersRepository.GetCustomerAsync(id, trackChange);
            var result = _mapper.Map<CustomerDto>(customer);
            return result;
        }
    }
}