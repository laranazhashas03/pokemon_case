using Entity.Entities;

namespace DataAccess.Repositories;

// Defines database operations for user's favorite Pokémon.
public interface IFavoritePokemonRepository
{
    // Finds a specific favorite for a specific user.
    // Returns null if the Pokémon is not a favorite.
    Task<FavoritePokemon?> GetAsync(int userId, int pokemonId);

    // Gets all favorite Pokémon belonging to a specific user.
    Task<List<FavoritePokemon>> GetByUserIdAsync(int userId);

    // Adds a new favorite Pokémon to the database context.
    Task AddAsync(FavoritePokemon favoritePokemon);

    // Marks an existing favorite Pokémon for deletion.
    void Delete(FavoritePokemon favoritePokemon);
}