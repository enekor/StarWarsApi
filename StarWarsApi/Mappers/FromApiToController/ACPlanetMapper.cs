using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACPlanetMapper
    {
        public PlanetDto MapToController(PlanetApi acPlanet)
        {
            if (acPlanet == null)
            {
                return null;
            }
            return new PlanetDto
            {
                Name = acPlanet.Name,
                url = acPlanet.url
            };
        }
    }
}
