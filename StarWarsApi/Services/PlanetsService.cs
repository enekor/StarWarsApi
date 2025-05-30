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
    public class PlanetsService : BaseService
    {
        private readonly HttpClient _httpClient;

        public PlanetsService(HttpClient httpClient, ModelContext context) : base(context)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlanetDto>> GetAllPlanetsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/planets");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var planets = JsonSerializer.Deserialize<List<PlanetApi>>(content);
            
            return planets.Select(p => ACPlanetMapper.Instance.MapToController(p)).ToList();
        }

        public async Task<PlanetDto> GetPlanetByIdAsync(string id)
        {
            var data = _context.Planets.GetById(id);
            if (data != null)
            {
                return DCPlanetMapper.Instance.ToDto(data);
            }

            return null;
        }

        public async void SavePlanetAsync(PlanetDto planetDto)
        {
            var planet = CDPlanetMapper.Instance.ToEntity(planetDto);
            _context.Planets.InsertOrUpdate(planet);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePlanetAsync(string id)
        {
            int deleted = _context.Planets.Delete(id);

            return deleted > 0;
        }
    }
}