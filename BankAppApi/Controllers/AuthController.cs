using BankAppApi.Core.DTO.Auth;
using BankAppApi.Core.Services;
using BankAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankApp_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto,CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(dto, cancellationToken);
            return Ok(ApiResponse<LoginResponseDTO>.SuccessResult(result,"Login successful"));

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto, CancellationToken cancellationToken)
        {
            await _authService.RegisterAsync(dto, cancellationToken);
            return Ok(ApiResponse<object>.SuccessResult(null, "Registration successful"));
        }







    }
}
