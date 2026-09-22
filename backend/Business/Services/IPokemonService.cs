namespace Business.Services;

// Defines operations for retrieving Pokémon data from PokeAPI.
public interface IPokemonService
{
    // Gets a paginated list of Pokémon, optionally filtered by name.
    Task<List<Core.DTOs.Pokemon.PokemonListItemDto>> GetPokemonsAsync(
        int limit,
        int offset,
        string? search);

    // Gets detailed information about a Pokémon by its ID.
    Task<Core.DTOs.Pokemon.PokemonDetailDto?> GetPokemonByIdAsync(int id);
}