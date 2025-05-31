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
    {        public PlanetsService(HttpClient httpClient, ModelContext context) : base(context, httpClient)
        {
        }

        public async Task<List<PlanetDto>> GetAllPlanetsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/planets");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var planets = JsonSerializer.Deserialize<List<PlanetApi>>(content);
            
            return planets.Select(p => ACPlanetMapper.Instance.MapToController(p)).ToList();
        }        public async Task<PlanetDto?> GetPlanetByIdAsync(string id)
        {
            PlanetDto? planetDto = null;
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/planets/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var planetApi = JsonSerializer.Deserialize<PlanetApi>(content);
                    if (planetApi != null)
                    {
                        planetDto = ACPlanetMapper.Instance.MapToController(planetApi);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error al obtener el planeta con id {id}: {e.Message}");
            }
            
            if (planetDto == null)
            {
                // Si no se encuentra en la API o hay error, intentar obtener de la base de datos
                var data = _context.Planets.GetById(id);
                planetDto = data != null ? DCPlanetMapper.Instance.ToDto(data) : null;
            }
            
            return planetDto;
        }        public async Task SavePlanetAsync(PlanetDto planetDto)
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

        public PlanetDto GetPlanetFromDB(string id)
        {
            var planet = _context.Planets.GetById(id);
            return planet != null ? DCPlanetMapper.Instance.ToDto(planet) : null;
        }
    }
}