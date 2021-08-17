using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderForManipulationDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public EOrderStatus Status { get; set; }
    }
}