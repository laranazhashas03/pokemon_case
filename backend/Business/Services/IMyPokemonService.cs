using Core.DTOs.MyPokemon;

namespace Business.Services;

// Defines operations for retrieving Pokémon caught by a user.
public interface IMyPokemonService
{
    // Gets all Pokémon caught by the specified user.
    Task<List<MyPokemonDto>> GetMyPokemonsAsync(int userId);
}