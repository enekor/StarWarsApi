using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCCharacterMapper
    {
        private static DCCharacterMapper? instance;
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
        
        public CharacterDto? ToDto(Character? character)
        {
            if (character == null)
                return null;

            return new CharacterDto
            {
                Description = character.Description,
                Created = character.Created,
                Edited = character.Edited,
                Name = character.Name,
                Gender = character.Gender,
                SkinColor = character.SkinColor,
                HairColor = character.HairColor,
                Height = character.Height,
                EyeColor = character.EyeColor,
                Mass = character.Mass,
                Homeworld = character.Homeworld,
                BirthYear = character.BirthYear,
                Url = character.Url
            };
        }

        public List<CharacterDto> ToDtoList(List<Character>? characters)
        {
            if (characters == null || !characters.Any())
                return new List<CharacterDto>();

            return characters.Select(c => ToDto(c)).Where(c => c != null).ToList()!;
        }
    }
}