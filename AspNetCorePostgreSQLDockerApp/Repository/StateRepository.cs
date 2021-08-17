using AspNetCorePostgreSQLDockerApp.Models;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class StateRepository : RepositoryBase<State, int>, IStateRepository
    {
        public StateRepository(CustomersDbContext dbContext) : base(dbContext)
        {
        }
    }
}