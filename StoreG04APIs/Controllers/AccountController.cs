using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Dtos.Login;
using Store.G04.Core.Entities.Identity;
using Store.G04.Core.Sevices.Contract;
using Store.G04.Service.Services.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.G04.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            IUserService userService,
            UserManager<AppUser> userManager,
            ITokenService tokenService
            )
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
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

        [HttpGet("GetCurrentUser")] //Get : /api/Account/GetCurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
             var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return BadRequest(error: new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(error: new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
    }
}
