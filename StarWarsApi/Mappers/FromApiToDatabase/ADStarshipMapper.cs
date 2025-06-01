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
                Uid = starshipApi.result.uid,
                Name = starshipApi.result.properties.name,
                Model = starshipApi.result.properties.model,
                Manufacturer = starshipApi.result.properties.manufacturer,
                CostInCredits = starshipApi.result.properties.cost_in_credits,
                Length = starshipApi.result.properties.length,
                MaxAtmospheringSpeed = starshipApi.result.properties.max_atmosphering_speed,
                Crew = starshipApi.result.properties.crew,
                Passengers = starshipApi.result.properties.passengers,
                CargoCapacity = starshipApi.result.properties.cargo_capacity,
                Consumables = starshipApi.result.properties.consumables,
                HyperdriveRating = starshipApi.result.properties.hyperdrive_rating,
                MGLT = starshipApi.result.properties.MGLT,
                StarshipClass = starshipApi.result.properties.starship_class,
                Created = starshipApi.result.properties.created,
                Edited = starshipApi.result.properties.edited,
                Description = starshipApi.result.description,
                Url = starshipApi.result.properties.url
                
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
