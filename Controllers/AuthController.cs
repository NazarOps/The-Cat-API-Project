using Cat_API_Project.DTO;
using Cat_API_Project.DTO.Account.Login;
using Cat_API_Project.Services.Interfaces.IAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        //auth controller maps to interface that uses DTO
        public AuthController(IAuthService authservice)
        {
            _authService = authservice;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAccountDTO registerAccountDTO)
        {
            var account = await _authService.RegisterAsync(registerAccountDTO);

            return CreatedAtAction(
                nameof(Register),
                new { id = account.AccountId },
                account
                );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAccountDTO loginAccountDTO)
        {
            var response = await _authService.LoginAsync(loginAccountDTO);

            return Ok(response);
        }
    }
}
