using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Apis;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Services;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.Controllers
{
    public class OrdersServiceControllerTest
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<ICustomersRepository> _customerRepoMock;
        private readonly Mock<ILogger<OrdersServiceController>> _loggerMock;
        private MapperConfiguration _mapperConfiguration;
        private readonly Mapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private OrderService _orderService;
        private Customer customer;
        
        public OrdersServiceControllerTest()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _customerRepoMock = new Mock<ICustomersRepository>();
            _loggerMock = new Mock<ILogger<OrdersServiceController>>();
            _mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile()));
            _mapper = (Mapper)_mapperConfiguration.CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            customer = CustomerFactory.Customer.Generate();
            customer.Id = 1;
            _customerRepoMock.Setup(x => x.GetCustomerAsync(customer.Id, false))
                .ReturnsAsync(customer);
        }
        
        [Fact]
        public void Create_Orders_ValidRequest_OkResult()
        {
            var orders = OrderFactory.Order.Generate(4)
                .Select(o => o.AddCustomer(customer))
                .Select(o => o.AddIndexKey()).ToList();
            
            _orderRepoMock.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orders);

            var mockOrderService = new Mock<IOrderService>();
            var orderDtos = orders.Select(o => o.ToDto(customer.Id));
            mockOrderService.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orderDtos);

            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);

            var orderDto = new CustomerCreateOrdersDto
            {
                CustomerId = customer.Id,
                OrderDtos = orders.Select(o => o.ToCreateDto(customer.Id)).ToList(),
            };
                
            var result = controller.CreateOrders(customer.Id, orderDto).Result;
            result.Should().BeOfType(typeof(ObjectResult));
            Assert.Equal(StatusCodes.Status201Created, (result as ObjectResult).StatusCode);
        }
        
        [Fact]
        public void Get_AllOrders_ValidRequest_OkResult()
        {
            var customer = CustomerFactory.Customer.Generate();
            customer.Id = 1;
            _customerRepoMock.Setup(x => x.GetCustomerAsync(customer.Id, false))
                .ReturnsAsync(customer);
            
            var orders = OrderFactory.Order.Generate(10)
                .Select(o => o.AddCustomer(customer));
        
            _orderRepoMock.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orders);
            
            var mockOrderService = new Mock<IOrderService>();
            var orderDtos = orders.Select(o => o.ToDto(customer.Id));
            mockOrderService.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orderDtos);
        
            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);
            var result = controller.GetOrders(customer.Id).Result;
            result.Should().BeOfType(typeof(OkObjectResult));
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }
        
        [Fact]
        public void Update_Order_ValidRequest_NoContentResult()
        {
            var order = OrderFactory.Order.Generate().AddCustomer(customer);
            order.Id = 1;
            order.Product = "Test";
            order.Price = 1;
            order.Quantity = 1;
            order.Status = EOrderStatus.Delivered;
        
            _orderRepoMock.Setup(x => x.GetOrderAsync(order.Id, false))
                .ReturnsAsync(order);
            
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.UpdateOrderAsync(It.IsAny<OrderForUpdateDto>()))
                .ReturnsAsync(order.ToDto(customer.Id));

            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);
            var result = controller.UpdateOrder(customer.Id, order.Id, order.ToUpdateDto(customer.Id)).Result;
            result.Should().BeOfType(typeof(ObjectResult));
        }
        
        [Fact]
        public void Cancel_Order_ValidRequest_NoContentResult()
        {
            var order = OrderFactory.Order.Generate().AddCustomer(customer);
            order.Id = 1;
            order.Status = EOrderStatus.Cancelled;
            
            _orderRepoMock.Setup(x => x.GetOrderAsync(order.Id, false))
                .ReturnsAsync(order);
            
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.CancelOrderAsync(order.Id))
                .ReturnsAsync(order.ToDto(customer.Id));
        
            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);
            var result = controller.CancelOrder(order.Id).Result;
            result.Should().BeOfType(typeof(ObjectResult));
            Assert.Equal(StatusCodes.Status200OK, (result as ObjectResult).StatusCode);
        }
        
        [Fact]
        public void Get_Order_InValidRequest_NotFoundResult()
        {
            var order = OrderFactory.Order.Generate().AddCustomer(customer);
            order.Id = 1;
            
            _orderRepoMock.Setup(x => x.GetOrderAsync(order.Id, false))
                .ReturnsAsync(order);
            
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.GetOrderAsync(order.Id, false))
                .ReturnsAsync(order.ToDto(customer.Id));
        
            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);
            var result = controller.GetOrder(-1).Result;
            result.Should().BeOfType(typeof(NotFoundResult));
            Assert.Equal(404, (result as NotFoundResult).StatusCode);
        }
        
        [Fact]
        public void Create_Orders_InValid_CustomerId_NotFoundResult()
        {
            var orders = OrderFactory.Order.Generate(4)
                .Select(o => o.AddCustomer(customer))
                .Select(o => o.AddIndexKey());
            
            _orderRepoMock.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orders);
            
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.GetOrdersAsync(customer.Id, false))
                .ReturnsAsync(orders.Select(o => o.ToDto(customer.Id)));
        
            var controller =
                new OrdersServiceController(mockOrderService.Object, _mapper, _customerRepoMock.Object, _loggerMock.Object);
            var orderDto = new CustomerCreateOrdersDto
            {
                CustomerId = customer.Id,
                OrderDtos = orders.Select(o => o.ToCreateDto(customer.Id)).ToList(),
            };
            var result = controller.CreateOrders(-1, orderDto).Result;
            result.Should().BeOfType(typeof(NotFoundResult));
            Assert.Equal(404, (result as NotFoundResult).StatusCode);
        }
    }
}