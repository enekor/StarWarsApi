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

        public StarshipService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<StarshipDto>> GetAllStarshipsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/starships");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var starships = JsonSerializer.Deserialize<List<StarshipApi>>(content);
            
            return starships.Select(s => ACStarshipMapper.Instance.MapToController(s)).ToList();
        }

        public async Task<StarshipDto?> GetStarshipByIdAsync(string id)
        {
            StarshipDto? starshipDto = null;
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/starships/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var starshipApi = JsonSerializer.Deserialize<StarshipApi>(content);
                    if (starshipApi != null)
                    {
                        starshipDto = ACStarshipMapper.Instance.MapToController(starshipApi);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error al obtener la nave con id {id}: {e.Message}");
            }

            if (starshipDto == null)
            {
                // Si no se encuentra en la API o hay error, intentar obtener de la base de datos
                var data = _context.Starships.GetById(id);
                starshipDto = data != null ? DCStarshipMapper.Instance.ToDto(data) : null;
            }

            return starshipDto;
        }

        public async Task SaveStarshipAsync(StarshipDto starshipDto)
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

        public StarshipDto GetStarshipFromDB(string id)
        {
            var starship = _context.Starships.GetById(id);
            return starship != null ? DCStarshipMapper.Instance.ToDto(starship) : null;
        }
    }
}