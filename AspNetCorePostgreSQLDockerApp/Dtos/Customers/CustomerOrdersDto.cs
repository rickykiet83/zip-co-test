using System.Collections.Generic;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerOrdersDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}