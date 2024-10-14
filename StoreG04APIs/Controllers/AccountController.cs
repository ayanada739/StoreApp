using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Dtos.Login;
using Store.G04.Core.Sevices.Contract;
using System.Threading.Tasks;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]  //Post: /api/Account/login
        public async Task< ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);

        }

        [HttpPost("register")]  //Post: /api/Account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto loginDto)
        {
            var user = await _userService.RegisterAsync(loginDto);
            if (user is null) return BadRequest(error: new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registeration !!"));
            return Ok(user);

        }
    }
}
