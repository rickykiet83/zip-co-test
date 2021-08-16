using System.Linq;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.Repositories
{
    public class OrderRepositoryTest
    {
        private readonly CustomersDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerFactory _logger;
        private readonly IStateRepository _stateRepository;
        
        public OrderRepositoryTest()
        {
            _context = ContextFactory.Create();
            _context.Database.EnsureCreated();
            _unitOfWork = new UnitOfWork(_context);
            _logger = new LoggerFactory();
            _stateRepository = new StateRepository(_context);
        }

        [Fact]
        public async Task Add_Valid_Order_Result_Success()
        {
            var customer = CustomerFactory.Customer.Generate();
            CustomersRepository customersRepository = new CustomersRepository(_context, _logger, _stateRepository);
            await customersRepository.InsertCustomerAsync(customer);
            
            var orders = OrderFactory.Order.Generate(4).Select(o => o.AddCustomer(customer))
                .ToList();
            customer.Orders = orders;
            OrderRepository orderRepository = new OrderRepository(_context, _logger);
            foreach (var order in orders)
            {
                await orderRepository.InsertOrderAsync(order);
            }

            var result = await orderRepository.GetOrdersAsync(customer.Id);
            result.Should().NotBeNullOrEmpty();
            result.ElementAt(0).CustomerId.Should().Equals(customer.Id);
        }
        
        [Fact]
        public async Task Cancel_Order_Result_Success()
        {
            var customer = CustomerFactory.Customer.Generate();
            CustomersRepository customersRepository = new CustomersRepository(_context, _logger, _stateRepository);
            await customersRepository.InsertCustomerAsync(customer);
            
            var orders = OrderFactory.Order.Generate(1).Select(o => o.AddCustomer(customer))
                .ToList();
            orders.ElementAt(0).Status = EOrderStatus.InProgress;
            customer.Orders = orders;
            OrderRepository orderRepository = new OrderRepository(_context, _logger);
            foreach (var order in orders)
            {
                await orderRepository.InsertOrderAsync(order);
            }

            var cancelOrder = orders.ElementAt(0);
            var result = await orderRepository.CancelOrderAsync(cancelOrder);

            result.Should().NotBeNull();
            result.Status.Should().Equals(EOrderStatus.Cancelled);
        }
        
        [Fact]
        public async Task Update_Order_Result_Success()
        {
            var customer = CustomerFactory.Customer.Generate();
            CustomersRepository customersRepository = new CustomersRepository(_context, _logger, _stateRepository);
            await customersRepository.InsertCustomerAsync(customer);
            
            var orders = OrderFactory.Order.Generate(1).Select(o => o.AddCustomer(customer))
                .ToList();
            customer.Orders = orders;
            OrderRepository orderRepository = new OrderRepository(_context, _logger);
            foreach (var order in orders)
            {
                await orderRepository.InsertOrderAsync(order);
            }

            var updateOrder = orders.ElementAt(0);
            updateOrder.Product = "Test";
            updateOrder.Price = 1;
            updateOrder.Quantity = 1;
            updateOrder.Status = EOrderStatus.Delivered;

            var result = await orderRepository.UpdateOrderAsync(updateOrder);
            result.AddCustomer(customer);
            
            result.Should().NotBeNull();
            result.Should().BeSameAs(updateOrder);
        }
    }
}