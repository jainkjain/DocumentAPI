using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.API.DTO;
using UserManagement.API.Services;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Check if the email is unique.
            bool isEmailUnique = await _authService.IsEmailUniqueAsync(dto.Email);
            if (!isEmailUnique)
            {
                _logger.LogInformation("Email is already taken by another user.");
                return BadRequest("Email is already taken by another user.");
            }

            // Create the user using the user service.
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto.EmailAddress, dto.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogDebug("Invalid credentials." + ex.Message);
                return Unauthorized("Invalid credentials.");
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Logic to logout (invalidate token on client-side)
            return Ok("Logged out successfully.");
        }
    }
}
