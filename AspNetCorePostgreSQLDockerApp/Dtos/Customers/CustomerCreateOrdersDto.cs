using System.Collections.Generic;
using System.Linq;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class CustomerCreateOrdersDto
    {
        public int CustomerId { get; set; }
        public List<OrderForCreationDto> Orders { get; set; }

        public int GetTotalOrderInProgress()
        {
            return Orders.Count(o => o.Status.Equals(EOrderStatus.InProgress));
        }
    }
}