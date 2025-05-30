using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACVehicleMapper
    {
        private static ACVehicleMapper instance;
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

        public List<VehicleDto> MapToControllerList(List<VehicleApi> acVehicles)
        {
            if (acVehicles == null || !acVehicles.Any())
            {
                return new List<VehicleDto>();
            }
            return acVehicles.Select(ac => MapToController(ac)).ToList();
        }
    }
}
