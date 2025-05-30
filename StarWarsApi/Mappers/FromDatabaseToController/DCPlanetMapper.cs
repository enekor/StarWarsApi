using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCPlanetMapper
    {
        private static DCPlanetMapper instance;
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
        
        public  PlanetDto ToDto(Planet planet)
        {
            if (planet == null)
                return null;

            return new PlanetDto
            {
                Name = planet.Name,
                url = planet.url,
            };
        }

        public  List<PlanetDto> ToDtoList(List<Planet> planets)
        {
            if (planets == null || !planets.Any())
                return new List<PlanetDto>();

            return planets.Select(p => ToDto(p)).ToList();
        }
    }
}