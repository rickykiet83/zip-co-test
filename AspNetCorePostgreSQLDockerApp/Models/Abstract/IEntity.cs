namespace AspNetCorePostgreSQLDockerApp.Models.Abstract
{
    public interface IEntity : IEntity<long>
    {
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}