using DataLayer.Products.Models;
using System.Text.Json;

namespace DataLayer.Products.DataProviders
{
    public class StubProductDataProvider : IProductDataProvider
    {
        private static readonly string _filePath = "./StubData/StubDataLayer.json";

        public async Task<List<Product>> GetAll()
        {
            var content = "";

            using (var reader = new StreamReader(_filePath))
            {
                content = reader.ReadToEnd();
            }

            var data = JsonSerializer.Deserialize<RootStubModel>(content);

            return data.Products;
        }

        public async Task SaveNew(Product product)
        {
        }
    }
}