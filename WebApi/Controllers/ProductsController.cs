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

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddProductRequest request)
        {
            await _productService.AddNew(new()
            {
                Name = request.Name,
                Description = request.Description
            });

            return NoContent();
        }
    }
}