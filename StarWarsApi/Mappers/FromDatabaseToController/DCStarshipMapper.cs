using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCStarshipMapper
    {
        private static DCStarshipMapper? instance;
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
        
        public StarshipDto? ToDto(Starship? starship)
        {
            if (starship == null)
                return null;

            return new StarshipDto
            {
                Description = starship.Description,
                Created = starship.Created,
                Edited = starship.Edited,
                Consumables = starship.Consumables,
                Name = starship.Name,
                CargoCapacity = starship.CargoCapacity,
                Passengers = starship.Passengers,
                MaxAtmospheringSpeed = starship.MaxAtmospheringSpeed,
                Crew = starship.Crew,
                Length = starship.Length,
                Model = starship.Model,
                CostInCredits = starship.CostInCredits,
                Manufacturer = starship.Manufacturer,
                MGLT = starship.MGLT,
                StarshipClass = starship.StarshipClass,
                HyperdriveRating = starship.HyperdriveRating,
                Url = starship.Url
            };
        }

        public List<StarshipDto> ToDtoList(List<Starship>? starships)
        {
            if (starships == null || !starships.Any())
                return new List<StarshipDto>();

            return starships.Select(s => ToDto(s)).Where(s => s != null).ToList()!;
        }
    }
}