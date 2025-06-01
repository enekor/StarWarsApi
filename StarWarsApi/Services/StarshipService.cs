using StarWarsApi.Models;
using StarWarsApi.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.api;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.database;
using BDCADAO.BDModels;

namespace StarWarsApi.Services
{
    public class StarshipService : BaseService
    {
        public StarshipService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<StarshipDto>> GetAllStarshipsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/starships");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var starships = JsonSerializer.Deserialize<List<StarshipApi>>(content);

                if (starships == null)
                    return new List<StarshipDto>();

                return starships
                    .Select(s => ACStarshipMapper.Instance.MapToController(s))
                    .Where(s => s != null)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener las naves: {e.Message}");
                return new List<StarshipDto>();
            }
        }

        public async Task<StarshipDto?> GetStarshipByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/starships/{id}");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var starship = JsonSerializer.Deserialize<StarshipApi>(content);

                return ACStarshipMapper.Instance.MapToController(starship);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener la nave con id {id}: {e.Message}");
                return null;
            }
        }

        public List<StarshipDto> GetStarshipsFromDB()
        {
            var starships = _context.Starships.GetAll();
            if (starships == null)
                return new List<StarshipDto>();

            return DCStarshipMapper.Instance.ToDtoList(starships);
        }

        public async Task SaveStarshipAsync(StarshipDto starshipDto)
        {
            var starship = CDStarshipMapper.Instance.ToEntity(starshipDto);
            if (starship != null)
            {
                if (string.IsNullOrEmpty(starship.Uid))
                {
                    starship.Uid = starshipDto.Url?.Split('/').Last() ?? Guid.NewGuid().ToString();
                }
                _context.Starships.InsertOrUpdate(starship);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteStarshipAsync(string id)
        {
            var deleted = _context.Starships.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public StarshipDto? GetStarshipFromDB(string id)
        {
            var starship = _context.Starships.GetById(id);
            return starship != null ? DCStarshipMapper.Instance.ToDto(starship) : null;
        }
    }
}