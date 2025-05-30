using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACStarshipMapper
    {
        private static ACStarshipMapper instance;
        private ACStarshipMapper() { }
        public static ACStarshipMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACStarshipMapper();
                }
                return instance;
            }
        }
        public StarshipDto MapToController(StarshipApi acStarship)
        {
            if (acStarship == null)
            {
                return null;
            }
            return new StarshipDto
            {
                Name = acStarship.Name,
                Url = acStarship.Url
            };
        }

        public List<StarshipDto> MapToControllerList(List<StarshipApi> acStarships)
        {
            if (acStarships == null || !acStarships.Any())
            {
                return new List<StarshipDto>();
            }
            return acStarships.Select(ac => MapToController(ac)).ToList();
        }
    }
}
