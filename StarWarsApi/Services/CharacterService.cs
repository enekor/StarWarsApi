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
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient, ModelContext context) : base(context)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharacterDto>> GetAllCharactersAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/people");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var characters = JsonSerializer.Deserialize<List<CharacterApi>>(content);
            
            return characters.Select(c => ACCharacterMapper.Instance.MapToController(c)).ToList();
        }

        public async Task<CharacterDto> GetCharacterByIdAsync(string id)
        {
            var data = _context.Characters.GetById(id);
            if (data != null)
            {
                return DCCharacterMapper.Instance.ToDto(data);
            }

            return null;
        }

        public async void SaveCharacterAsync(CharacterDto characterDto)
        {
            var character = CDCharacterMapper.Instance.ToEntity(characterDto);
            _context.Characters.InsertOrUpdate(character);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            int deleted = _context.Characters.Delete(id);

            return deleted > 0;
        }
    }
}