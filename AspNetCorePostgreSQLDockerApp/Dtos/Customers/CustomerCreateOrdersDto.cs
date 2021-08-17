using System.Collections.Generic;
using System.Linq;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerCreateOrdersDto
    {
        public int CustomerId { get; set; }
        public List<OrderForCreationDto> OrderDtos { get; set; }

        public int GetTotalOrderInProgress()
        {
            return OrderDtos.Count(o => o.Status.Equals(EOrderStatus.InProgress));
        }
    }
}