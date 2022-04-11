using BusinessLayer.Products.Models;

namespace BusinessLayer.Products.Services
{
    public interface IProductService
    {
        Task AddNew(ProductDto product);
    }
}