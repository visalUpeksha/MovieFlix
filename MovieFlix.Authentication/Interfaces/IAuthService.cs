using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MovieFlix.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetAuthorizationToken(RegisterRequest login);
        Task<string> Register(RegisterRequest request);
    }
}
