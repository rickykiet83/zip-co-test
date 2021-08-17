using System.Collections.Generic;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerCreateOrdersDto
    {
        public int CustomerId { get; set; }
        public List<OrderForCreationDto> OrderDtos { get; set; }
    }
}