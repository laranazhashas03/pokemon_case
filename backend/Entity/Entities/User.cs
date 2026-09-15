namespace Entity.Entities;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<UserPokemon> UserPokemons { get; set; } = new List<UserPokemon>();

    public ICollection<FavoritePokemon> FavoritePokemons { get; set; } = new List<FavoritePokemon>();
}
