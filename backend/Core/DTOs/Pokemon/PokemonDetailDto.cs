namespace Core.DTOs.Pokemon;

// Represents detailed Pokémon information retrieved from PokeAPI.
public class PokemonDetailDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int Height { get; set; }

    public int Weight { get; set; }

    // The sum of all six base stats from PokeAPI.
    public int BaseStatTotal { get; set; }
}