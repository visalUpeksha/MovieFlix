using Azure.Core;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieFlix.Authentication.Interfaces;
using MovieFlix.Domain.Classes;
using MovieFlix.Infrastructure;
using MovieFlix.Infrastructure.Classes;
using MovieFlix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Classes
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;
        private readonly MovieDBContext _movieDBContext;
        private readonly IPasswordService _passwordService;

        public AuthService(ITokenService _tokenService, MovieDBContext movieDBContext, IPasswordService passwordService)
        {
            tokenService = _tokenService;
            _movieDBContext = movieDBContext;
            _passwordService = passwordService;
        }
        public async Task<string> GetAuthorizationToken(RegisterRequest request)
        {
            var user = await _movieDBContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return ("401|Invalid username or password.");

            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
                return ("401|Account is locked. Try again later.");

            if (!_passwordService.VerifyPassword(user, request.Password))
            {
                user.FailedLoginAttempts += 1;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(15); // Lock for 15 minutes
                    user.FailedLoginAttempts = 0; // reset attempts after lock
                }

                await _movieDBContext.SaveChangesAsync();
                return ("401|Invalid username or password.");
            }

            // Reset on successful login
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            await _movieDBContext.SaveChangesAsync();
            var token = tokenService.GenerateToken(user.UserId.ToString(), user.Email);
            return ("200|"+token);
        }

        public async Task<string> Register(RegisterRequest request)
        {
            var existingUser = await _movieDBContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return ("400|Username already exists.");

            var user = new User
            {
                Email = request.Email
            };

            user.PasswordHash = _passwordService.HashPassword(user, request.Password);

            _movieDBContext.Users.Add(user);
            await _movieDBContext.SaveChangesAsync();

            return ("200|User registered successfully!");
        }

    }
}
