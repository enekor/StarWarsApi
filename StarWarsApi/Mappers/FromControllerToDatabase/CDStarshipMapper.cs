using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDStarshipMapper
    {
        private static CDStarshipMapper? instance;
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
        
        public Starship? ToEntity(StarshipDto? starshipDto)
        {
            if (starshipDto == null)
                return null;

            return new Starship
            {
                Description = starshipDto.Description,
                Created = starshipDto.Created,
                Edited = starshipDto.Edited,
                Consumables = starshipDto.Consumables,
                Name = starshipDto.Name,
                CargoCapacity = starshipDto.CargoCapacity,
                Passengers = starshipDto.Passengers,
                MaxAtmospheringSpeed = starshipDto.MaxAtmospheringSpeed,
                Crew = starshipDto.Crew,
                Length = starshipDto.Length,
                Model = starshipDto.Model,
                CostInCredits = starshipDto.CostInCredits,
                Manufacturer = starshipDto.Manufacturer,
                MGLT = starshipDto.MGLT,
                StarshipClass = starshipDto.StarshipClass,
                HyperdriveRating = starshipDto.HyperdriveRating,
                Url = starshipDto.Url
            };
        }

        public List<Starship> ToEntityList(List<StarshipDto>? starshipsDto)
        {
            if (starshipsDto == null || !starshipsDto.Any())
                return new List<Starship>();

            return starshipsDto.Select(s => ToEntity(s)).Where(s => s != null).ToList()!;
        }
    }
}