using AspNetCorePostgreSQLDockerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<State> States { get; set; }
    }
}