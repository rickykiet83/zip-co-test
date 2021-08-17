namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderForUpdateDto : OrderForManipulationDto
    {
        public OrderForUpdateDto(int customerId)
        {
            CustomerId = customerId;
        }
        public int Id { get; set; }
    }
}