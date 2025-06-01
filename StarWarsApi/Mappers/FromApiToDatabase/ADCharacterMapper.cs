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
                Uid = acCharacter.result.uid,
                Name = acCharacter.result.properties.name,
                Url = acCharacter.result.properties.url,
                BirthYear = acCharacter.result.properties.birth_year,
                Created = acCharacter.result.properties.created,
                Edited = acCharacter.result.properties.edited,
                Description = acCharacter.result.description,
                EyeColor = acCharacter.result.properties.eye_color,
                Gender = acCharacter.result.properties.gender,
                HairColor = acCharacter.result.properties.hair_color,
                Height = acCharacter.result.properties.height,
                Mass = acCharacter.result.properties.mass,
                Homeworld = acCharacter.result.properties.homeworld,
                SkinColor = acCharacter.result.properties.skin_color,
                
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
