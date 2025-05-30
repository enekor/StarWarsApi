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
        public Species MapToDatabase(SpeciesApi specieApi)
        {
            if (specieApi == null)
            {
                return null;
            }
            return new Species
            {
                Uid = specieApi.Uid,
                Name = specieApi.Name,
                Url = specieApi.Url
            };
        }

        public List<Species> MapToDatabaseList(List<SpeciesApi> speciesApiList)
        {
            if (speciesApiList == null || !speciesApiList.Any())
            {
                return new List<Species>();
            }
            return speciesApiList.Select(specie => MapToDatabase(specie)).ToList();
        }
    }
}
