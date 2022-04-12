using BusinessLayer.Products.Models;
using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Models.Product;
using WebApi.monitoring.Models;

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
        [ProducesResponseType(500)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
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
                var errorId = Error.Id();
                _logger.Error(Error.LogMessage(nameof(AddNewProduct), errorId, ex));
                return StatusCode(500, Error.Display(errorId));
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductResponseDto))]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductResponseDto>? products;

            try
            {
                products = await _productService.GetAll();
            }
            catch (Exception ex)
            {
                var errorId = Error.Id();
                _logger.Error(Error.LogMessage(nameof(AddNewProduct), errorId, ex));
                return StatusCode(500, Error.Display(errorId));
            }
            return Ok(products);
        }
    }
}
