using System.Collections.Generic;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Models
{
    public class DockerCommand : DomainEntity<int>
    {
        public string Command { get; set; }
        public string Description { get; set; }
        public List<DockerCommandExample> Examples { get; set; }
    }
}