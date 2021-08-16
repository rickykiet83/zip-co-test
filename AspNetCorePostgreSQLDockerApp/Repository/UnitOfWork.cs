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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}