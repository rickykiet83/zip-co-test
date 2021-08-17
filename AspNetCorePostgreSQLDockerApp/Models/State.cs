using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Models
{
    public class State : DomainEntity<int>
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}