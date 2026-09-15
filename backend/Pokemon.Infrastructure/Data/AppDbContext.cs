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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserPokemons)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.FavoritePokemons)
            .WithOne(fp => fp.User)
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}