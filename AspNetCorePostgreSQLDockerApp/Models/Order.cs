using System.ComponentModel.DataAnnotations.Schema;
using AspNetCorePostgreSQLDockerApp.Models.Abstract;

namespace AspNetCorePostgreSQLDockerApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        public EOrderStatus Status { get; set; }
        
        public int CustomerId { get; set; }
        
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}