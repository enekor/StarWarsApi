using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCSpecieMapper
    {
        private static DCSpecieMapper? instance;
        private DCSpecieMapper() { }
        public static DCSpecieMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCSpecieMapper();
                }
                return instance;
            }
        }
        
        public SpeciesDto? ToDto(Species? species)
        {
            if (species == null)
                return null;

            return new SpeciesDto
            {
                Description = species.Description,
                Created = species.Created,
                Edited = species.Edited,
                Classification = species.Classification,
                Name = species.Name,
                Designation = species.Designation,
                Language = species.Language,
                Homeworld = species.Homeworld,
                AverageLifespan = species.AverageLifespan,
                AverageHeight = species.AverageHeight,
                Url = species.Url
            };
        }

        public List<SpeciesDto> ToDtoList(List<Species>? species)
        {
            if (species == null || !species.Any())
                return new List<SpeciesDto>();

            return species.Select(s => ToDto(s)).Where(s => s != null).ToList()!;
        }
    }
}