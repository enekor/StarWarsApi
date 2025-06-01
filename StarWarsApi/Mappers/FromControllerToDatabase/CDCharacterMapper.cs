using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDCharacterMapper
    {
        private static CDCharacterMapper? instance;
        private CDCharacterMapper() { }
        public static CDCharacterMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDCharacterMapper();
                }
                return instance;
            }
        }
        
        public Character? ToEntity(CharacterDto? characterDto)
        {
            if (characterDto == null)
                return null;

            return new Character
            {
                Description = characterDto.Description,
                Created = characterDto.Created,
                Edited = characterDto.Edited,
                Name = characterDto.Name,
                Gender = characterDto.Gender,
                SkinColor = characterDto.SkinColor,
                HairColor = characterDto.HairColor,
                Height = characterDto.Height,
                EyeColor = characterDto.EyeColor,
                Mass = characterDto.Mass,
                Homeworld = characterDto.Homeworld,
                BirthYear = characterDto.BirthYear,
                Url = characterDto.Url
            };
        }

        public List<Character> ToEntityList(List<CharacterDto>? charactersDto)
        {
            if (charactersDto == null || !charactersDto.Any())
                return new List<Character>();

            return charactersDto.Select(c => ToEntity(c)).Where(c => c != null).ToList()!;
        }
    }
}