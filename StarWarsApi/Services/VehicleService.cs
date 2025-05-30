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
    public class VehicleService : BaseService
    {

        public VehicleService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            var response = await _httpClient.GetAsync($"{_connectionString}/vehicles");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var vehicles = JsonSerializer.Deserialize<List<VehicleApi>>(content);
            
            return vehicles.Select(v => ACVehicleMapper.Instance.MapToController(v)).ToList();
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(string id)
        {
            VehicleDto? vehicleDto = null;
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/vehicles/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var vehicleApi = JsonSerializer.Deserialize<VehicleApi>(content);
                    if (vehicleApi != null)
                    {
                        vehicleDto = ACVehicleMapper.Instance.MapToController(vehicleApi);// Guardamos en BD para futuras consultas
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error al obtener el vehículo con id {id}: {e.Message}");
            }

            return vehicleDto;
        }

        public async void SaveVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = CDVehicleMapper.Instance.ToEntity(vehicleDto);
            _context.Vehicles.InsertOrUpdate(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVehicleAsync(string id)
        {
            int deleted = _context.Vehicles.Delete(id);

            return deleted > 0;
        }

        public VehicleDto GetVehicleFromDB(string id)
        {
            var vehicle = _context.Vehicles.GetById(id);
            return vehicle != null ? DCVehicleMapper.Instance.ToDto(vehicle) : null;
        }
    }
}