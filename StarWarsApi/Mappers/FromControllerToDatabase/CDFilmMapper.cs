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
                Characters = string.Join(",",filmDto.Characters.Select(c => c.Url.Split("/").Last()).ToList()),
                Planets = string.Join(",", filmDto.Planets.Select(p => p.url.Split("/").Last()).ToList()),
                Starships = string.Join(",", filmDto.Starships.Select(s => s.Url.Split("/").Last()).ToList()),
                Vehicles = string.Join(",", filmDto.Vehicles.Select(v => v.Url.Split("/").Last()).ToList()),
                Species = string.Join(",", filmDto.Species.Select(s => s.Url.Split("/").Last()).ToList()),
                Description = filmDto.Description,
                Created = filmDto.Created,
                Edited = filmDto.Edited,
                Url = filmDto.Url

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