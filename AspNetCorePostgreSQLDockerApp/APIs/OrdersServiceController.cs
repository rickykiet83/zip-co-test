using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Helpers;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Apis
{
    [Route("api/customers/{customerId}/orders")]
    public class OrdersServiceController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersServiceController> _logger;
        private readonly IMapper _mapper;
        
        public OrdersServiceController(IOrderService orderService, IMapper mapper, ILogger<OrdersServiceController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
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
        [ProducesResponseType(typeof(CustomerOrdersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomerOrdersDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrders([Required] int customerId)
        {
            var customer = await _orderService.GetCustomerAsync(customerId);
            if (customer == null)
            {
                _logger.LogInformation($"Customer with orderId: {customer} doesn't exist in the database.");
                return NotFound();
            }
            
            var result = await _orderService.GetOrdersAsync(customerId);
            if (result == null) return NotFound();
            
            return new OkObjectResult(result);
        }
        
        [HttpGet("{orderId}", Name = RouteNames.GetOrder)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrder([Required] int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null) return NotFound();

            return new OkObjectResult(order);
        }
        
        [HttpPost(Name = RouteNames.CreateOrders)]
        [ApiValidationFilter]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateOrders([Required] int customerId, [FromBody]CustomerCreateOrdersDto ordersDto)
        {
            var customer = await _orderService.GetCustomerAsync(customerId);
            if (customer == null)
            {
                _logger.LogInformation($"Customer with orderId: {customer} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntities = _mapper.Map<IEnumerable<Order>>(ordersDto.Orders);
            var orders = await _orderService.CreateOrdersAsync(customerId, orderEntities.ToList());
            return StatusCode(StatusCodes.Status201Created, orders.ToList());
        }
        
        [HttpPut("{orderId}",Name = RouteNames.UpdateOrder)]
        [ApiValidationFilter]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOrder([Required] int customerId, [Required] int orderId, [FromBody]OrderForUpdateDto orderDto)
        {
            var customer = await _orderService.GetCustomerAsync(customerId);
            if (customer == null)
            {
                _logger.LogInformation($"Customer with customerId: {customer.Id} doesn't exist in the database.");
                return NotFound();
            }

            var orderToReturn = await _orderService.UpdateOrderAsync(orderDto);
            if (orderToReturn != null) return StatusCode(StatusCodes.Status200OK, orderToReturn);
            _logger.LogInformation($"Order with orderId: {orderDto.Id} doesn't exist in the database.");
            return NotFound();

        }
        
        [HttpGet("{orderId}/cancel", Name = RouteNames.CancelOrder)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelOrder([Required] int orderId)
        {
            var cancelOrder = await _orderService.CancelOrderAsync(orderId);
            if (cancelOrder != null) return StatusCode(StatusCodes.Status200OK, cancelOrder);
            _logger.LogInformation($"Order with orderId: {orderId} doesn't exist in the database.");
            return NotFound();

        }

    }
}