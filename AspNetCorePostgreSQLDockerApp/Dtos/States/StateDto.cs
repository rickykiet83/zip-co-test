using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos.States
{
    public class StateDto : DomainEntity<int>
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}