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

    public async Task<PokemonDetailDto?> GetPokemonByIdAsync(int id)
    {
    var response = await _httpClient.GetAsync(
        $"https://pokeapi.co/api/v2/pokemon/{id}");

    if (!response.IsSuccessStatusCode)
    {
        return null;
    }

    var json = await response.Content.ReadAsStringAsync();

    using var document = JsonDocument.Parse(json);

    var root = document.RootElement;

    var name = root.GetProperty("name").GetString() ?? "";

    var height = root.GetProperty("height").GetInt32();

    var weight = root.GetProperty("weight").GetInt32();

    // Get all six base stats from PokeAPI and calculate their total.
    var stats = root.GetProperty("stats");
    
    var baseStatTotal = stats.EnumerateArray().Sum(stat => stat.GetProperty("base_stat").GetInt32());

    return new PokemonDetailDto
    {
        Id = id,
        Name = name,
        ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
        Height = height,
        Weight = weight,
        BaseStatTotal = baseStatTotal
    };
    }
}