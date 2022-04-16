using BusinessLayer.Products.Models;
using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models.Product;
using WebApi.monitoring.Errors;
using WebApi.monitoring.Switchers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOptionsMonitor<ProductSwitchers> _productSwitchers;
        private readonly ErrorHandler _error;

        public ProductsController(
            IProductService productService,
            IOptionsMonitor<ProductSwitchers> productSwitchers,
            ErrorHandler error)
        {
            _productService = productService;
            _productSwitchers = productSwitchers;
            _error = error;
        }

        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddNewProduct(AddProductRequest request)
        {
            if (!_productSwitchers.CurrentValue.AddServiceEnabed)
            {
                return StatusCode(503, _error.DisabledService(nameof(AddNewProduct)));
            }
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
                return StatusCode(500, _error.DefaultHandle(nameof(AddNewProduct), ex));
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [ProducesResponseType(200, Type = typeof(ProductResponseDto))]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!_productSwitchers.CurrentValue.GetServiceEnabled)
            {
                return StatusCode(503, _error.DisabledService(nameof(GetAllProducts)));
            }
            List<ProductResponseDto>? products;
            try
            {
                products = await _productService.GetAll();
            }
            catch (Exception ex)
            {
                return StatusCode(500, _error.DefaultHandle(nameof(GetAllProducts), ex));
            }
            return Ok(products);
        }
    }
}