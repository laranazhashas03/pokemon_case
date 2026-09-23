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

        // Calculate the catch chance based on the Pokémon's total base stats.
        // Stronger Pokémon are harder to catch.
        double catchChance;

        if (pokemon.BaseStatTotal <= 300)
        {
            catchChance = 0.80;
        }
        else if (pokemon.BaseStatTotal <= 450)
        {
            catchChance = 0.65;
        }
        else if (pokemon.BaseStatTotal <= 600)
        {
            catchChance = 0.50;
        }
        else if (pokemon.BaseStatTotal <= 700)
        {
            catchChance = 0.35;
        }
        else
        {
            catchChance = 0.20;
        }

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
                CatchChance = catchChance,
                Message = "Pokemon caught successfully."
            };
        }

        // The catch attempt failed, so nothing is saved.
        return new CatchPokemonResponseDto
        {
            Success = false,
            PokemonId = pokemon.Id,
            PokemonName = pokemon.Name,
            CatchChance = catchChance,
            Message = "Pokemon could not be caught."
        };
    }
}