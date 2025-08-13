using Application.Authentication.Query.Login;
using Application.Authentication.Query.RefreshToken;
using Application.Common.Dtos;
using MediatR;

namespace Infrastructure.Persistance.Repository
{
    public interface ILoginWithRefreshTokenRepository
    {
        Task<LoginResponse> handle(RefreshTokenQuery request);
    }
}