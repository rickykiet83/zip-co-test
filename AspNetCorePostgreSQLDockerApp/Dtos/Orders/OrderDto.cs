using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderDto : IEntity<int>
    {
        public OrderDto()
        {
            
        }

        public OrderDto(int customerId)
        {
            CustomerId = customerId;
        }
        
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        
        public EOrderStatus Status { get; set; }
    }
}