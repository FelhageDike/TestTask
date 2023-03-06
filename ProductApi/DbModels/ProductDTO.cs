namespace ProductApi.DbModels
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

    }
}
