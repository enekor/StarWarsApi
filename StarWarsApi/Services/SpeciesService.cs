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
    public class SpeciesService : BaseService
    {
        public SpeciesService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<SpeciesDto>> GetAllSpeciesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/species");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var species = JsonSerializer.Deserialize<List<SpecieApi>>(content);

                if (species == null)
                    return new List<SpeciesDto>();

                return species
                    .Select(s => ACSpecieMapper.Instance.MapToController(s))
                    .Where(s => s != null)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener las especies: {e.Message}");
                return new List<SpeciesDto>();
            }
        }

        public async Task<SpeciesDto?> GetSpeciesByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/species/{id}");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var species = JsonSerializer.Deserialize<SpecieApi>(content);

                return ACSpecieMapper.Instance.MapToController(species);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener la especie con id {id}: {e.Message}");
                return null;
            }
        }

        public List<SpeciesDto> GetSpeciesFromDB()
        {
            var species = _context.Species.GetAll();
            if (species == null)
                return new List<SpeciesDto>();

            return DCSpecieMapper.Instance.ToDtoList(species);
        }

        public async Task SaveSpeciesAsync(SpeciesDto speciesDto)
        {
            var species = CDSpecieMapper.Instance.ToEntity(speciesDto);
            if (species != null)
            {
                _context.Species.InsertOrUpdate(species);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteSpeciesAsync(string id)
        {
            var deleted = _context.Species.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public SpeciesDto? GetSpecieFromDB(string id)
        {
            var species = _context.Species.GetById(id);
            return species != null ? DCSpecieMapper.Instance.ToDto(species) : null;
        }
    }
}