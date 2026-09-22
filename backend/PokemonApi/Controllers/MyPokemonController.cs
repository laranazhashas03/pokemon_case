using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MyPokemonController : ControllerBase
{
    private readonly IMyPokemonService _myPokemonService;

    public MyPokemonController(
        IMyPokemonService myPokemonService)
    {
        _myPokemonService = myPokemonService;
    }

    // Gets all Pokémon caught by the logged-in user.
    [HttpGet]
    public async Task<IActionResult> GetMyPokemons()
    {
        var userId = GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var myPokemons = await _myPokemonService.GetMyPokemonsAsync(userId.Value);

        return Ok(myPokemons);
    }

    // Gets the logged-in user's ID from the JWT NameIdentifier claim.
    private int? GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return null;
        }

        return userId;
    }
}