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

        public static Order AddCustomer(this Order order, Customer customer)
        {
            order.Customer = customer;
            order.CustomerId = customer.Id;
            return order;
        }

        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Price = order.Price,
                Product = order.Product,
                Quantity = order.Quantity,
                CustomerId = order.CustomerId
            };
        }
    }
}