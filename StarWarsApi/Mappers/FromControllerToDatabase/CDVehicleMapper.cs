using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDVehicleMapper
    {
        private static CDVehicleMapper instance;
        private CDVehicleMapper() { }
        public static CDVehicleMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDVehicleMapper();
                }
                return instance;
            }
        }
        
        public Vehicle ToEntity(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                return null;

            return new Vehicle
            {
                Name = vehicleDto.Name,
                Url = vehicleDto.Url,
            };
        }

        public List<Vehicle> ToEntityList(List<VehicleDto> vehiclesDto)
        {
            if (vehiclesDto == null || !vehiclesDto.Any())
                return new List<Vehicle>();

            return vehiclesDto.Select(v => ToEntity(v)).ToList();
        }
    }
}