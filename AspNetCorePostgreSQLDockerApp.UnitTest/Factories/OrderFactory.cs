using System;
using AspNetCorePostgreSQLDockerApp.Dtos;
using Bogus;

namespace AspNetCorePostgreSQLDockerApp.UnitTest.Factories
{
    public class OrderFactory
    {
        public static readonly Faker<OrderDto> OrderDto = new Faker<OrderDto>()
            .StrictMode(true)
            .RuleFor(o => o.Id, f => f.Random.Number(1, 100))
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, Decimal.MaxValue))
            .RuleFor(o => o.Product, f => f.Commerce.Product())
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10));
    }
}