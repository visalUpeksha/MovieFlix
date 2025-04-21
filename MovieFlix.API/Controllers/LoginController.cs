using Microsoft.AspNetCore.Mvc;
using MovieFlix.Authentication.Interfaces;
using MovieFlix.Domain.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieFlix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService authService;
        public LoginController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login user)
        {
            var token = authService.GetAuthorizationToken(user);
            if (String.IsNullOrEmpty(token)){
                return Unauthorized();
            }
            else
            {
                return Ok(new { token });
            }
        }
    }
}
