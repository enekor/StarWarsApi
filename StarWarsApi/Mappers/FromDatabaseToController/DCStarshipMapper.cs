using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCStarshipMapper
    {
        private static DCStarshipMapper instance;
        private DCStarshipMapper() { }
        public static DCStarshipMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCStarshipMapper();
                }
                return instance;
            }
        }
        
        public  StarshipDto ToDto(Starship starship)
        {
            if (starship == null)
                return null;

            return new StarshipDto
            {
                Name = starship.Name,
                Url = starship.Url,
            };
        }

        public  List<StarshipDto> ToDtoList(List<Starship> starships)
        {
            if (starships == null || !starships.Any())
                return new List<StarshipDto>();

            return starships.Select(s => ToDto(s)).ToList();
        }
    }
}