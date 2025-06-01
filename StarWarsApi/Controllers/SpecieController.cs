using BDCADAO.BDModels;
using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class SpecieController : ControllerBase
    {
        private readonly SpeciesService _specieService;

        public SpecieController(SpeciesService specieService)
        {
            _specieService = specieService;
        }

        [HttpGet("/[controller]/GetSpecies")]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpecies()
        {
            var species = await _specieService.GetAllSpeciesAsync();
            return Ok(species);
        }

        [HttpPost("/[controller]/SaveSpecie")]
        public async Task<ActionResult<Species>> CreateSpecie([FromBody] SpeciesDto specie)
        {
            _specieService.SaveSpeciesAsync(specie);
            return CreatedAtAction("Insert specie", specie);
        }

        [HttpDelete("/[controller]/DeleteSpecie/{id}")]
        public async Task<IActionResult> DeleteSpecie(string id)
        {
            await _specieService.DeleteSpeciesAsync(id);
            return NoContent();
        }
    }
}