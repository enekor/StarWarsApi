using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADVehicleMapper
    {
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
    }
}
