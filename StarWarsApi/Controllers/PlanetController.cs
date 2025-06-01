using BDCADAO.BDModels;
using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class PlanetController : ControllerBase
    {

        
        private readonly PlanetsService _planetService;

        public PlanetController(PlanetsService planetService)
        {
            _planetService = planetService;
        }

        [HttpGet("/[controller]/GetPlanets")]
        public async Task<ActionResult<IEnumerable<Planet>>> GetPlanets()
        {
            var planets = await _planetService.GetAllPlanetsAsync();
            return Ok(planets);
        }

        [HttpPost("/[controller]/SavePlanet")]
        public async Task<ActionResult<Planet>> CreatePlanet([FromBody] PlanetDto planet)
        {
            _planetService.SavePlanetAsync(planet);
            return CreatedAtAction("Insert planet", planet);
        }

        [HttpDelete("/[controller]/DeletePlanet/{id}")]
        public async Task<IActionResult> DeletePlanet(string id)
        {
            await _planetService.DeletePlanetAsync(id);
            return NoContent();
        }
    }
}