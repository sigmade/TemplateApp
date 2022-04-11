using BusinessLayer.Products.Models;
using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
                var errorId = new Random().Next(100, 99999);
                _logger.Error($"{nameof(AddNewProduct)} failed. ErrorId: {errorId}. {ex.Message}");
                throw new Exception($"Ошибка операции. Код {errorId}");
            }
            return NoContent();
        }

        [HttpGet]
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
                var errorId = new Random().Next(100, 99999);
                _logger.Error($"{nameof(GetAllProducts)} failed. ErrorId: {errorId}. {ex.Message}");
                throw new Exception($"Ошибка операции. Код {errorId}");
            }
            return Ok(products);
        }
    }
}