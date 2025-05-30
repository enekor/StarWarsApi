using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADSpecieMapper
    {
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
    }
}
