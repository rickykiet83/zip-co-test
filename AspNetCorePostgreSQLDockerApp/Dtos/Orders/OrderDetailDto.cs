namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderDetailDto : OrderDto
    {
        public CustomerDto Customer { get; set; } = new CustomerDto();
        public int CustomerId => Customer.Id;
    }
}