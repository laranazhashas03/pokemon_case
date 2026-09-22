using DataAccess.Repositories;

namespace DataAccess.UnitOfWork;

// Defines the repositories and database save operation available through UnitOfWork.
public interface IUnitOfWork
{
    // Provides access to user-related database operations.
    IUserRepository Users { get; }

    // Provides access to refresh token database operations.
    IRefreshTokenRepository RefreshTokens { get; }

    // Provides access to favorite Pokémon database operations.
    IFavoritePokemonRepository FavoritePokemons { get; }

    // Saves all pending database changes.
    Task<int> SaveChangesAsync();
}