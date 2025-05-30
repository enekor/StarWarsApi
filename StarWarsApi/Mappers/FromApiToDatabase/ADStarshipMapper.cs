using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADStarshipMapper
    {
        public Starship MapToDatabase(StarshipApi starshipApi)
        {
            if (starshipApi == null)
            {
                return null;
            }
            return new Starship
            {
                Uid = starshipApi.Uid,
                Name = starshipApi.Name,
                Url = starshipApi.Url
            };
        }
    }
}
