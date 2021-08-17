using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;
using AspNetCorePostgreSQLDockerApp.Repository;
using AutoMapper;

namespace AspNetCorePostgreSQLDockerApp.Services
{
    public class ApplicationService<T, TDto> : ApplicationService<T, TDto, long>
        where T : IEntity<long>
        where TDto : IEntity<long>
    {
        public ApplicationService(IRepositoryBase<T, long> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(
            repository, unitOfWork, mapper)
        {
        }
    }

    public class ApplicationService<T, TDto, K> : IApplicationService<T, TDto, K>
        where T : IEntity<K>
        where TDto : IEntity<K>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IRepositoryBase<T, K> _repository;

        public ApplicationService(IRepositoryBase<T, K> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<TDto> GetAll(bool trackChange)
        {
            var entities = _repository.FindAll(trackChange);
            return _mapper.ProjectTo<TDto>(entities);
        }

        public async Task<TDto> GetByIdAsync(K id)
        {
            var entity = await _repository.FindByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> DeleteAsync(K id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new NullReferenceException();
            _repository.Delete(entity);
            return _mapper.Map<TDto>(entity);
        }

        public Task<int> SaveAsync() => _unitOfWork.CommitAsync();
    }

    public class ApplicationService<T, TDto, TCreateDto, K> : ApplicationService<T, TDto, K>,
        IApplicationService<T, TDto, TCreateDto, K>
        where T : IEntity<K>
        where TDto : IEntity<K>
    {
        public ApplicationService(IRepositoryBase<T, K> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(
            repository, unitOfWork, mapper)
        {
        }

        public void Create(TCreateDto input)
        {
            var entity = _mapper.Map<T>(input);
            _repository.Create(entity);
        }
    }

    public class ApplicationService<T, TDto, TCreateDto, TUpdateDto, K> : ApplicationService<T, TDto, TCreateDto, K>,
        IApplicationService<T, TDto, TCreateDto, TUpdateDto, K>
        where T : IEntity<K>
        where TDto : IEntity<K>
        where TUpdateDto : IEntity<K>
    {
        public ApplicationService(IRepositoryBase<T, K> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(
            repository, unitOfWork, mapper)
        {
        }

        public void Update(TUpdateDto input, K id)
        {
            var entity = _mapper.Map<T>(input);
            entity.Id = id;
            _repository.Update(entity);
        }
    }
}