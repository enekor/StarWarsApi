using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCPlanetMapper
    {
        private static DCPlanetMapper? instance;
        private DCPlanetMapper() { }
        public static DCPlanetMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCPlanetMapper();
                }
                return instance;
            }
        }
        
        public PlanetDto? ToDto(Planet? planet)
        {
            if (planet == null)
                return null;

            return new PlanetDto
            {
                Description = planet.Description,
                Created = planet.Created,
                Edited = planet.Edited,
                Climate = planet.Climate,
                SurfaceWater = planet.SurfaceWater,
                Name = planet.Name,
                Diameter = planet.Diameter,
                RotationPeriod = planet.RotationPeriod,
                Terrain = planet.Terrain,
                Gravity = planet.Gravity,
                OrbitalPeriod = planet.OrbitalPeriod,
                Population = planet.Population,
                Url = planet.Url
            };
        }

        public List<PlanetDto> ToDtoList(List<Planet>? planets)
        {
            if (planets == null || !planets.Any())
                return new List<PlanetDto>();

            return planets.Select(p => ToDto(p)).Where(p => p != null).ToList()!;
        }
    }
}