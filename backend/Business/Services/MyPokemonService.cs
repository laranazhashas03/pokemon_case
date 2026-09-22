using Core.DTOs.MyPokemon;
using DataAccess.UnitOfWork;

namespace Business.Services;

// Handles business logic for Pokémon caught by the logged-in user.
public class MyPokemonService : IMyPokemonService
{
    private readonly IUnitOfWork _unitOfWork;

    public MyPokemonService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Gets all Pokémon caught by the specified user.
    public async Task<List<MyPokemonDto>> GetMyPokemonsAsync(
        int userId)
    {
        // Get the caught Pokémon belonging to this user.
        var userPokemons = await _unitOfWork.UserPokemons
            .GetByUserIdAsync(userId);

        // Convert database entities into DTOs for the API response.
        return userPokemons
            .Select(up => new MyPokemonDto
            {
                PokemonId = up.PokemonId,
                PokemonName = up.PokemonName,
                CaughtAt = up.CaughtAt
            })
            .ToList();
    }
}