using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADCharacterMapper
    {
        public Character MapToDatabase(CharacterApi acCharacter)
        {
            if (acCharacter == null)
            {
                return null;
            }
            return new Character
            {
                Uid = acCharacter.Uid,
                Name = acCharacter.Name,
                Url = acCharacter.Url
            };
        }
    }
}
