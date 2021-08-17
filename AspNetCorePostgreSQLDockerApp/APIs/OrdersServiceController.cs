using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Apis
{
    [Route("api/customers/{customerId}/orders")]
    public class OrdersServiceController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly ILogger<OrdersServiceController> _logger;
        private readonly IMapper _mapper;
        
        public OrdersServiceController(IOrderRepository orderRepository, IMapper mapper, ICustomersRepository customersRepository, ILogger<OrdersServiceController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _customersRepository = customersRepository;
            _logger = logger;
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
            public const string GetOrder = nameof(GetOrder);
            public const string CreateOrder = nameof(CreateOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string CancelOrder = nameof(CancelOrder);
        } 
        
        [HttpGet(Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrders([Required] int customerId)
        {
            var orders = await _orderRepository.GetOrdersAsync(customerId);
            if (orders == null) return NotFound();

            var ordersToReturn = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(ordersToReturn);
        }
        
        [HttpGet("{orderId}", Name = RouteNames.GetOrder)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrder([Required] int orderId)
        {
            var orders = await _orderRepository.GetOrderAsync(orderId);
            if (orders == null) return NotFound();

            return Ok(orders);
        }
        
        [HttpPost(Name = RouteNames.CreateOrder)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateOrders([Required] int customerId, OrderDto orderDto)
        {
            var customer = await _customersRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                _logger.LogInformation($"Customer with id: {customer} doesn't exist in the database.");
                return NotFound();
            }
            orderDto.CustomerId = customerId;
            var orderEntity = _mapper.Map<Order>(orderDto);
            await _orderRepository.InsertOrderAsync(orderEntity);
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);

            return CreatedAtRoute(RouteNames.GetOrder, new { customerId, orderId = orderToReturn.Id }, orderToReturn);
        }
        
        [HttpPut(Name = RouteNames.UpdateOrder)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOrder(OrderDto orderDto)
        {
            var orderEntity = await _orderRepository.GetOrderAsync(orderDto.Id.Value);
            if (orderEntity == null)
            {
                _logger.LogInformation($"Customer with id: {orderDto.Id} doesn't exist in the database.");
                return NotFound();
            }

            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.UpdateOrderAsync(order);

            return NoContent();
        }
        
        [HttpGet("{orderId}/cancel", Name = RouteNames.CancelOrder)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelOrder([Required] int orderId)
        {
            var orderEntity = await _orderRepository.GetOrderAsync(orderId);
            if (orderEntity == null)
            {
                _logger.LogInformation($"Customer with id: {orderId} doesn't exist in the database.");
                return NotFound();
            }

            await _orderRepository.CancelOrderAsync(orderEntity);

            return NoContent();
        }

    }
}