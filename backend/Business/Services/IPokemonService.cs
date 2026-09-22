namespace Business.Services;

public interface IPokemonService
{
    Task<List<Core.DTOs.Pokemon.PokemonListItemDto>> GetPokemonsAsync(
        int limit,
        int offset,
        string? search);
}