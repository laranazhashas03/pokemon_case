using DataAccess.Data;
using DataAccess.Repositories;

namespace DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }

    public IRefreshTokenRepository RefreshTokens { get; }

    public IFavoritePokemonRepository FavoritePokemons { get; }

    public UnitOfWork(
        AppDbContext context,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IFavoritePokemonRepository favoritePokemonRepository)
    {
        _context = context;
        Users = userRepository;
        RefreshTokens = refreshTokenRepository;
        FavoritePokemons = favoritePokemonRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}