
namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderForCreationDto : OrderForManipulationDto
    {
        public OrderForCreationDto()
        {
            
        }
        
        public OrderForCreationDto(int customerId)
        {
            CustomerId = customerId;
        }
    }
}