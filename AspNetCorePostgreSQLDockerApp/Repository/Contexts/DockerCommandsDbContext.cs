using AspNetCorePostgreSQLDockerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class DockerCommandsDbContext : DbContext
    {
        public DockerCommandsDbContext(DbContextOptions<DockerCommandsDbContext> options) : base(options)
        {
        }

        public DbSet<DockerCommand> DockerCommands { get; set; }
    }
}