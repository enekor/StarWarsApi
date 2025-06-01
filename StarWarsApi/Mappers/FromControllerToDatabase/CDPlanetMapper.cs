using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDPlanetMapper
    {
        private static CDPlanetMapper? instance;
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
        
        public Planet? ToEntity(PlanetDto? planetDto)
        {
            if (planetDto == null)
                return null;

            return new Planet
            {
                Description = planetDto.Description,
                Created = planetDto.Created,
                Edited = planetDto.Edited,
                Climate = planetDto.Climate,
                SurfaceWater = planetDto.SurfaceWater,
                Name = planetDto.Name,
                Diameter = planetDto.Diameter,
                RotationPeriod = planetDto.RotationPeriod,
                Terrain = planetDto.Terrain,
                Gravity = planetDto.Gravity,
                OrbitalPeriod = planetDto.OrbitalPeriod,
                Population = planetDto.Population,
                Url = planetDto.Url
            };
        }

        public List<Planet> ToEntityList(List<PlanetDto>? planetsDto)
        {
            if (planetsDto == null || !planetsDto.Any())
                return new List<Planet>();

            return planetsDto.Select(p => ToEntity(p)).Where(p => p != null).ToList()!;
        }
    }
}