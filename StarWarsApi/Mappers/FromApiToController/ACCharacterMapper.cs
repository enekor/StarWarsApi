using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACCharacterMapper
    {

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
    }
}
