namespace AspNetCorePostgreSQLDockerApp.Models.Abstract
{
    public class DomainEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        
        // True if domain has an identity
        public bool IsTransient()
        {
            return Id.Equals(default(TKey));
        }
    }
}