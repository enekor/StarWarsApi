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
                Uid = vehicle.Uid,
                Name = vehicle.Name,
                Url = vehicle.Url
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
