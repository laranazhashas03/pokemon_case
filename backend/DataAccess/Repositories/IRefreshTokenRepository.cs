using Entity.Entities;

namespace DataAccess.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByTokenAsync(string token);
}