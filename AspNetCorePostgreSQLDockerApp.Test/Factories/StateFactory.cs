using AspNetCorePostgreSQLDockerApp.Models;
using Bogus;

namespace AspNetCorePostgreSQLDockerApp.Test.Factories
{
    public class StateFactory
    {
        public static readonly Faker<State> State = new Faker<State>()
            .StrictMode(true)
            .RuleFor(o => o.Id, 0)
            .RuleFor(s => s.Abbreviation, f => f.Address.State())
            .RuleFor(s => s.Name, f => f.Address.State())
            ;
    }
}