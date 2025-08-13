using Domain.Users;

namespace Infrastructure.Persistance.Repository
{
    public interface IRefreshTokenRepository
    {
        Task Add(Users users, string RefreshToken);
    }
}