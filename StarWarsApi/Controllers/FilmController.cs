using BDCADAO.BDModels;
using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class FilmController : ControllerBase
    {
        private readonly FilmsService _filmService;

        public FilmController(FilmsService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet("/[controller]/GetFilms")]
        public async Task<ActionResult<IEnumerable<Films>>> GetFilms()
        {
            var films = await _filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        [HttpPost("/[controller]/SaveFilm")]
        public async Task<ActionResult<Films>> CreateFilm([FromBody] FilmsDto film)
        {
            try
            {
                await _filmService.SaveFilmAsync(film);
                return Ok("Film inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inserting film: {ex.Message}");
            }
        }

        [HttpDelete("/[controller]/DeleteFilm/{id}")]
        public async Task<IActionResult> DeleteFilm(string id)
        {
            await _filmService.DeleteFilmAsync(id);
            return NoContent();
        }

        [HttpGet("/[controller]/GetFilmsFromDatabase")]
        public ActionResult<List<Films>> GetFilmsFromDatabase()
        {
            var films = _filmService.GetFilmsFromDB();
            return Ok(films);
        }
    }
}