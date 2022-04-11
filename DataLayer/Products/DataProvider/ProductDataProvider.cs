using DataLayer.EF;
using DataLayer.Products.Models;

namespace DataLayer.Products.DataProvider
{
    public class ProductDataProvider : IProductDataProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductDataProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveNew(Product product)
        {
            product.CreatedDate = DateTimeOffset.UtcNow;
            product.IsDeleted = false;

            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}