using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface IRepositoryBase<T, K> where T : IEntity<K>
    {
        IQueryable<T> FindAll(bool trackChange = false);
        IQueryable<T> FindAll(bool trackChange, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChange = false);
        
        IQueryable<T> FindByCondition(bool trackChange, Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindByIdAsync(bool trackChange, K id, params Expression<Func<T, object>>[] includeProperties);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveAsync();
    }
}