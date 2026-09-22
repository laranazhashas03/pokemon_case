using Core.DTOs.MyPokemon;
using DataAccess.UnitOfWork;

namespace Business.Services;

// Handles business logic for Pokémon caught by the logged-in user.
public class MyPokemonService : IMyPokemonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPokemonService _pokemonService;

    public MyPokemonService(
        IUnitOfWork unitOfWork,
        IPokemonService pokemonService)
    {
        _unitOfWork = unitOfWork;
        _pokemonService = pokemonService;
    }

    // Gets all Pokémon caught by the specified user.
    public async Task<List<MyPokemonDto>> GetMyPokemonsAsync(
        int userId)
    {
        // Get the Pokémon caught by this user from the database.
        var userPokemons = await _unitOfWork.UserPokemons
            .GetByUserIdAsync(userId);

        var result = new List<MyPokemonDto>();

        foreach (var userPokemon in userPokemons)
        {
            // Get the latest Pokémon details from PokeAPI.
            var pokemon = await _pokemonService.GetPokemonByIdAsync(userPokemon.PokemonId);

            // Skip the Pokémon if it no longer exists in PokeAPI.
            if (pokemon == null)
            {
                continue;
            }

            result.Add(new MyPokemonDto
            {
                PokemonId = userPokemon.PokemonId,
                PokemonName = userPokemon.PokemonName,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                ImageUrl = pokemon.ImageUrl,
                CaughtAt = userPokemon.CaughtAt
            });
        }

        return result;
    }
}