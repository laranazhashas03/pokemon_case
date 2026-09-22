using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

[HttpGet]
public async Task<IActionResult> GetPokemons(
    [FromQuery] int limit = 20,
    [FromQuery] int offset = 0,
    [FromQuery] string? search = null)
{
    if (limit < 1 || limit > 100)
    {
        return BadRequest("Limit must be between 1 and 100.");
    }

    if (offset < 0)
    {
        return BadRequest("Offset cannot be negative.");
    }

    var pokemons = await _pokemonService.GetPokemonsAsync(
        limit,
        offset,
        search);

    return Ok(pokemons);
    }
}