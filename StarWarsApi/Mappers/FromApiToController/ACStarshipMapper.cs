using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACStarshipMapper
    {
        private static ACStarshipMapper? instance;
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
        
        public StarshipDto? MapToController(StarshipApi? acStarship)
        {
            if (acStarship?.result == null)
            {
                return null;
            }

            return new StarshipDto
            {
                Description = acStarship.result.description,
                Created = acStarship.result.properties.created,
                Edited = acStarship.result.properties.edited,
                Consumables = acStarship.result.properties.consumables,
                Name = acStarship.result.properties.name,
                CargoCapacity = acStarship.result.properties.cargo_capacity,
                Passengers = acStarship.result.properties.passengers,
                MaxAtmospheringSpeed = acStarship.result.properties.max_atmosphering_speed,
                Crew = acStarship.result.properties.crew,
                Length = acStarship.result.properties.length,
                Model = acStarship.result.properties.model,
                CostInCredits = acStarship.result.properties.cost_in_credits,
                Manufacturer = acStarship.result.properties.manufacturer,
                MGLT = acStarship.result.properties.MGLT,
                StarshipClass = acStarship.result.properties.starship_class,
                HyperdriveRating = acStarship.result.properties.hyperdrive_rating,
                Url = acStarship.result.properties.url
            };
        }

        public List<StarshipDto> MapToControllerList(List<StarshipApi>? acStarships)
        {
            if (acStarships == null || !acStarships.Any())
                return new List<StarshipDto>();

            return acStarships.Select(s => MapToController(s)).Where(s => s != null).ToList()!;
        }
    }
}
