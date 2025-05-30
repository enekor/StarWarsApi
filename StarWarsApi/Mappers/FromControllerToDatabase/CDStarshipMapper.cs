using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDStarshipMapper
    {
        private static CDStarshipMapper instance;
        private CDStarshipMapper() { }
        public static CDStarshipMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDStarshipMapper();
                }
                return instance;
            }
        }
        
        public Starship ToEntity(StarshipDto starshipDto)
        {
            if (starshipDto == null)
                return null;

            return new Starship
            {
                Name = starshipDto.Name,
                Url = starshipDto.Url,
            };
        }

        public List<Starship> ToEntityList(List<StarshipDto> starshipsDto)
        {
            if (starshipsDto == null || !starshipsDto.Any())
                return new List<Starship>();

            return starshipsDto.Select(s => ToEntity(s)).ToList();
        }
    }
}