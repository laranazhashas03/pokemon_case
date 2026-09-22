using Core.DTOs.Catch;
using DataAccess.UnitOfWork;

namespace Business.Services;

// Handles the business logic for attempting to catch a Pokémon.
public class CatchPokemonService : ICatchPokemonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPokemonService _pokemonService;

    public CatchPokemonService(
        IUnitOfWork unitOfWork,
        IPokemonService pokemonService)
    {
        _unitOfWork = unitOfWork;
        _pokemonService = pokemonService;
    }

    // Attempts to catch a Pokémon for the specified user.
    public async Task<CatchPokemonResponseDto> CatchPokemonAsync(
        int userId,
        int pokemonId)
    {
        // Get the Pokémon from PokeAPI through the existing Pokémon service.
        var pokemon = await _pokemonService.GetPokemonByIdAsync(pokemonId);

        if (pokemon == null)
        {
            throw new KeyNotFoundException("Pokemon not found.");
        }

        // The Pokémon has a 50% chance of being caught.
        const double catchChance = 0.50;

        // Generate a random value between 0 and 1.
        var roll = Random.Shared.NextDouble();

        if (roll <= catchChance)
        {
            // Save the caught Pokémon for the current user.
            var userPokemon = new Entity.Entities.UserPokemon
            {
                UserId = userId,
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Name
            };

            await _unitOfWork.UserPokemons.AddAsync(userPokemon);

            await _unitOfWork.SaveChangesAsync();

            return new CatchPokemonResponseDto
            {
                Success = true,
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Name,
                Message = "Pokemon caught successfully."
            };
        }

        // The catch attempt failed, so nothing is saved.
        return new CatchPokemonResponseDto
        {
            Success = false,
            PokemonId = pokemon.Id,
            PokemonName = pokemon.Name,
            Message = "Pokemon could not be caught."
        };
    }
}