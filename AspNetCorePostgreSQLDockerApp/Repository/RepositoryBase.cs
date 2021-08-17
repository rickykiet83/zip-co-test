using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class RepositoryBase<T, K> : IRepositoryBase<T, K>, IDisposable where T: DomainEntity<K>
    {
        private readonly CustomersDbContext _dbContext;
        
        public RepositoryBase(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();
        
        public IQueryable<T> FindAll(bool trackChange, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _dbContext.Set<T>();
            if (!trackChange) items.AsNoTracking();

            if (includeProperties == null) return items;
            return includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges
                ? _dbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : _dbContext.Set<T>()
                    .Where(expression);

        public async Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAll(false, includeProperties).SingleAsync(x => x.Id.Equals(id));
        }

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