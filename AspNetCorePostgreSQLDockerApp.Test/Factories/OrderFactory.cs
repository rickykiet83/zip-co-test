using System;
using System.Runtime.CompilerServices;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using Bogus;

namespace AspNetCorePostgreSQLDockerApp.Test.Factories
{
    public static class OrderFactory
    {
        public static readonly Faker<Order> Order = new Faker<Order>()
            .StrictMode(true)
            .RuleFor(o => o.Id, 0)
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, Decimal.MaxValue))
            .RuleFor(o => o.Product, f => f.Commerce.Product())
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(o => o.Status, f => f.PickRandom<EOrderStatus>())
            .RuleFor(o => o.Customer, CustomerFactory.Customer.Generate())
            .RuleFor(o => o.CustomerId, 0)
            ;

        public static Order AddIndexKey(this Order order)
        {
            return Order
                .RuleFor(o => o.Id, f => f.IndexFaker);
        }
        
        public static Order AddCustomer(this Order order, Customer customer)
        {
            order.Customer = customer;
            order.CustomerId = customer.Id;
            return order;
        }

        public static OrderDto ToDto(this Order order, int customerId)
        {
            return new OrderDto(customerId)
            {
                Price = order.Price,
                Product = order.Product,
                Quantity = order.Quantity,
                Status = order.Status
            };
        }
        
        public static OrderForCreationDto ToCreateDto(this Order order, int customerId)
        {
            return new OrderForCreationDto(customerId)
            {
                Price = order.Price,
                Product = order.Product,
                Quantity = order.Quantity,
                Status = order.Status
            };
        }
        
        public static OrderForUpdateDto ToUpdateDto(this Order order, int customerId)
        {
            return new OrderForUpdateDto(customerId)
            {
                Id = order.Id,
                Price = order.Price,
                Product = order.Product,
                Quantity = order.Quantity,
                Status = order.Status
            };
        }
    }
}