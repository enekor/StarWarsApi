using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADPlanetMapper
    {
        private static ADPlanetMapper instance;
        private ADPlanetMapper() { }
        public static ADPlanetMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADPlanetMapper();
                }
                return instance;
            }
        }
        public Planet MapToDatabase(PLanetApu acPlanet)
        {
            if (acPlanet == null)
            {
                return null;
            }
            return new Planet
            {
                Uid = acPlanet.result.uid,
                Name = acPlanet.result.properties.name,
                Climate = acPlanet.result.properties.climate,
                SurfaceWater = acPlanet.result.properties.surface_water,
                Diameter = acPlanet.result.properties.diameter,
                RotationPeriod = acPlanet.result.properties.rotation_period,
                Terrain = acPlanet.result.properties.terrain,
                Gravity = acPlanet.result.properties.gravity,
                OrbitalPeriod = acPlanet.result.properties.orbital_period,
                Population = acPlanet.result.properties.population,
                Url = acPlanet.result.properties.url,
                Created = acPlanet.result.properties.created,
                Edited = acPlanet.result.properties.edited,
                Description = acPlanet.result.description
                
            };
        }

        public List<Planet> MapToDatabaseList(List<PLanetApu> acPlanets)
        {
            if (acPlanets == null || !acPlanets.Any())
            {
                return new List<Planet>();
            }
            return acPlanets.Select(ac => MapToDatabase(ac)).ToList();
        }
    }
}
