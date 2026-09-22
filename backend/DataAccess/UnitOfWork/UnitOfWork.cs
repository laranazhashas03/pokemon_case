using DataAccess.Data;
using DataAccess.Repositories;

namespace DataAccess.UnitOfWork;

// Coordinates repositories and saves their changes through a single database context.
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }

    public IRefreshTokenRepository RefreshTokens { get; }

    public IFavoritePokemonRepository FavoritePokemons { get; }

    public IUserPokemonRepository UserPokemons { get; }

    public UnitOfWork(
        AppDbContext context,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IFavoritePokemonRepository favoritePokemonRepository,
        IUserPokemonRepository userPokemonRepository)
    {
        _context = context;
        Users = userRepository;
        RefreshTokens = refreshTokenRepository;
        FavoritePokemons = favoritePokemonRepository;
        UserPokemons = userPokemonRepository;
    }

    // Saves all pending changes made through the repositories.
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}