using System.IO;
using StarWarsApi.Daos;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCFilmMapper
    {
        private static DCFilmMapper instance;
        private DCFilmMapper() { }
        public static DCFilmMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DCFilmMapper();
                }
                return instance;
            }
        }
        
        public  FilmsDto ToDto(Films film)
        {
            if (film == null)
                return null;

            return new FilmsDto
            {
                Title = film.Title,
                Characters = film.Characters,
                Planets = film.Planets,
                Species = film.Species,
                Starships = film.Starships,
                Vehicles = film.Vehicles,
                Description = film.Description,
                Created = film.Created,
                Edited = film.Edited,
            };
        }

        public  List<FilmsDto> ToDtoList(List<Films> films)
        {
            if (films == null || !films.Any())
                return new List<FilmsDto>();

            return films.Select(f => ToDto(f)).ToList();
        }
    }
}