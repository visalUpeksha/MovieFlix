using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieFlix.Authentication.Interfaces;
using MovieFlix.Domain.Classes;
using MovieFlix.Infrastructure.Classes;

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

        [EnableCors("AllowReactApp")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] RegisterRequest user)
        {
            var token = await authService.GetAuthorizationToken(user);
            if (String.IsNullOrEmpty(token)){
                return Unauthorized();
            }
            else if(token.Split('|', 2)[0] != "200")
            {
                return Unauthorized(token.Split('|', 2)[1]);
            }
            else
            {
                return Ok(new { token = token.Split('|', 2)[1] });
            }
        }

        [EnableCors("AllowReactApp")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await authService.Register(request);
            
            if(result.Split('|', 2)[0] == "200")
            {
                return Ok(result.Split('|', 2)[1]);
            }
            else
            {
                return BadRequest(result.Split('|', 2)[1]);
            }
        }

    }
}
