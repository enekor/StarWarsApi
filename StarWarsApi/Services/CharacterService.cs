using StarWarsApi.Models;
using StarWarsApi.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.controller;
using BDCADAO.BDModels;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Mappers.FromControllerToDatabase;

namespace StarWarsApi.Services
{
    public class CharacterService : BaseService
    {

        public CharacterService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<CharacterDto>> GetAllCharactersAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/people");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var characters = JsonSerializer.Deserialize<List<CharacterApi>>(content);
            
            return characters.Select(c => ACCharacterMapper.Instance.MapToController(c)).ToList();
        }
        public async Task<CharacterDto?> GetCharacterByIdAsync(string id)
        {
            CharacterDto? characterDto = null;
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/people/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var characterApi = JsonSerializer.Deserialize<CharacterApi>(content);

                    characterDto = ACCharacterMapper.Instance.MapToController(characterApi);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error al obtener el personaje con id {id}: {e.Message}");
            }
            
            return characterDto;
        }

        public List<CharacterDto> GetCharactersFromDB()
        {
            var characters = _context.Characters.GetAll();
            return DCCharacterMapper.Instance.ToDtoList(characters);
        }

        public async Task SaveCharacterAsync(CharacterDto characterDto)
        {
            var character = CDCharacterMapper.Instance.ToEntity(characterDto);
            _context.Characters.InsertOrUpdate(character);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            var deleted = _context.Characters.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public CharacterDto GetCharacterFromDB(string id)
        {
            var character = _context.Characters.GetById(id);
            return DCCharacterMapper.Instance.ToDto(character);
        }
    }
}