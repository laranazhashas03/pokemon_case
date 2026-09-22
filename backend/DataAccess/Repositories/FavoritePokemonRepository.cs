using DataAccess.Data;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class FavoritePokemonRepository : IFavoritePokemonRepository
{
    private readonly AppDbContext _context;

    public FavoritePokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FavoritePokemon?> GetAsync(
        int userId,
        int pokemonId)
    {
        return await _context.FavoritePokemons
            .FirstOrDefaultAsync(f =>
                f.UserId == userId &&
                f.PokemonId == pokemonId);
    }

    public async Task<List<FavoritePokemon>> GetByUserIdAsync(
        int userId)
    {
        return await _context.FavoritePokemons
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(FavoritePokemon favoritePokemon)
    {
        await _context.FavoritePokemons.AddAsync(favoritePokemon);
    }

    public void Delete(FavoritePokemon favoritePokemon)
    {
        _context.FavoritePokemons.Remove(favoritePokemon);
    }
}