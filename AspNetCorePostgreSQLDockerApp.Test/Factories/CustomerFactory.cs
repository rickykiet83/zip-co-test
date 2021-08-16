using System;
using System.Collections.Generic;
using AspNetCorePostgreSQLDockerApp.Models;
using Bogus;
using Bogus.DataSets;

namespace AspNetCorePostgreSQLDockerApp.Test.Factories
{
    public class CustomerFactory
    {
        private static readonly List<string> _genders = new List<string>{"Male", "Female"};

        public static readonly Faker<Customer> Customer = new Faker<Customer>()
            .StrictMode(true)
            .RuleFor(o => o.Id, 0)
            .RuleFor(o => o.Email, f => f.Person.Email)
            .RuleFor(o => o.FirstName, f => f.Person.FirstName)
            .RuleFor(o => o.LastName, f => f.Person.LastName)
            .RuleFor(o => o.Gender, f => f.PickRandom<string>(_genders))
            .RuleFor(o => o.State, (f, state) => new State
            {
                Id = 0,
                Abbreviation = f.Address.State(),
                Name = f.Address.State(),
            })
            .RuleFor(o => o.Zip, f => f.Address.ZipCode().GetHashCode())
            .RuleFor(o => o.Address, f => f.Address.StreetAddress())
            .RuleFor(o => o.City, f => f.Address.City())
            .RuleFor(o => o.Orders, new List<Order>())
            .RuleFor(o => o.OrderCount, 0)
            ;
    }
}