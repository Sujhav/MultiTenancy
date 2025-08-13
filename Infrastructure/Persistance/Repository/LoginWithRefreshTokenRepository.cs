using Application.Authentication.Query.Login;
using Application.Authentication.Query.RefreshToken;
using Application.Common.Dtos;
using Application.Common.Interfaces.Authentication;
using Azure.Core;
using Domain.Users;
using Infrastructure.Service.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    internal sealed class LoginWithRefreshTokenRepository(MultiTenencyDbContext multiTenencyDbContext, IJwtTokenGenerator jwtTokenGenerator) : ILoginWithRefreshTokenRepository
    {

        public async Task<LoginResponse> handle(RefreshTokenQuery request)
        {
            var user = multiTenencyDbContext.Users.Include(s => s.RefreshTokens).FirstOrDefault(rft => rft.RefreshTokens.Any(s => s.Token == request.token));

            var refreshToken = user?.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);


            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.Now)
            {
                throw new ApplicationException("The refresh token is expired");

            }
            string access = jwtTokenGenerator.GenerateToken(user!);

            var NewRefreshToken = jwtTokenGenerator.GenerateRefreshToken();

            refreshToken.UpdateToken(NewRefreshToken, DateTime.UtcNow.AddDays(7));

            await multiTenencyDbContext.SaveChangesAsync();

            return new LoginResponse(access, NewRefreshToken);

        }
    }
}
