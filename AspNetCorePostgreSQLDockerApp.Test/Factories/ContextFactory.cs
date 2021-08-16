using System;
using AspNetCorePostgreSQLDockerApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Test.Factories
{
    public class ContextFactory
    {
        public static CustomersDbContext Create()
        {
            DbContextOptions<CustomersDbContext> options = new DbContextOptionsBuilder<CustomersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new CustomersDbContext(options);
            return context;
        }
    }
}