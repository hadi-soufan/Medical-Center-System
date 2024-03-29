﻿using Domain.Entities;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// Service for handling JWT token generation.
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>The JWT token string.</returns>
        public string CreateToken(AppUser user)
        {
            Env.Load();

            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);



            
        }
    }
}
