using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCVehicleMapper
    {
        private static DCVehicleMapper? instance;
        private DCVehicleMapper() { }
        public static DCVehicleMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCVehicleMapper();
                }
                return instance;
            }
        }
        
        public VehicleDto? ToDto(Vehicle? vehicle)
        {
            if (vehicle == null)
                return null;

            return new VehicleDto
            {
                Description = vehicle.Description,
                Created = vehicle.Created,
                Edited = vehicle.Edited,
                Consumables = vehicle.Consumables,
                Name = vehicle.Name,
                CargoCapacity = vehicle.CargoCapacity,
                Passengers = vehicle.Passengers,
                MaxAtmospheringSpeed = vehicle.MaxAtmospheringSpeed,
                Crew = vehicle.Crew,
                Length = vehicle.Length,
                Model = vehicle.Model,
                CostInCredits = vehicle.CostInCredits,
                Manufacturer = vehicle.Manufacturer,
                VehicleClass = vehicle.VehicleClass,
                Url = vehicle.Url
            };
        }

        public List<VehicleDto> ToDtoList(List<Vehicle>? vehicles)
        {
            if (vehicles == null || !vehicles.Any())
                return new List<VehicleDto>();

            return vehicles.Select(v => ToDto(v)).Where(v => v != null).ToList()!;
        }
    }
}