using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AutoMapper;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int customerId, bool trackChanges = false)
        {
            var orders = await _orderRepository.GetOrdersAsync(customerId, trackChanges);
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            
            return result;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId, bool trackChanges = false)
        {
            var order = await _orderRepository.GetOrderAsync(orderId, trackChanges);
            if (order == null) return null;
            
            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderDto>> CreateOrdersAsync(int customerId, List<Order> orders)
        {
            var addedOrders = await _orderRepository.CreateOrdersAsync(customerId, orders);
            await SaveAsync();
            var result = _mapper.Map<IEnumerable<OrderDto>>(addedOrders);
            
            return result;
        }

        public async Task<OrderDto> CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null) return null;
            
            var canceledOrder = await _orderRepository.CancelOrderAsync(order);
            await SaveAsync();
            var result = _mapper.Map<OrderDto>(canceledOrder);
            
            return result; 
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderForUpdateDto orderDto)
        {
            var order = await _orderRepository.GetOrderAsync(orderDto.Id);
            if (order == null) return null;
            
            var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
            await SaveAsync();
            var result = _mapper.Map<OrderDto>(updatedOrder);
            
            return result; 
        }

        public Task<int> SaveAsync()
        {
            return _unitOfWork.CommitAsync();
        }
    }
}