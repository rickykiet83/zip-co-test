using System.Threading.Tasks;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomersDbContext _context;
        
        public UnitOfWork(CustomersDbContext context)
        {
            _context = context;
        }
        
        public void Commit()
        {
            _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}