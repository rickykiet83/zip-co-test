using System.Collections.Generic;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerOrdersDto
    {
        public CustomerDto Customer { get; set; } = new CustomerDto();
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public int CustomerId => Customer.Id;
        public int OrderCount => Orders.Count;
    }
}