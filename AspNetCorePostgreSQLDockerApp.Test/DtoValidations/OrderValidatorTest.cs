using System.Collections.Generic;
using System.Linq;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Test.Factories;
using AspNetCorePostgreSQLDockerApp.Validations;
using Bogus;
using FluentAssertions;
using Xunit;

namespace AspNetCorePostgreSQLDockerApp.Test.DtoValidations
{
    public class OrderValidatorTest
    {
        private OrderCreateValidator _validatorCreate;
        private CustomerCreateOrdersValidator _validatorCustomerCreateOrders;
        private OrderUpdateValidator _validatorUpdate;
        private readonly OrderForCreationDto _orderDto;
        private readonly CustomerCreateOrdersDto _customerCreateOrdersDto;

        public OrderValidatorTest()
        {
            _customerCreateOrdersDto = CustomerFactory.Customer.Generate()
                .AddIndexKey()
                .ToCreateOrderDtos();
            _orderDto = OrderFactory.Order.Generate().ToCreateDto(_customerCreateOrdersDto.CustomerId);
        }

        [Fact]
        public void Should_Valid_Result_When_Valid_Request()
        {
            var orders = OrderFactory.Order.Generate(4)
                .Select(o => o.ToCreateDto(_customerCreateOrdersDto.CustomerId)).ToList();
            _customerCreateOrdersDto.OrderDtos = orders;
            _validatorCustomerCreateOrders = new CustomerCreateOrdersValidator();
            var result = _validatorCustomerCreateOrders.Validate(_customerCreateOrdersDto);
            result.IsValid.Should().BeTrue();
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_Miss_CustomerId()
        {
            _orderDto.CustomerId = 0;
            _validatorCreate = new OrderCreateValidator();
            var result = _validatorCreate.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("is required");
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_Miss_ProductName()
        {
            _orderDto.Product = string.Empty;
            _validatorCreate = new OrderCreateValidator();
            var result = _validatorCreate.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("is required");
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_ProductName_Length_GreaterThan_150()
        {
            _orderDto.Product = new string(new Faker().Random.Chars(count: 151));
            _validatorCreate = new OrderCreateValidator();
            var result = _validatorCreate.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("maximum length should be 150");
        }
        
        [Fact]
        public void Should_Error_Result_When_Orders_IsEmpty()
        {
            _validatorCustomerCreateOrders = new CustomerCreateOrdersValidator();
            _customerCreateOrdersDto.OrderDtos = new List<OrderForCreationDto>();
            var result = _validatorCustomerCreateOrders.Validate(_customerCreateOrdersDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("is required");
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Error_Result_When_Dto_Quantity_LessThan_1(int quantity)
        {
            _orderDto.Quantity = quantity;
            _validatorCreate = new OrderCreateValidator();
            var result = _validatorCreate.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault().ErrorMessage.Should().Contain(">= 1");
        }
        
        [Theory]
        [InlineData(-1)]
        public void Should_Error_Result_When_Dto_Price_Invalid(decimal price)
        {
            _orderDto.Price = price;
            _validatorCreate = new OrderCreateValidator();
            var result = _validatorCreate.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault().ErrorMessage.Should().Contain(">= 0");
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Error_Result_When_Update_Dto_Without_Id(int id)
        {
            _validatorUpdate = new OrderUpdateValidator();
            var updateOrderDto = OrderFactory.Order.Generate().ToUpdateDto(1);
            updateOrderDto.Id = id;
            var result = _validatorUpdate.Validate(updateOrderDto);
            result.IsValid.Should().BeFalse();
            var errorMessages = result.Errors.ToList();
            errorMessages.Should().Contain(errorMessages, "invalid", "required");
        }
    }
}