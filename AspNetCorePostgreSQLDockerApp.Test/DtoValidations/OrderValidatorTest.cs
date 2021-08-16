using System;
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
        private OrderCreateValidator _validator;
        private OrderDto _orderDto;

        public OrderValidatorTest()
        {
            _orderDto = OrderFactory.Order.Generate().ToDto();
            _orderDto.CustomerId = 1;
        }

        [Fact]
        public void Should_Valid_Result_When_Valid_Request()
        {
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeTrue();
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_Miss_CustomerId()
        {
            _orderDto.CustomerId = 0;
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("is required");
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_Miss_ProductName()
        {
            _orderDto.Product = string.Empty;
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("is required");
        }
        
        [Fact]
        public void Should_Error_Result_When_Dto_ProductName_Length_GreaterThan_150()
        {
            _orderDto.Product = new string(new Faker().Random.Chars(count: 151));
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.ElementAt(0).ErrorMessage.Should().Contain("maximum length should be 150");
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Error_Result_When_Dto_Quantity_LessThan_1(int quantity)
        {
            _orderDto.Quantity = quantity;
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault().ErrorMessage.Should().Contain(">= 1");
        }
        
        [Theory]
        [InlineData(-1)]
        public void Should_Error_Result_When_Dto_Price_Invalid(decimal price)
        {
            _orderDto.Price = price;
            _validator = new OrderCreateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault().ErrorMessage.Should().Contain(">= 0");
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Error_Result_When_Update_Dto_Without_Id(int id)
        {
            _orderDto.Id = id;
            _validator = new OrderUpdateValidator();
            var result = _validator.Validate(_orderDto);
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault().ErrorMessage.Should().Contain("invalid");
        }
    }
}