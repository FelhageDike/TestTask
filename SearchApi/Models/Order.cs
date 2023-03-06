
using System.Security.Cryptography.X509Certificates;

namespace SearchApi.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ICollection<Product>? Products { get; set; }
        public Guid UserId { get; set; }
        public Order() 
        {
            Products = new List<Product>();
        }

    }
}
