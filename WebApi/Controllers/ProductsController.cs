using BusinessLayer.Products.Models;
using BusinessLayer.Products.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models.Products;
using WebApi.monitoring.Errors;
using WebApi.monitoring.Switchers;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер управления продуктами
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
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

        /// <summary>
        ///  Добавление нового продукта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "name": "Iphone",
        ///        "description": "Iphone X"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Модель нового продукта</param>
        /// <returns>Статус 204 NoContent</returns>
        /// <response code="500">Прочие ошибки сервера</response>
        /// <response code="503">Принудительно выключен сервис</response>
        /// <response code="204">Успешный ответ без возвращаемого значения</response>
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

        /// <summary>
        /// Получение списка всех продуктов
        /// </summary>
        /// <returns>Статус 200. Все продукты</returns>
        /// <response code="500">Прочие ошибки сервера</response>
        /// <response code="503">Принудительно выключен сервис</response>
        /// <response code="200">Возвращает список всех продуктов</response>
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [ProducesResponseType(200, Type = typeof(ProductResponse))]
        public async Task<ActionResult<List<ProductResponse>>> GetAllProducts()
        {
            if (!_productSwitchers.CurrentValue.GetServiceEnabled)
            {
                return StatusCode(503, _error.DisabledService(nameof(GetAllProducts)));
            }
            List<ProductResponse>? productResponse;
            try
            {
                var productsDto = await _productService.GetAll();
                productResponse = productsDto.Select(p => new ProductResponse
                {
                    Name = p.Name,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate
                }).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, _error.DefaultHandle(nameof(GetAllProducts), ex));
            }
            return Ok(productResponse);
        }
    }
}