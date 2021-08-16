using System;
using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using Bogus;

namespace AspNetCorePostgreSQLDockerApp.Test.Factories
{
    public class OrderFactory
    {
        public static readonly Faker<Order> OrderDto = new Faker<Order>()
            .StrictMode(true)
            .RuleFor(o => o.Id, 0)
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, Decimal.MaxValue))
            .RuleFor(o => o.Product, f => f.Commerce.Product())
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10));
    }
}