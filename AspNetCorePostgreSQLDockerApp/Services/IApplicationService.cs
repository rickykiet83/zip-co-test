using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public interface IApplicationService<TEntity, TEntityDto> : IApplicationService<TEntity, TEntityDto, long>
        where TEntity : IEntity<long>
        where TEntityDto : IEntity<long>
    {
    }
    
    public interface IApplicationService<TEntity, TEntityDto, in TKey>
        where TEntity : IEntity<TKey>
        where TEntityDto : IEntity<TKey>
    {
        IEnumerable<TEntityDto> GetAll(bool trackChange);
        Task<TEntityDto> GetByIdAsync(TKey id);
        Task<TEntityDto> DeleteAsync(TKey id);
        Task<int> SaveAsync();
    }
    
    public interface IApplicationService<TEntity, TEntityDto, in TEntityCreateDto, TKey> 
        : IApplicationService<TEntity, TEntityDto, TKey>
        where TEntity : IEntity<TKey>
        where TEntityDto : IEntity<TKey>
        where TEntityCreateDto : IEntity<TKey>
    {
        void Create(TEntityCreateDto input);
    }
    
    public interface IApplicationService<TEntity, TEntityDto, in TEntityCreateDto, in TEntityUpdateDto, TKey> 
        : IApplicationService<TEntity, TEntityDto, TEntityCreateDto, TKey>
        where TEntity : IEntity<TKey>
        where TEntityDto : IEntity<TKey>
        where TEntityCreateDto : IEntity<TKey>
        where TEntityUpdateDto : IEntity<TKey>
    {
        void Update(TEntityUpdateDto input, TKey id);
    }
}