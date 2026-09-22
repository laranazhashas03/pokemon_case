using Entity.Entities;

namespace DataAccess.Repositories;

// Defines database operations related to refresh tokens.
public interface IRefreshTokenRepository
{
    // Adds a new refresh token to the database context.
    Task AddAsync(RefreshToken refreshToken);

    // Finds a refresh token by its token value.
    Task<RefreshToken?> GetByTokenAsync(string token);
}