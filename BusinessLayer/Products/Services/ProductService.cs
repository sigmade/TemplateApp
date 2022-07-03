using BusinessLayer.Products.Models;
using DataLayer.Products.DataProvider;

namespace BusinessLayer.Products.Services
{
    public sealed class ProductService : IProductService
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

        public async Task<List<ProductResponseDto>> GetAll()
        {
            var products = await _productDataProvider.GetAll();
            return products
                .Select(p => new ProductResponseDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate
                }).ToList();
        }
    }
}