using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class StarshipController : ControllerBase
    {
        private readonly StarshipService _starshipService;

        public StarshipController(StarshipService starshipService)
        {
            _starshipService = starshipService;
        }

        [HttpGet("/[controller]/GetStarships")]
        public async Task<ActionResult<IEnumerable<Starship>>> GetStarships()
        {
            var starships = await _starshipService.GetAllStarshipsAsync();
            return Ok(starships);
        }

        [HttpPost("/[controller]/SaveStarship")]
        public async Task<ActionResult<Starship>> CreateStarship([FromBody] StarshipDto starship)
        {
             _starshipService.SaveStarshipAsync(starship);
            return CreatedAtAction("Insert starship", starship);
        }

        [HttpDelete("/[controller]/DeleteStarship/{id}")]
        public async Task<IActionResult> DeleteStarship(string id)
        {
            await _starshipService.DeleteStarshipAsync(id);
            return NoContent();
        }
    }
}