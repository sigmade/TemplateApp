using BusinessLayer.Products.Models;
using DataLayer.Products.DataProvider;

namespace BusinessLayer.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataProvider _productDataProvider;

        public ProductService(IProductDataProvider productDataProvider)
        {
            _productDataProvider = productDataProvider;
        }

        public async Task AddNew(ProductDto product)
        {
            await _productDataProvider.SaveNew(new()
            {
                Name = product.Name,
                Description = product.Description
            });
        }
    }
}