using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalAPI.Services;
using JournalAPI.Models;
namespace JournalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<AuthResult>>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Success)
                return Unauthorized(new ResponseDto<AuthResult> { Success = false, Message = result.Message });

            // Set JWT as HttpOnly cookie
            Response.Cookies.Append("jwt_token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new ResponseDto<AuthResult> { Success = true, Data = result });
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<AuthResult>>> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
                return BadRequest(new ResponseDto<AuthResult> { Success = false, Message = result.Message });

            // Set JWT as HttpOnly cookie
            Response.Cookies.Append("jwt_token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new ResponseDto<AuthResult> { Success = true, Data = result });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            return Ok(new ResponseDto<string> { Success = true, Message = "Logged out successfully." });
        }
    }
}