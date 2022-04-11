using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Product;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly Serilog.ILogger _logger;

        public ProductsController(
            IProductService productService,
            Serilog.ILogger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddProductRequest request)
        {
            try
            {
                await _productService.AddNew(new()
                {
                    Name = request.Name,
                    Description = request.Description
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"AddNewProduct failed: {ex.Message}");
                throw new Exception(ex.Message);
            }
            return NoContent();
        }
    }
}