using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDPlanetMapper
    {
        private static CDPlanetMapper instance;
        private CDPlanetMapper() { }
        public static CDPlanetMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDPlanetMapper();
                }
                return instance;
            }
        }
        
        public Planet ToEntity(PlanetDto planetDto)
        {
            if (planetDto == null)
                return null;

            return new Planet
            {
                Name = planetDto.Name,
                url = planetDto.url,
            };
        }

        public List<Planet> ToEntityList(List<PlanetDto> planetsDto)
        {
            if (planetsDto == null || !planetsDto.Any())
                return new List<Planet>();

            return planetsDto.Select(p => ToEntity(p)).ToList();
        }
    }
}