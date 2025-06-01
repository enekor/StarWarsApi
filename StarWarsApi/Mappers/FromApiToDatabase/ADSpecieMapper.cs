using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADSpecieMapper
    {
        private static ADSpecieMapper instance;
        private ADSpecieMapper() { }
        public static ADSpecieMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADSpecieMapper();
                }
                return instance;
            }
        }
        public Species MapToDatabase(SpecieApi specieApi)
        {
            if (specieApi == null)
            {
                return null;
            }
            return new Species
            {
                AverageHeight = specieApi.result.properties.average_height,
                AverageLifespan = specieApi.result.properties.average_lifespan,
                Classification = specieApi.result.properties.classification,
                Created = specieApi.result.properties.created,
                Description = specieApi.result.description,
                Designation = specieApi.result.properties.designation,
                Edited = specieApi.result.properties.edited,
                Homeworld = specieApi.result.properties.homeworld,
                Language = specieApi.result.properties.language,
                Name = specieApi.result.properties.name,
                Uid = specieApi.result.uid,
                Url = specieApi.result.properties.url,
            };
        }

        public List<Species> MapToDatabaseList(List<SpecieApi> speciesApiList)
        {
            if (speciesApiList == null || !speciesApiList.Any())
            {
                return new List<Species>();
            }
            return speciesApiList.Select(specie => MapToDatabase(specie)).ToList();
        }
    }
}
