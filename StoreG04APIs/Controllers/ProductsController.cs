using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
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

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] //Get BaseUrl /api/Products

        //sort : name, priceAsc, priceDesc
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery]ProductSpecParams productSpec  )//Endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpec);
            return Ok(result); //200
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")] //Get BaseUrl/ api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GeyAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);

        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")] //Get BaseUrl/ api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);

        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ApiErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ApiErrorResponse>), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] //Get BaseUrl/ api/Products
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest(error: new ApiErrorResponse(statusCode:400));
            var result = await _productService.GetProductById(id.Value);

            if (result is null) return NotFound(value: new ApiErrorResponse(statusCode: 404, message: $"The Product With Id: {id} not found"));
            return Ok(result);

        }
    }
}
