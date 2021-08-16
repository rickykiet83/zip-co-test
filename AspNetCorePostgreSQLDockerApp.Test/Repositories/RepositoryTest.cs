using System.Collections.Generic;
using System.Linq;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using FluentAssertions;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.Repositories
{
    public class RepositoryTest
    {
        private readonly CustomersDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryTest()
        {
            _context = ContextFactory.Create();
            _context.Database.EnsureCreated();
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public void Constructor_Should_Success_When_Create_Repository()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            repositoryBase.Should().NotBeNull();
        }

        [Fact]
        public void Add_Should_Have_Record_When_Insert()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customer = CustomerFactory.Customer.Generate();
            repositoryBase.Create(customer);
            _unitOfWork.Commit();
            customer.Should().NotBeNull();
            customer.Id.Should().NotBe(0);
        }

        [Fact]
        public void FindAll_Should_Return_All_Record_In_Table()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customers = CustomerFactory.Customer.Generate(10);
            foreach (var customer in customers)
            {
                repositoryBase.Create(customer);
            }

            _unitOfWork.Commit();
            List<Customer> results = repositoryBase.FindAll(false).ToList();
            results.Should().NotBeNull();
            results.Count.Should().Equals(10);
        }

        [Fact]
        public void FindById_Should_Return_True_Record_In_Table()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customer = CustomerFactory.Customer.Generate();
            repositoryBase.Create(customer);
            _unitOfWork.Commit();

            var result = repositoryBase.FindByCondition(x => x.Id.Equals(customer.Id), false).SingleOrDefault();
            result.Should().NotBeNull();
            result.Id.Should().Equals(customer.Id);
        }

        [Fact]
        public void Update_Should_Have_Change_Record()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customer = CustomerFactory.Customer.Generate();
            repositoryBase.Create(customer);
            _unitOfWork.Commit();

            var updateCustomer = repositoryBase.FindByCondition(x => x.Id.Equals(customer.Id)).SingleOrDefault();
            updateCustomer.FirstName = "Test";
            repositoryBase.Update(updateCustomer);
            _unitOfWork.Commit();

            var result = repositoryBase.FindByCondition(x => x.Id.Equals(customer.Id), false).SingleOrDefault();
            result.Should().NotBeNull();
            result.Id.Should().Equals(customer.Id);
            result.FirstName.Should().Equals(updateCustomer.FirstName);
        }

        [Fact]
        public void Remove_Should_Success_When_Pass_Valid_Id()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customer = CustomerFactory.Customer.Generate();
            repositoryBase.Create(customer);
            _unitOfWork.Commit();
            
            repositoryBase.Delete(customer);
            _unitOfWork.Commit();
            
            var result = repositoryBase.FindByCondition(x => x.Id.Equals(customer.Id), false).SingleOrDefault();
            result.Should().BeNull();
        }

        [Fact]
        public void FindSingle_Should_Return_One_Record_If_Condition_Is_Match()
        {
            RepositoryBase<Customer> repositoryBase = new RepositoryBase<Customer>(_context);
            var customer = CustomerFactory.Customer.Generate();
            customer.Email = "test@gmail.com";
            repositoryBase.Create(customer);
            _unitOfWork.Commit();

            var result = repositoryBase.FindByCondition(x => x.Email.Equals(customer.Email), false).SingleOrDefault();
            result.Should().NotBeNull();
            result.Email.Should().Equals(customer.Email);
        }
}
}