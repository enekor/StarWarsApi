using BDCADAO.BDModels;
using Microsoft.AspNetCore.Mvc;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;

namespace StarWarsApi.Controllers
{
    public class CharacterController : ControllerBase
    {
        
        private readonly CharacterService _characterService;

        public CharacterController(CharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("/[controller]/GetCharacters")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            return Ok(characters);
        }

        [HttpPost("/[controller]/SaveCharacterToDatabase")]
        public async Task<ActionResult<Character>> CreateCharacter([FromBody] CharacterDto character)
        {
            _characterService.SaveCharacterAsync(character);
            return CreatedAtAction("Insert character",character);
        }

        [HttpGet("/[controller]/GetCharactersFromDatabase/{id}")]
        public async Task<ActionResult<List<Character>>> GetCharactersFromDatabase(string id)
        {
            var characters = _characterService.GetCharactersFromDB();

            return Ok(characters);
        }

        [HttpDelete("/[controller]/DeleteCharacter/{id}")]
        public async Task<IActionResult> DeleteCharacter(string id)
        {
            await _characterService.DeleteCharacterAsync(id);
            return NoContent();
        }

    }
}
