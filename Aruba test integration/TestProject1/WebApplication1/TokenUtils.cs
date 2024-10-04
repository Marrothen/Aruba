﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1
{
    public class TokenUtils
    {
        private readonly IConfiguration _configuration;
        public TokenUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string nome)
        {
            string tokenString = "";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nome),
                 new Claim(ClaimTypes.Role, "Admin"),

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, login.Username),
                //    new Claim(ClaimTypes.Role, "Admin") // Aggiungi ruoli se necessario
                //}),
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}
