using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Services;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.Services
{
    public class OrderServiceTest
    {
        private readonly Mock<IRepositoryBase<Order, int>> _baseRepoMock;
        private readonly Mock<IOrdersRepository> _orderRepoMock;
        private readonly Mock<ICustomersRepository> _customerRepoMock;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private MapperConfiguration _mapperConfiguration;
        private readonly Mapper _mapper;
        private readonly Customer customer;

        public OrderServiceTest()
        {
            _baseRepoMock = new Mock<IRepositoryBase<Order, int>>();
            _customerRepoMock = new Mock<ICustomersRepository>();
            _orderRepoMock = new Mock<IOrdersRepository>();
            _mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile()));
            _mapper = (Mapper)_mapperConfiguration.CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            customer = CustomerFactory.Customer.Generate().AddIndexKey();
            _customerRepoMock.Setup(x => x.GetCustomerAsync(customer.Id, false))
                .ReturnsAsync(customer);
        }

        [Fact]
        public void Customer_Add_ValidOrders_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(4)
                .Select(o => o.AddCustomer(customer).AddIndexKey())
                .ToList();
            customer.AddOrders(orders);

            _orderRepoMock.Setup(x => x.CreateOrders(customer.Id, orders))
                .Returns(orders);
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.CreateOrdersAsync(customer.Id, orders).Result;
            result.Should().NotBeNull();
            result.Customer.Id.Should().Equals(customer.Id);
            result.OrderCount.Should().Equals(orders.Count);
        }
        
        [Fact]
        public void Get_CustomerOrders_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(4)
                .Select(o => o.AddCustomer(customer).AddIndexKey())
                .ToList();
            customer.AddOrders(orders);
            _customerRepoMock.Setup(x => x.GetCustomerOrdersAsync(customer.Id, false))
                .ReturnsAsync(customer);

            _orderRepoMock.Setup(x => x.CreateOrders(customer.Id, orders))
                .Returns(orders);
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.GetOrdersAsync(customer.Id).Result;
            result.Should().NotBeNull();
            result.Customer.Id.Should().Equals(customer.Id);
            result.OrderCount.Should().Equals(orders.Count);
        }
        
        [Fact]
        public void Cancel_Order_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(1)
                .Select(o => o.AddIndexKey().AddCustomer(customer)).ToList();
            customer.AddOrders(orders);
            orders.ElementAt(0).Status = EOrderStatus.Cancelled;
            var cancelOrder = orders.ElementAt(0);

            _orderRepoMock.Setup(x => x.GetOrderAsync(cancelOrder.Id, false))
                .ReturnsAsync(cancelOrder);
            _orderRepoMock.Setup(x => x.CancelOrder(cancelOrder))
                .Returns(cancelOrder);
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.CancelOrderAsync(cancelOrder.Id).Result;
            result.Should().NotBeNull();
            result.Status.Should().Equals(EOrderStatus.Cancelled);
        }

        [Fact]
        public void Update_Order_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(1)
                .Select(o => o.AddIndexKey().AddCustomer(customer)).ToList();
            customer.AddOrders(orders);
            var updateOrder = orders.ElementAt(0);
            updateOrder.Product = "Test";
            updateOrder.Price = 1;
            updateOrder.Quantity = 1;
            updateOrder.Status = EOrderStatus.Delivered;

            _orderRepoMock.Setup(x => x.GetOrderAsync(updateOrder.Id, true))
                .ReturnsAsync(updateOrder);
            _orderRepoMock.Setup(x => x.UpdateOrder(It.IsAny<Order>()))
                .Returns(updateOrder);
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.UpdateOrderAsync(updateOrder.ToUpdateDto(customer.Id)).Result;
            result.Should().NotBeNull();
            result.Id.Should().Equals(updateOrder.Id);
            result.Product.Should().Equals(updateOrder.Product);
            result.Price.Should().Equals(updateOrder.Price);
            result.Status.Should().Equals(updateOrder.Status);
        }
        
        [Fact]
        public void Get_Customer_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(1)
                .Select(o => o.AddIndexKey().AddCustomer(customer)).ToList();
            customer.AddOrders(orders);

            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.GetCustomerAsync(customer.Id).Result;
            result.Should().NotBeNull();
            result.Id.Should().Equals(customer.Id);
        }

        #region Failed Test Cases

        [Fact]
        public void Customer_Add_InValidOrders_Result_Failed()
        {
            var orders = OrderFactory.Order.Generate(1)
                .Select(o => o.AddIndexKey().AddCustomer(customer)).ToList();
            customer.AddOrders(orders);
            orders.ElementAt(0).Product = null;

            _orderRepoMock.Setup(x => x.CreateOrders(customer.Id, orders));
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.CreateOrdersAsync(customer.Id, orders).Result;
            result.Customer.Should().BeNull();
            result.Orders.Should().BeEmpty();
        }

        [Theory]
        [InlineData(0)]
        public void Get_InvalidId_Result_Null(int id)
        {
            var orders = OrderFactory.Order.Generate(1)
                .Select(o => o.AddIndexKey().AddCustomer(customer)).ToList();
            customer.AddOrders(orders);
            _orderRepoMock.Setup(x => x.CreateOrders(customer.Id, orders));
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.GetOrderAsync(id, false).Result;
            result.Should().BeNull();
        }


        #endregion
        

    }
}