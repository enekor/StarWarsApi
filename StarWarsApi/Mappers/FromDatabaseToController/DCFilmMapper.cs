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
        
        public  FilmsDto ToDto(Films film, List<StarshipDto> starships, List<CharacterDto> characters, List<VehicleDto> vehicles, List<PlanetDto> planets, List<SpeciesDto> species)
        {
            if (film == null)
                return null;

            return new FilmsDto
            {
                Title = film.Title,
                Characters = characters,
                Planets = planets,
                Species = species,
                Starships = starships,
                Vehicles = vehicles,
                Description = film.Description,
                Created = film.Created,
                Edited = film.Edited,
                Url = film.Url
            };
        }
    }
}