namespace Business.Services;

public interface IPokemonService
{
    Task<List<Core.DTOs.Pokemon.PokemonListItemDto>> GetPokemonsAsync(
        int limit,
        int offset,
        string? search);

    Task<Core.DTOs.Pokemon.PokemonDetailDto?> GetPokemonByIdAsync(int id);
}