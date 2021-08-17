using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Helpers;
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
            public const string CreateOrders = nameof(CreateOrders);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string CancelOrder = nameof(CancelOrder);
        } 
        
        [HttpGet(Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrders([Required] int customerId)
        {
            var orders = await _orderRepository.GetOrdersAsync(customerId);
            if (orders == null) return NotFound();

            var ordersToReturn = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return new OkObjectResult(ordersToReturn);
        }
        
        [HttpGet("{orderId}", Name = RouteNames.GetOrder)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrder([Required] int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null) return NotFound();

            var orderToReturn = _mapper.Map<OrderDto>(order);

            return new OkObjectResult(orderToReturn);
        }
        
        [HttpPost(Name = RouteNames.CreateOrders)]
        [ApiValidationFilter]
        [ProducesResponseType(typeof(List<OrderForCreationDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<OrderForCreationDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateOrders([Required] int customerId, [FromBody]CustomerCreateOrdersDto ordersDto)
        {
            var customer = await _customersRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                _logger.LogInformation($"Customer with orderId: {customer} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntities = _mapper.Map<IEnumerable<Order>>(ordersDto.OrderDtos);
            await _orderRepository.CreateOrdersAsync(customerId, orderEntities.ToList());
            var ordersToReturn = _mapper.Map<IEnumerable<OrderDto>>(orderEntities);

            return StatusCode(StatusCodes.Status201Created, ordersToReturn);
        }
        
        [HttpPut("{orderId}",Name = RouteNames.UpdateOrder)]
        [ApiValidationFilter]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOrder([Required] int customerId, [Required] int orderId, [FromBody]OrderForUpdateDto orderDto)
        {
            var orderEntity = await _orderRepository.GetOrderAsync(orderDto.Id);
            if (orderEntity == null)
            {
                _logger.LogInformation($"Customer with orderId: {orderDto.Id} doesn't exist in the database.");
                return NotFound();
            }

            var order = _mapper.Map<Order>(orderDto);
            order = await _orderRepository.UpdateOrderAsync(order);
            var ordersToReturn = _mapper.Map<OrderDto>(order);

            return StatusCode(StatusCodes.Status200OK, ordersToReturn);
        }
        
        [HttpGet("{orderId}/cancel", Name = RouteNames.CancelOrder)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelOrder([Required] int orderId)
        {
            var orderEntity = await _orderRepository.GetOrderAsync(orderId);
            if (orderEntity == null)
            {
                _logger.LogInformation($"Customer with orderId: {orderId} doesn't exist in the database.");
                return NotFound();
            }

            await _orderRepository.CancelOrderAsync(orderEntity);

            return NoContent();
        }

    }
}