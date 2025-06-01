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
        public PlanetsService(HttpClient httpClient, ModelContext context) : base(context, httpClient)
        {
        }

        public async Task<List<PlanetDto>> GetAllPlanetsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/planets");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var planets = JsonSerializer.Deserialize<List<PLanetApu>>(content);

                if (planets == null)
                    return new List<PlanetDto>();

                return planets
                    .Select(p => ACPlanetMapper.Instance.MapToController(p))
                    .Where(p => p != null)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los planetas: {e.Message}");
                return new List<PlanetDto>();
            }
        }

        public async Task<PlanetDto?> GetPlanetByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/planets/{id}");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var planet = JsonSerializer.Deserialize<PLanetApu>(content);

                if (planet?.result == null)
                {
                    // Si no se encuentra en la API, intentar obtener de la base de datos
                    return GetPlanetFromDB(id);
                }

                return ACPlanetMapper.Instance.MapToController(planet);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el planeta con id {id}: {e.Message}");
                // Si hay error en la API, intentar obtener de la base de datos
                return GetPlanetFromDB(id);
            }
        }

        public async Task SavePlanetAsync(PlanetDto planetDto)
        {
            var planet = CDPlanetMapper.Instance.ToEntity(planetDto);
            if (planet != null)
            {
                _context.Planets.InsertOrUpdate(planet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeletePlanetAsync(string id)
        {
            var deleted = _context.Planets.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public PlanetDto? GetPlanetFromDB(string id)
        {
            var planet = _context.Planets.GetById(id);
            return planet != null ? DCPlanetMapper.Instance.ToDto(planet) : null;
        }

        public List<PlanetDto> GetPlanetsFromDB()
        {
            var planets = _context.Planets.GetAll();
            if (planets == null)
                return new List<PlanetDto>();

            return DCPlanetMapper.Instance.ToDtoList(planets);
        }
    }
}