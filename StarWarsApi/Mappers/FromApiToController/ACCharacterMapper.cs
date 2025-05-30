using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACCharacterMapper
    {
        private static ACCharacterMapper instance;
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
        public CharacterDto MapToController(CharacterApi acCharacter)
        {
            if (acCharacter == null)
            {
                return null;
            }
            return new CharacterDto
            {
                Name = acCharacter.Name,
                Url = acCharacter.Url
            };
        }

        public List<CharacterDto> MapToControllerList(List<CharacterApi> acCharacters)
        {
            if (acCharacters == null || !acCharacters.Any())
            {
                return new List<CharacterDto>();
            }
            return acCharacters.Select(ac => MapToController(ac)).ToList();
        }
    }
}
