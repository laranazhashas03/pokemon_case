namespace Business.Services;

// Defines operations for managing a user's favorite Pokémon.
public interface IFavoritePokemonService
{
    // Adds a Pokémon to the user's favorites.
    Task AddFavoriteAsync(int userId, int pokemonId);

    // Removes a Pokémon from the user's favorites.
    Task RemoveFavoriteAsync(int userId, int pokemonId);

    // Gets all favorite Pokémon belonging to the user.
    Task<List<Core.DTOs.Favorite.FavoritePokemonDto>> GetFavoritesAsync(
        int userId);
}