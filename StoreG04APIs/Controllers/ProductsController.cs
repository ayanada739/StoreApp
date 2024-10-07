using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Sevices.Contract;

namespace Store.G04.APIs.Controllers
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
        [HttpGet] //Get BaseUrl /api/Products

        //sort : name, priceAsc, priceDesc
        public async Task<IActionResult> GetAllProducts([FromQuery] string? sort, [FromQuery] int? brandId, [FromQuery] int? typeId, [FromQuery] int? pageSize = 5 , [FromQuery] int? pageIndex =1 )//Endpoint
        {
            var result = await _productService.GetAllProductsAsync(sort, brandId, typeId, pageSize, pageIndex);
            return Ok(result); //200
        }

        [HttpGet("brands")] //Get BaseUrl/ api/Products
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);

        }

        [HttpGet("types")] //Get BaseUrl/ api/Products
        public async Task<IActionResult> GeyAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);

        }

        [HttpGet("{id}")] //Get BaseUrl/ api/Products
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest("Invalid Id !!");
            var result = await _productService.GetProductById(id.Value);

            if (result is null) return NotFound($"The Product With Id: {id} not found at BD :(");
            return Ok(result);

        }
    }
}
