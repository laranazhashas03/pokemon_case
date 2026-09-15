using Microsoft.EntityFrameworkCore;
using Pokemon.Domain.Entities;

namespace Pokemon.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<UserPokemon> UserPokemons => Set<UserPokemon>();

    public DbSet<FavoritePokemon> FavoritePokemons => Set<FavoritePokemon>();
}

