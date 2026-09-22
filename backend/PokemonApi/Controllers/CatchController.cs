using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CatchController : ControllerBase
{
    private readonly ICatchPokemonService _catchPokemonService;

    public CatchController(
        ICatchPokemonService catchPokemonService)
    {
        _catchPokemonService = catchPokemonService;
    }

    // Attempts to catch a Pokémon for the logged-in user.
    [HttpPost("{pokemonId}")]
    public async Task<IActionResult> CatchPokemon(int pokemonId)
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
            var result = await _catchPokemonService.CatchPokemonAsync(
                userId.Value,
                pokemonId);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
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