using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritePokemonService _favoritePokemonService;

    public FavoritesController(
        IFavoritePokemonService favoritePokemonService)
    {
        _favoritePokemonService = favoritePokemonService;
    }

    // Adds a Pokémon to the logged-in user's favorites.
    [HttpPost("{pokemonId}")]
    public async Task<IActionResult> AddFavorite(int pokemonId)
    {
        var userId = GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        if (pokemonId < 1)
        {
            return BadRequest("Pokemon ID must be greater than 0.");
        }

        try
        {
            await _favoritePokemonService.AddFavoriteAsync(
                userId.Value,
                pokemonId);

            return Ok("Pokemon added to favorites.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Removes a Pokémon from the logged-in user's favorites.
    [HttpDelete("{pokemonId}")]
    public async Task<IActionResult> RemoveFavorite(int pokemonId)
    {
        var userId = GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        if (pokemonId < 1)
        {
            return BadRequest("Pokemon ID must be greater than 0.");
        }

        try
        {
            await _favoritePokemonService.RemoveFavoriteAsync(
                userId.Value,
                pokemonId);

            return Ok("Pokemon removed from favorites.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // Gets all favorite Pokémon of the logged-in user.
    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var favorites = await _favoritePokemonService
            .GetFavoritesAsync(userId.Value);

        return Ok(favorites);
    }

    // Gets the logged-in user's ID from the JWT NameIdentifier claim.
    private int? GetUserId()
    {
        var userIdClaim = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return null;
        }

        return userId;
    }
}