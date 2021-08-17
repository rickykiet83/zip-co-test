using System.Collections.Generic;
using System.Linq;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.Repositories
{
    public class CustomerRepositoryTest
    {
        private readonly CustomersDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerFactory _logger;
        private readonly IStateRepository _stateRepository;

        public CustomerRepositoryTest()
        {
            _context = ContextFactory.Create();
            _context.Database.EnsureCreated();
            _unitOfWork = new UnitOfWork(_context);
            _logger = new LoggerFactory();
            _stateRepository = new StateRepository(_context);
        }

        [Fact]
        public void Constructor_Should_Success_When_Create_Repository()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            repository.Should().NotBeNull();
        }

        [Fact]
        public void Add_Should_Have_Record_When_Insert()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customer = CustomerFactory.Customer.Generate();
            repository.Create(customer);
            _unitOfWork.Commit();
            customer.Should().NotBeNull();
            customer.Id.Should().NotBe(0);
        }

        [Fact]
        public void FindAll_Should_Return_All_Record_In_Table()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customers = CustomerFactory.Customer
                .Generate(10);
            
            foreach (var customer in customers)
            {
                repository.Create(customer);
            }
            
            _unitOfWork.Commit();

            List<Customer> results = repository.FindAll(true).ToList();
            results.Should().NotBeNull();
            results.Count.Should().Equals(10);
        }

        [Fact]
        public void FindById_Should_Return_True_Record_In_Table()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customer = CustomerFactory.Customer.Generate();
            repository.Create(customer);
            _unitOfWork.Commit();

            var result = repository.FindByCondition(x => x.Id.Equals(customer.Id), false).SingleOrDefault();
            result.Should().NotBeNull();
            result.Id.Should().Equals(customer.Id);
        }

        [Fact]
        public void Update_Should_Have_Change_Record()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customer = CustomerFactory.Customer.Generate();
            repository.Create(customer);
            _unitOfWork.Commit();

            var updateCustomer = repository.FindByCondition(x => x.Id.Equals(customer.Id), true).SingleOrDefault();
            updateCustomer.FirstName = "Test";
            repository.Update(updateCustomer);

            var result = repository.FindByCondition(x => x.Id.Equals(customer.Id), true).SingleOrDefault();
            result.Should().NotBeNull();
            result.Id.Should().Equals(customer.Id);
            result.FirstName.Should().Equals(updateCustomer.FirstName);
        }

        [Fact]
        public void Remove_Should_Success_When_Pass_Valid_Id()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customer = CustomerFactory.Customer.Generate();
            repository.Create(customer);
            _unitOfWork.Commit();

            repository.Delete(customer);
            _unitOfWork.Commit();

            var result = repository.FindByCondition(x => x.Id.Equals(customer.Id), false).SingleOrDefault();
            result.Should().BeNull();
        }

        [Fact]
        public void FindSingle_Should_Return_One_Record_If_Condition_Is_Match()
        {
            CustomersRepository repository = new CustomersRepository(_context, _logger, _stateRepository);
            var customer = CustomerFactory.Customer.Generate();
            customer.Email = "test@gmail.com";
            repository.Create(customer);
            _unitOfWork.Commit();

            var result = repository.FindByCondition(x => x.Email.Equals(customer.Email), false).SingleOrDefault();
            result.Should().NotBeNull();
            result.Email.Should().Equals(customer.Email);
        }

        [Fact]
        public void FindAll_States_Should_Return_All_Record_In_Table()
        {
            var states = StateFactory.State.Generate(10);
            foreach (var state in states)
            {
                _stateRepository.Create(state);
            }
            _unitOfWork.Commit();

            var result = _stateRepository.FindAll().ToList();
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Equals(10);
        }
    }
}