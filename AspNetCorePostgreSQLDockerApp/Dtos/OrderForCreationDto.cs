namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderForCreationDto : OrderForManipulationDto
    {
        public OrderForCreationDto(int customerId)
        {
            CustomerId = customerId;
        }
    }
}