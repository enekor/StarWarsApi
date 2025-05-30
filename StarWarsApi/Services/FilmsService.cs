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
    public class FilmsService : BaseService
    {
        private readonly HttpClient _httpClient;

        public FilmsService(HttpClient httpClient, ModelContext context) : base(context)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FilmsDto>> GetAllFilmsAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/films");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var films = JsonSerializer.Deserialize<List<FilmsApi>>(content);
            
            return films.Select(f => ACFilmMapper.Instance.MapToController(f)).ToList();
        }

        public async Task<FilmsDto> GetFilmByIdAsync(string id)
        {
            var data = _context.Films.GetById(id);
            if (data != null)
            {
                return DCFilmMapper.Instance.ToDto(data);
            }

            return null;
        }

        public async void SaveFilmAsync(FilmsDto filmDto)
        {
            var film = CDFilmMapper.Instance.ToEntity(filmDto);
            _context.Films.InsertOrUpdate(film);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFilmAsync(string id)
        {
            int deleted = _context.Films.Delete(id);

            return deleted > 0;
        }
    }
}