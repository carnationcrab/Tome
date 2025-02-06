using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;
using System.Threading.Tasks;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _userService.RegisterUserAsync(model);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var userId = await _userService.LoginUserAsync(model);
            if (userId == null) return Unauthorized("Invalid credentials.");
            return Ok(new { userId });
        }
    }
}
