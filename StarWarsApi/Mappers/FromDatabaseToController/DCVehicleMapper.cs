
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCVehicleMapper
    {
        private static DCVehicleMapper instance;
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
        
        public  VehicleDto ToDto(Vehicle vehicle)
        {
            if (vehicle == null)
                return null;

            return new VehicleDto
            {
                Name = vehicle.Name,
                Url = vehicle.Url,
            };
        }

        public  List<VehicleDto> ToDtoList(List<Vehicle> vehicles)
        {
            if (vehicles == null || !vehicles.Any())
                return new List<VehicleDto>();

            return vehicles.Select(v => ToDto(v)).ToList();
        }
    }
}