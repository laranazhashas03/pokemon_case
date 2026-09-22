using Core.DTOs.Catch;

namespace Business.Services;

// Defines operations for attempting to catch a Pokémon.
public interface ICatchPokemonService
{
    // Attempts to catch a Pokémon for the specified user.
    Task<CatchPokemonResponseDto> CatchPokemonAsync(
        int userId,
        int pokemonId);
}