using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Interfaces
{
    public interface IJwtConfiguration
    {
        string Issuer { get; }

        string Secret { get; }

        string Audience { get; }

        int ExpireDays { get; }
    }
}
