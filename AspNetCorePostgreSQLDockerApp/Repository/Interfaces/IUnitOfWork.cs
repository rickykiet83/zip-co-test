using System;
using System.Threading.Tasks;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call save change from db context
        /// </summary>
        void Commit();

        Task<int> CommitAsync();
    }
}