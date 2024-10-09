using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[code]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(value: new ApiErrorResponse(StatusCodes.Status404NotFound, message: "Not Found EndPint !"));
        }
    }
}
