using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieFlix.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Authentication.Classes
{
    public static class JwtAuthBuilderExtesnions
    {
        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IJwtConfiguration jwtConfiguration)
        {
            services.AddAuthorization();

            return services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),

                    RequireExpirationTime = true,
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        Console.WriteLine("Authentication failed full exception: " + context.Exception.ToString());
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        string authorization = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine("Authorization Header: " + authorization);

                        if (string.IsNullOrEmpty(authorization))
                        {
                            context.NoResult();
                        }
                        else
                        {
                            context.Token = authorization.Replace("Bearer ", string.Empty).Trim().Trim('"');

                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token successfully validated");
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
