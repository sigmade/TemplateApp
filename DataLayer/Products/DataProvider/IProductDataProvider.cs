using DataLayer.Products.Models;

namespace DataLayer.Products.DataProvider
{
    public interface IProductDataProvider
    {
        Task SaveNew(Product product);

        Task<List<Product>> GetAll();
    }
}