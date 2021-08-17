using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderForUpdateDto : OrderForManipulationDto, IEntity<int>
    {
        public OrderForUpdateDto()
        {
            
        }
        
        public OrderForUpdateDto(int customerId)
        {
            CustomerId = customerId;
        }
        public int Id { get; set; }
    }
}