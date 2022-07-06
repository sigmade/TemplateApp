using DataLayer.Products.Models;

namespace DataLayer.Products.DataProviders
{
    public interface IProductDataProvider
    {
        Task SaveNew(Product product);

        Task<List<Product>> GetAll();
    }
}