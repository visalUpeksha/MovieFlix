using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string id, string email);
    }
}
