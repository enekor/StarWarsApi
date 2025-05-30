using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADCharacterMapper
    {
        private static ADCharacterMapper instance;
        private ADCharacterMapper() { }

        public static ADCharacterMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADCharacterMapper();
                }
                return instance;
            }
        }
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

        public List<Character> MapToDatabaseList(List<CharacterApi> acCharacters)
        {
            if (acCharacters == null || !acCharacters.Any())
            {
                return new List<Character>();
            }
            return acCharacters.Select(ac => MapToDatabase(ac)).ToList();
        }
    }
}
