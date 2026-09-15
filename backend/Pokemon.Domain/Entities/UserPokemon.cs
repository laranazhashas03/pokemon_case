namespace Pokemon.Domain.Entities;

public class UserPokemon
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PokemonId { get; set; }

    public string PokemonName { get; set; } = string.Empty;

    public DateTime CaughtAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}