using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACSpecieMapper
    {
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
    }
}
