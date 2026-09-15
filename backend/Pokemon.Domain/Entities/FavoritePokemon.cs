namespace Pokemon.Domain.Entities;

public class FavoritePokemon
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PokemonId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}