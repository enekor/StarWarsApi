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
using StarWarsApi.Daos;
using StarWarsApi.Mappers.FromControllerToDatabase;

namespace StarWarsApi.Services
{
    public class StarshipService : BaseService
    {
        private readonly HttpClient _httpClient;

        public StarshipService(HttpClient httpClient, ModelContext context) : base(context)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StarshipDto>> GetAllStarshipsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/starships");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var starships = JsonSerializer.Deserialize<List<StarshipApi>>(content);
            
            return starships.Select(s => ACStarshipMapper.Instance.MapToController(s)).ToList();
        }

        public async Task<StarshipDto> GetStarshipByIdAsync(string id)
        {
            var data = _context.Starships.GetById(id);
            if (data != null)
            {
                return DCStarshipMapper.Instance.ToDto(data);
            }

            return null;
        }

        public async void SaveStarshipAsync(StarshipDto starshipDto)
        {
            var starship = CDStarshipMapper.Instance.ToEntity(starshipDto);
            _context.Starships.InsertOrUpdate(starship);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStarshipAsync(string id)
        {
            int deleted = _context.Starships.Delete(id);

            return deleted > 0;
        }
    }
}