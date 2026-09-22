namespace Core.DTOs.Catch;

// Represents the result of a Pokémon catch attempt.
public class CatchPokemonResponseDto
{
    // Indicates whether the Pokémon was successfully caught.
    public bool Success { get; set; }

    // The ID of the Pokémon that was attempted.
    public int PokemonId { get; set; }

    // The Pokémon's name.
    public string PokemonName { get; set; } = string.Empty;

    // Message describing the result of the catch attempt.
    public string Message { get; set; } = string.Empty;
}