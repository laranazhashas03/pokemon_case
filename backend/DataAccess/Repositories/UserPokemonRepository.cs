using DataAccess.Data;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

// Handles database operations related to Pokémon caught by users.
public class UserPokemonRepository : IUserPokemonRepository
{
    private readonly AppDbContext _context;

    public UserPokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    // Gets all Pokémon caught by a specific user.
    public async Task<List<UserPokemon>> GetByUserIdAsync(int userId)
    {
        return await _context.UserPokemons
            .Where(up => up.UserId == userId)
            .ToListAsync();
    }

    // Adds a caught Pokémon to the database context.
    // The actual database save is handled by UnitOfWork.
    public async Task AddAsync(UserPokemon userPokemon)
    {
        await _context.UserPokemons.AddAsync(userPokemon);
    }
}