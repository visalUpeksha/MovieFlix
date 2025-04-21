using MovieFlix.Authentication.Interfaces;
using MovieFlix.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Classes
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;

        public AuthService(ITokenService _tokenService)
        {
            tokenService = _tokenService;
        }
        public string GetAuthorizationToken(Login login)
        {
            var userIsAuthenticated = login.UserName == "admin" && login.Password == "admin";

            if (!userIsAuthenticated)
            {
                return "";
            }
            var userId = "9999"; // Get user id from database
            var email = "valentin.osidach@gmail.com"; // Get email from database
            var token = tokenService.GenerateToken(userId, email);
            return token;
        }
    }
}
