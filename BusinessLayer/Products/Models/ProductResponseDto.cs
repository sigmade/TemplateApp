namespace BusinessLayer.Products.Models
{
    public class ProductResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
