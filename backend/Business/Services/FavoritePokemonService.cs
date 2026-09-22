using Core.DTOs.Favorite;
using DataAccess.UnitOfWork;
using Entity.Entities;

namespace Business.Services;

// Handles the business logic for user's favorite Pokémon.
public class FavoritePokemonService : IFavoritePokemonService
{
    private readonly IUnitOfWork _unitOfWork;

    public FavoritePokemonService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Adds a Pokémon to the user's favorites.
    public async Task AddFavoriteAsync(int userId, int pokemonId)
    {
        // Check whether the Pokémon is already a favorite.
        var existingFavorite = await _unitOfWork.FavoritePokemons
            .GetAsync(userId, pokemonId);

        if (existingFavorite != null)
        {
            throw new InvalidOperationException(
                "Pokemon is already in favorites.");
        }

        var favoritePokemon = new FavoritePokemon
        {
            UserId = userId,
            PokemonId = pokemonId
        };

        await _unitOfWork.FavoritePokemons.AddAsync(favoritePokemon);

        await _unitOfWork.SaveChangesAsync();
    }

    // Removes a Pokémon from the user's favorites.
    public async Task RemoveFavoriteAsync(int userId, int pokemonId)
    {
        // Find the favorite belonging to this user.
        var favoritePokemon = await _unitOfWork.FavoritePokemons
            .GetAsync(userId, pokemonId);

        if (favoritePokemon == null)
        {
            throw new KeyNotFoundException(
                "Pokemon is not in favorites.");
        }

        _unitOfWork.FavoritePokemons.Delete(favoritePokemon);

        await _unitOfWork.SaveChangesAsync();
    }

    // Gets all favorite Pokémon belonging to the user.
    public async Task<List<FavoritePokemonDto>> GetFavoritesAsync(
        int userId)
    {
        var favorites = await _unitOfWork.FavoritePokemons
            .GetByUserIdAsync(userId);

        return favorites
            .Select(f => new FavoritePokemonDto
            {
                PokemonId = f.PokemonId,
                CreatedAt = f.CreatedAt
            })
            .ToList();
    }
}