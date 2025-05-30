using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDFilmMapper
    {
        private static CDFilmMapper instance;
        private CDFilmMapper() { }
        public static CDFilmMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CDFilmMapper();
                }
                return instance;
            }
        }
        
        public Films ToEntity(FilmsDto filmDto)
        {
            if (filmDto == null)
                return null;

            return new Films
            {
                Title = filmDto.Title,
                Characters = filmDto.Characters,
                Planets = filmDto.Planets,
                Starships = filmDto.Starships,
                Vehicles = filmDto.Vehicles,
                Species = filmDto.Species,
                Description = filmDto.Description,
                Created = filmDto.Created,
                Edited = filmDto.Edited

            };
        }

        public List<Films> ToEntityList(List<FilmsDto> filmsDto)
        {
            if (filmsDto == null || !filmsDto.Any())
                return new List<Films>();

            return filmsDto.Select(f => ToEntity(f)).ToList();
        }
    }
}