using BusinessLayer.Products.Models;
using DataLayer.Products.DataProvider;
using DataLayer.Products.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace BusinessLayer.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataProvider _productDataProvider;
        private readonly IDistributedCache _cache;

        public ProductService(
            IProductDataProvider productDataProvider,
            IDistributedCache cache)
        {
            _productDataProvider = productDataProvider;
            _cache = cache;
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
            var cacheKey = "allproducts";
            var encodedProducts = await _cache.GetAsync(cacheKey);
            List<Product>? products;
            string serializedProducts;

            if (encodedProducts is null)
            {
                products = await _productDataProvider.GetAll();
                serializedProducts = JsonSerializer.Serialize(products);
                encodedProducts = Encoding.UTF8.GetBytes(serializedProducts);
                var options = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await _cache.SetAsync(cacheKey, encodedProducts, options);
            }
            else
            {
                serializedProducts = Encoding.UTF8.GetString(encodedProducts);
                products = JsonSerializer.Deserialize<List<Product>>(serializedProducts);
            }

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