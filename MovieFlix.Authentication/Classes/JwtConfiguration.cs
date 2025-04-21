using Microsoft.Extensions.Configuration;
using MovieFlix.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Classes
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Issuer { get; } = string.Empty;

        public string Secret { get; } = string.Empty;

        public string Audience { get; } = string.Empty;

        public int ExpireDays { get; }
        private IConfiguration _configuration;

        public JwtConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            var section = _configuration.GetSection("JWT");

            Issuer = section[nameof(Issuer)];
            Secret = section[nameof(Secret)];
            Audience = section[nameof(Secret)];
            ExpireDays = Convert.ToInt32(section[nameof(ExpireDays)], CultureInfo.InvariantCulture);
        }
    }
}
