using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
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
            
            customer = CustomerFactory.Customer.Generate();
            customer.Id = 1;
            _customerRepoMock.Setup(x => x.GetCustomerAsync(customer.Id, false))
                .ReturnsAsync(customer);
        }

        [Fact]
        public void Customer_Add_ValidOrders_Result_Success()
        {
            var orders = OrderFactory.Order.Generate(4).Select(o => o.AddCustomer(customer))
                .ToList();
            customer.AddOrders(orders);

            _orderRepoMock.Setup(x => x.CreateOrders(customer.Id, orders))
                .Returns(orders);
            
            var orderService = new OrderService(_baseRepoMock.Object, _mockUnitOfWork.Object, _mapper,
                _orderRepoMock.Object, _customerRepoMock.Object);

            var result = orderService.CreateOrdersAsync(customer.Id, orders).Result;
            result.Should().NotBeNull();
        }
        
    }
}