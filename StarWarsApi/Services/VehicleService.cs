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
    public class VehicleService : BaseService
    {
        public VehicleService(HttpClient httpClient, ModelContext context) : base(context,httpClient)
        {
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/vehicles");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var vehicles = JsonSerializer.Deserialize<List<VehicleApi>>(content);

                if (vehicles == null)
                    return new List<VehicleDto>();

                return vehicles
                    .Select(v => ACVehicleMapper.Instance.MapToController(v))
                    .Where(v => v != null)
                    .ToList()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los vehículos: {e.Message}");
                return new List<VehicleDto>();
            }
        }

        public async Task<VehicleDto?> GetVehicleByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_connectionString}/vehicles/{id}");
                response.EnsureSuccessStatusCode();
            
                var content = await response.Content.ReadAsStringAsync();
                var vehicle = JsonSerializer.Deserialize<VehicleApi>(content);

                return ACVehicleMapper.Instance.MapToController(vehicle);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el vehículo con id {id}: {e.Message}");
                return null;
            }
        }

        public List<VehicleDto> GetVehiclesFromDB()
        {
            var vehicles = _context.Vehicles.GetAll();
            if (vehicles == null)
                return new List<VehicleDto>();

            return DCVehicleMapper.Instance.ToDtoList(vehicles);
        }

        public async Task SaveVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = CDVehicleMapper.Instance.ToEntity(vehicleDto);
            if (vehicle != null)
            {
                _context.Vehicles.InsertOrUpdate(vehicle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteVehicleAsync(string id)
        {
            var deleted = _context.Vehicles.Delete(id);
            await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public VehicleDto? GetVehicleFromDB(string id)
        {
            var vehicle = _context.Vehicles.GetById(id);
            return vehicle != null ? DCVehicleMapper.Instance.ToDto(vehicle) : null;
        }
    }
}