using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T: class
    {
        private readonly CustomersDbContext _dbContext;
        
        public RepositoryBase(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges
                ? _dbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : _dbContext.Set<T>()
                    .Where(expression);
        
        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
        
        public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}