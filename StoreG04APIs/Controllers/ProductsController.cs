using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Helper;
using Store.G04.Core.Sevices.Contract;
using Store.G04.Core.Specifications.Products;

namespace Store.G04.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet] //Get BaseUrl /api/Products

        //sort : name, priceAsc, priceDesc
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecParams productSpec  )//Endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpec);
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
