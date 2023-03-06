namespace SearchApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public  Order Order { get; set; }

    }
}
