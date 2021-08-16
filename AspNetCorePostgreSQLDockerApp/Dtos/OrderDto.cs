namespace AspNetCorePostgreSQLDockerApp.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
    }
}