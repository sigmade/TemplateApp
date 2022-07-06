using BusinessLayer.Products.Models;
using System.Text.Json;

namespace BusinessLayer.Products.Services
{
    public class StubProductService : IProductService
    {
        private static readonly string _filePath = "./StubData/StubBusinessLayer.json";

        public async Task AddNew(ProductDto product)
        {
        }

        public async Task<List<ProductResponseDto>> GetAll()
        {
            var content = "";

            using (var reader = new StreamReader(_filePath))
            {
                content = reader.ReadToEnd();
            }

            var data = JsonSerializer.Deserialize<RootStubModel>(content);

            return data.Products;
        }
    }
}