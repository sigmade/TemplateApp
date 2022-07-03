namespace DataLayer.Products.Models
{
    public sealed class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}