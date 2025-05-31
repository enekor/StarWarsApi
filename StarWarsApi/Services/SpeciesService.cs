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
    public class SpeciesService : BaseService
    {

        public SpeciesService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {

        }

        public async Task<List<SpeciesDto>> GetAllSpeciesAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/species");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var species = JsonSerializer.Deserialize<List<SpeciesApi>>(content);
            
            return species.Select(s => ACSpecieMapper.Instance.MapToController(s)).ToList();
        }        public async Task<SpeciesDto?> GetSpeciesByIdAsync(string id)
        {
            SpeciesDto? speciesDto = null;
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/species/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var speciesApi = JsonSerializer.Deserialize<SpeciesApi>(content);
                    if (speciesApi != null)
                    {
                        speciesDto = ACSpecieMapper.Instance.MapToController(speciesApi);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error al obtener la especie con id {id}: {e.Message}");
            }
            
            if (speciesDto == null)
            {
                // Si no se encuentra en la API o hay error, intentar obtener de la base de datos
                var data = _context.Species.GetById(id);
                speciesDto = data != null ? DCSpecieMapper.Instance.ToDto(data) : null;
            }
            
            return speciesDto;
        }        public async Task SaveSpeciesAsync(SpeciesDto speciesDto)
        {
            var species = CDSpecieMapper.Instance.ToEntity(speciesDto);
            _context.Species.InsertOrUpdate(species);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSpeciesAsync(string id)
        {
            int deleted = _context.Species.Delete(id);

            return deleted > 0;
        }

        public SpeciesDto GetSpecieFromDB(string id)
        {
            var species = _context.Species.GetById(id);
            return species != null ? DCSpecieMapper.Instance.ToDto(species) : null;
        }
    }
}