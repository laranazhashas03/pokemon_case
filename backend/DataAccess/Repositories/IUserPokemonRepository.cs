using Entity.Entities;

namespace DataAccess.Repositories;

// Defines database operations related to Pokémon caught by users.
public interface IUserPokemonRepository
{
    // Gets all Pokémon caught by a specific user.
    Task<List<UserPokemon>> GetByUserIdAsync(int userId);

    // Adds a caught Pokémon to the database context.
    Task AddAsync(UserPokemon userPokemon);
}