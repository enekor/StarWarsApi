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
        public CharacterService(HttpClient httpClient, ModelContext context) : base(context, httpClient)
        {
        }

        public async Task<List<CharacterDto>> GetAllCharactersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/people");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var characters = JsonSerializer.Deserialize<List<CharacterApi>>(content);

                if (characters == null)
                    return new List<CharacterDto>();

                return characters
                    .Select(c => ACCharacterMapper.Instance.MapToController(c))
                    .Where(c => c != null)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los personajes: {e.Message}");
                return new List<CharacterDto>();
            }
        }

        public async Task<CharacterDto?> GetCharacterByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/people/{id}");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var character = JsonSerializer.Deserialize<CharacterApi>(content);

                return ACCharacterMapper.Instance.MapToController(character);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el personaje con id {id}: {e.Message}");
                return null;
            }
        }

        public List<CharacterDto> GetCharactersFromDB()
        {
            var characters = _context.Characters.GetAll();
            if (characters == null)
                return new List<CharacterDto>();

            return DCCharacterMapper.Instance.ToDtoList(characters);
        }

        public async Task SaveCharacterAsync(CharacterDto characterDto)
        {
            var character = CDCharacterMapper.Instance.ToEntity(characterDto);
            if (character != null)
            {
                if (string.IsNullOrEmpty(character.Uid))
                {
                    character.Uid = characterDto.Url?.Split('/').Last() ?? Guid.NewGuid().ToString();
                }
                _context.Characters.InsertOrUpdate(character);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            var deleted = _context.Characters.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public CharacterDto? GetCharacterFromDB(string id)
        {
            var character = _context.Characters.GetById(id);
            return character != null ? DCCharacterMapper.Instance.ToDto(character) : null;
        }
    }
}