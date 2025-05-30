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
        private readonly HttpClient _httpClient;

        public SpeciesService(HttpClient httpClient, ModelContext context) : base(context)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SpeciesDto>> GetAllSpeciesAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/species");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var species = JsonSerializer.Deserialize<List<SpeciesApi>>(content);
            
            return species.Select(s => ACSpecieMapper.Instance.MapToController(s)).ToList();
        }

        public async Task<SpeciesDto> GetSpeciesByIdAsync(string id)
        {
            var data = _context.Species.GetById(id);
            if (data != null)
            {
                return DCSpecieMapper.Instance.ToDto(data);
            }

            return null;
        }

        public async void SaveSpeciesAsync(SpeciesDto speciesDto)
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
    }
}