using System;
using System.Text;
using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Constants;
using Library.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Shared.DI.Configs
{
    public static class AuthenticationDIConfig
    {
        public static AuthenticationBuilder AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.UTF8.GetBytes(
                        configuration.GetValue<string>(JwtConfig.JwtSecretKey));

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };

                    jwt.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add(Headers.TokenExpired, bool.TrueString.ToLower());
                                context.Response.AddAccessControlExposeHeaders(Headers.TokenExpired);
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        public static IServiceCollection ConfigureJwtConfig(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)))
                .AddSingleton(s => s.GetRequiredService<IOptions<JwtConfig>>().Value);
    }
}