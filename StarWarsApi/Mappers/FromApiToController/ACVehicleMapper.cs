using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACVehicleMapper
    {
        public VehicleDto MapToController(VehicleApi acVehicle)
        {
            if (acVehicle == null)
            {
                return null;
            }
            return new VehicleDto
            {
                Name = acVehicle.Name,
                Url = acVehicle.Url
            };
        }
    }
}
