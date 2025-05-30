using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADStarshipMapper
    {
        private static ADStarshipMapper instance;
        private ADStarshipMapper() { }
        public static ADStarshipMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADStarshipMapper();
                }
                return instance;
            }
        }
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

        public List<Starship> MapToDatabaseList(List<StarshipApi> starshipApiList)
        {
            if (starshipApiList == null || !starshipApiList.Any())
            {
                return new List<Starship>();
            }
            return starshipApiList.Select(starship => MapToDatabase(starship)).ToList();
        }
    }
}
