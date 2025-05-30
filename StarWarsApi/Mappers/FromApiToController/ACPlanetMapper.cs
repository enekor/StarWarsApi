using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACPlanetMapper
    {
        private static ACPlanetMapper instance;
        private ACPlanetMapper() { }
        public static ACPlanetMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACPlanetMapper();
                }
                return instance;
            }
        }
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

        public List<PlanetDto> MapToControllerList(List<PlanetApi> acPlanets)
        {
            if (acPlanets == null || !acPlanets.Any())
            {
                return new List<PlanetDto>();
            }
            return acPlanets.Select(ac => MapToController(ac)).ToList();
        }
    }
}
