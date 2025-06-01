using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACVehicleMapper
    {
        private static ACVehicleMapper? instance;
        private ACVehicleMapper() { }
        public static ACVehicleMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACVehicleMapper();
                }
                return instance;
            }
        }
        
        public VehicleDto? MapToController(VehicleApi? acVehicle)
        {
            if (acVehicle?.result == null)
            {
                return null;
            }

            return new VehicleDto
            {
                Description = acVehicle.result.description,
                Created = acVehicle.result.properties.created,
                Edited = acVehicle.result.properties.edited,
                Consumables = acVehicle.result.properties.consumables,
                Name = acVehicle.result.properties.name,
                CargoCapacity = acVehicle.result.properties.cargo_capacity,
                Passengers = acVehicle.result.properties.passengers,
                MaxAtmospheringSpeed = acVehicle.result.properties.max_atmosphering_speed,
                Crew = acVehicle.result.properties.crew,
                Length = acVehicle.result.properties.length,
                Model = acVehicle.result.properties.model,
                CostInCredits = acVehicle.result.properties.cost_in_credits,
                Manufacturer = acVehicle.result.properties.manufacturer,
                VehicleClass = acVehicle.result.properties.vehicle_class,
                Url = acVehicle.result.properties.url
            };
        }

        public List<VehicleDto> MapToControllerList(List<VehicleApi>? acVehicles)
        {
            if (acVehicles == null || !acVehicles.Any())
                return new List<VehicleDto>();

            return acVehicles.Select(v => MapToController(v)).Where(v => v != null).ToList()!;
        }
    }
}
