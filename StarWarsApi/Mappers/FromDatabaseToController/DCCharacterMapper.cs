using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCCharacterMapper
    {
        private static DCCharacterMapper instance;
        private DCCharacterMapper() { }
        public static DCCharacterMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCCharacterMapper();
                }
                return instance;
            }
        }
        
        public  CharacterDto ToDto(Character character)
        {
            if (character == null)
                return null;

            return new CharacterDto
            {
                Name = character.Name,
                Url = character.Url,
            };
        }

        public  List<CharacterDto> ToDtoList(List<Character> characters)
        {
            if (characters == null || !characters.Any())
                return new List<CharacterDto>();

            return characters.Select(c => ToDto(c)).ToList();
        }
    }
}