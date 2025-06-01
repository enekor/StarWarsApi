using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACCharacterMapper
    {
        private static ACCharacterMapper? instance;
        private ACCharacterMapper() { }

        public static ACCharacterMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACCharacterMapper();
                }
                return instance;
            }
        }

        public CharacterDto? MapToController(CharacterApi? acCharacter)
        {
            if (acCharacter?.result == null)
            {
                return null;
            }
            
            return new CharacterDto
            {
                Description = acCharacter.result.description,
                Created = acCharacter.result.properties.created,
                Edited = acCharacter.result.properties.edited,
                Name = acCharacter.result.properties.name,
                Gender = acCharacter.result.properties.gender,
                SkinColor = acCharacter.result.properties.skin_color,
                HairColor = acCharacter.result.properties.hair_color,
                Height = acCharacter.result.properties.height,
                EyeColor = acCharacter.result.properties.eye_color,
                Mass = acCharacter.result.properties.mass,
                Homeworld = acCharacter.result.properties.homeworld,
                BirthYear = acCharacter.result.properties.birth_year,
                Url = acCharacter.result.properties.url
            };
        }

        public List<CharacterDto> MapToControllerList(List<CharacterApi>? acCharacters)
        {
            if (acCharacters == null || !acCharacters.Any())
            {
                return new List<CharacterDto>();
            }
            return acCharacters.Select(ac => MapToController(ac)).Where(c => c != null).ToList()!;
        }
    }
}
