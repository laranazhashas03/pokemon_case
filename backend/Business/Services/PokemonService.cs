using Core.DTOs.Pokemon;
using System.Text.Json;

namespace Business.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PokemonListItemDto>> GetPokemonsAsync(
        int limit,
        int offset,
        string? search)
    {
        var response = await _httpClient.GetAsync(
            "https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(json);

        var results = document.RootElement
            .GetProperty("results")
            .EnumerateArray();

        var filteredPokemons = new List<PokemonListItemDto>();

        foreach (var pokemon in results)
        {
            var name = pokemon.GetProperty("name").GetString() ?? "";

            if (!string.IsNullOrWhiteSpace(search) &&
                !name.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var url = pokemon.GetProperty("url").GetString() ?? "";

            var id = int.Parse(
                url.TrimEnd('/').Split('/').Last());

            filteredPokemons.Add(new PokemonListItemDto
            {
                Id = id,
                Name = name,
                ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png"
            });
        }

        return filteredPokemons
            .Skip(offset)
            .Take(limit)
            .ToList();
    }
}