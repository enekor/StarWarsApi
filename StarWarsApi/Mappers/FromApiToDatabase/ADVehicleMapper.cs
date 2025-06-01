using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADVehicleMapper
    {
        private static ADVehicleMapper instance;
        private ADVehicleMapper() { }
        public static ADVehicleMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADVehicleMapper();
                }
                return instance;
            }
        }
        public Vehicle MapToDatabase(VehicleApi vehicle)
        {
            if (vehicle == null)
            {
                return null;
            }
            return new Vehicle
            {
                Uid = vehicle.result.uid,
                Name = vehicle.result.properties.name,
                Model = vehicle.result.properties.model,
                CargoCapacity = vehicle.result.properties.cargo_capacity,
                Consumables = vehicle.result.properties.consumables,
                CostInCredits = vehicle.result.properties.cost_in_credits,
                Crew = vehicle.result.properties.crew,
                Passengers = vehicle.result.properties.passengers,
                MaxAtmospheringSpeed = vehicle.result.properties.max_atmosphering_speed,
                Length = vehicle.result.properties.length,
                Created = vehicle.result.properties.created,
                Edited = vehicle.result.properties.edited,
                Description = vehicle.result.description,
                Manufacturer = vehicle.result.properties.manufacturer,
                Url = vehicle.result.properties.url,
                VehicleClass = vehicle.result.properties.vehicle_class,

            };
        }

        public List<Vehicle> MapToDatabaseList(List<VehicleApi> vehicles)
        {
            if (vehicles == null || !vehicles.Any())
            {
                return new List<Vehicle>();
            }
            return vehicles.Select(v => MapToDatabase(v)).ToList();
        }
    }
}
