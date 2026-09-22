namespace Core.DTOs.MyPokemon;

// Represents a Pokémon caught by the logged-in user.
public class MyPokemonDto
{
    // The Pokémon's ID from PokeAPI.
    public int PokemonId { get; set; }

    // The Pokémon's name.
    public string PokemonName { get; set; } = string.Empty;

    // The Pokémon's height from PokeAPI.
    public int Height { get; set; }

    // The Pokémon's weight from PokeAPI.
    public int Weight { get; set; }

    // The Pokémon's image URL from PokeAPI.
    public string ImageUrl { get; set; } = string.Empty;

    // The date and time when the Pokémon was caught.
    public DateTime CaughtAt { get; set; }
}