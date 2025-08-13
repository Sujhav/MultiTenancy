using Application.Common.Interfaces.Authentication;
using Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Authentication
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSetting _jwtSetting;

        public JwtTokenGenerator(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public string GenerateToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            var SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,users.Id.Value.ToString()!),
                new Claim(JwtRegisteredClaimNames.GivenName,users.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,users.LastName),
                new Claim(JwtRegisteredClaimNames.Email,users.Email.Value),
            };

            var SecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
                claims: claims,
                signingCredentials: SigningCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
