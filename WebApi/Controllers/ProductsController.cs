using BusinessLayer.Products.Models;
using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
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
        private readonly ErrorHandler _error;
        private readonly IOptionsMonitor<ProductSwitchers> _productSwitchers;
        public ProductsController(
            IProductService productService,
            ErrorHandler error,
            IOptionsMonitor<ProductSwitchers> productSwitchers)
        {
            _productService = productService;
            _error = error;
            _productSwitchers = productSwitchers;
        }

        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductResponseDto))]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductResponseDto>? products;

            if (!_productSwitchers.CurrentValue.GetServiceEnabled)
            {
                return StatusCode(503, _error.DisabledService(nameof(GetAllProducts)));
            }
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