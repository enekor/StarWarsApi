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
        public Planet MapToDatabase(PlanetApi acPlanet)
        {
            if (acPlanet == null)
            {
                return null;
            }
            return new Planet
            {
                Uid = acPlanet.Uid,
                Name = acPlanet.Name,
                url = acPlanet.url
            };
        }

        public List<Planet> MapToDatabaseList(List<PlanetApi> acPlanets)
        {
            if (acPlanets == null || !acPlanets.Any())
            {
                return new List<Planet>();
            }
            return acPlanets.Select(ac => MapToDatabase(ac)).ToList();
        }
    }
}
