using API.Services;
using Domain.Entities;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text;

namespace API.Extensions
{
    /// <summary>
    /// Extension methods for configuring identity services.
    /// </summary>
    public static class IdentityServiceExtension
    {
        /// <summary>
        /// Adds identity services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The <see cref="IConfiguration"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config) 
        {
            Env.Load();

            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            

            services.AddScoped<TokenService>();

            return services;
        }
    }
}
