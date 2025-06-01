using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACPlanetMapper
    {
        private static ACPlanetMapper? instance;
        private ACPlanetMapper() { }
        public static ACPlanetMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACPlanetMapper();
                }
                return instance;
            }
        }
        
        public PlanetDto? MapToController(PLanetApu? acPlanet)
        {
            if (acPlanet?.result == null)
            {
                return null;
            }

            return new PlanetDto
            {
                Description = acPlanet.result.description,
                Created = acPlanet.result.properties.created,
                Edited = acPlanet.result.properties.edited,
                Climate = acPlanet.result.properties.climate,
                SurfaceWater = acPlanet.result.properties.surface_water,
                Name = acPlanet.result.properties.name,
                Diameter = acPlanet.result.properties.diameter,
                RotationPeriod = acPlanet.result.properties.rotation_period,
                Terrain = acPlanet.result.properties.terrain,
                Gravity = acPlanet.result.properties.gravity,
                OrbitalPeriod = acPlanet.result.properties.orbital_period,
                Population = acPlanet.result.properties.population,
                Url = acPlanet.result.properties.url
            };
        }

        public List<PlanetDto> MapToControllerList(List<PLanetApu>? acPlanets)
        {
            if (acPlanets == null || !acPlanets.Any())
                return new List<PlanetDto>();

            return acPlanets.Select(p => MapToController(p)).Where(p => p != null).ToList()!;
        }
    }
}
