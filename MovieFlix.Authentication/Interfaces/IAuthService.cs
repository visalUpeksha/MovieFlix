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
        string GetAuthorizationToken(Login login);
    }
}
