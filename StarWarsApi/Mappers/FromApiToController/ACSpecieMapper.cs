using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACSpecieMapper
    {
        private static ACSpecieMapper instance;
        private ACSpecieMapper() { }
        public static ACSpecieMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACSpecieMapper();
                }
                return instance;
            }
        }
        public SpeciesDto MapToController(SpeciesApi acSpecie)
        {
            if (acSpecie == null)
            {
                return null;
            }
            return new SpeciesDto
            {
                Name = acSpecie.Name,
                Url = acSpecie.Url
            };
        }

        public List<SpeciesDto> MapToControllerList(List<SpeciesApi> acSpecies)
        {
            if (acSpecies == null || !acSpecies.Any())
            {
                return new List<SpeciesDto>();
            }
            return acSpecies.Select(ac => MapToController(ac)).ToList();
        }
    }
}
