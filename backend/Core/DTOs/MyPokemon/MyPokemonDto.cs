namespace Core.DTOs.MyPokemon;

// Represents a Pokémon caught by the logged-in user.
public class MyPokemonDto
{
    // The Pokémon's ID from PokeAPI.
    public int PokemonId { get; set; }

    // The Pokémon's name.
    public string PokemonName { get; set; } = string.Empty;

    // The date and time when the Pokémon was caught.
    public DateTime CaughtAt { get; set; }
}