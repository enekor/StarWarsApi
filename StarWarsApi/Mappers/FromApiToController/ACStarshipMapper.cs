using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACStarshipMapper
    {
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
    }
}
