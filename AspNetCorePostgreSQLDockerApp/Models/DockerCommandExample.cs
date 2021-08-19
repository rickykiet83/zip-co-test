using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Models
{
    public class DockerCommandExample : DomainEntity<int>
    {
        public string Example { get; set; }
        public string Description { get; set; }
    }
}