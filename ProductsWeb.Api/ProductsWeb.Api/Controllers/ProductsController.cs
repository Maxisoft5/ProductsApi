using Microsoft.AspNetCore.Mvc;
using Products.DataAccessEfCore;
using Products.DTO;
using ProductsWeb.Services.Interfaces;

namespace ProductsWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetProductsByName([FromQuery] string name)
        {
            var products = await _productsService.GetByName(name);
            return Ok(products);
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find([FromQuery] string productName, [FromQuery] string productVersionName, 
            [FromQuery] decimal minSize, [FromQuery] decimal maxSize)
        {
            var products = await _productsService.Find(productName, productVersionName, minSize, maxSize);
            return Ok(products);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            if (!product.Id.HasValue)
            {
                return BadRequest("To update the product need to specify an Id");
            }
            if (!await _productsService.IsExists(product))
            {
                return BadRequest($"Product with Id {product.Id} and name ${product.Name} not found");
            }

            var editProduct = await _productsService.Edit(product);
            return Ok(editProduct);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
        {
            var addedProduct = await _productsService.Create(product);
            return Ok(addedProduct);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            var result = await _productsService.Delete(id);
            if (result)
            {
                return Ok();
            } 
            return BadRequest();
        }
    }
}