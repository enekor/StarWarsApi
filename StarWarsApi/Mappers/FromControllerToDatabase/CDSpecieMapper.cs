using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDSpecieMapper
    {
        private static CDSpecieMapper? instance;
        private CDSpecieMapper() { }
        public static CDSpecieMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDSpecieMapper();
                }
                return instance;
            }
        }
        
        public Species? ToEntity(SpeciesDto? specieDto)
        {
            if (specieDto == null)
                return null;

            return new Species
            {
                Description = specieDto.Description,
                Created = specieDto.Created,
                Edited = specieDto.Edited,
                Classification = specieDto.Classification,
                Name = specieDto.Name,
                Designation = specieDto.Designation,
                Language = specieDto.Language,
                Homeworld = specieDto.Homeworld,
                AverageLifespan = specieDto.AverageLifespan,
                AverageHeight = specieDto.AverageHeight,
                Url = specieDto.Url
            };
        }

        public List<Species> ToEntityList(List<SpeciesDto>? speciesDto)
        {
            if (speciesDto == null || !speciesDto.Any())
                return new List<Species>();

            return speciesDto.Select(s => ToEntity(s)).Where(s => s != null).ToList()!;
        }
    }
}