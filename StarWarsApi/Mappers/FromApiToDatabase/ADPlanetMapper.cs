using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADPlanetMapper
    {
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
    }
}
