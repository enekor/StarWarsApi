using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACSpecieMapper
    {
        private static ACSpecieMapper? instance;
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
        
        public SpeciesDto? MapToController(SpecieApi? acSpecies)
        {
            if (acSpecies?.result == null)
            {
                return null;
            }

            return new SpeciesDto
            {
                Description = acSpecies.result.description,
                Created = acSpecies.result.properties.created,
                Edited = acSpecies.result.properties.edited,
                Classification = acSpecies.result.properties.classification,
                Name = acSpecies.result.properties.name,
                Designation = acSpecies.result.properties.designation,
                EyeColors = acSpecies.result.properties.eye_colors,
                People = acSpecies.result.properties.people,
                SkinColors = acSpecies.result.properties.skin_colors,
                Language = acSpecies.result.properties.language,
                HairColors = acSpecies.result.properties.hair_colors,
                Homeworld = acSpecies.result.properties.homeworld,
                AverageLifespan = acSpecies.result.properties.average_lifespan,
                AverageHeight = acSpecies.result.properties.average_height,
                Url = acSpecies.result.properties.url
            };
        }

        public List<SpeciesDto> MapToControllerList(List<SpecieApi>? acSpecies)
        {
            if (acSpecies == null || !acSpecies.Any())
                return new List<SpeciesDto>();

            return acSpecies.Select(s => MapToController(s)).Where(s => s != null).ToList()!;
        }
    }
}
