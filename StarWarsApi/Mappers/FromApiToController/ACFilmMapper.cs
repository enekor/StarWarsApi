using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACFilmMapper
    {
        private static ACFilmMapper instance;
        private ACFilmMapper() { }
        public static ACFilmMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ACFilmMapper();
                }
                return instance;
            }
        }
        public FilmsDto MapToController(FilmsApi acFilm, List<StarshipDto> starships, List<CharacterDto> characters, List<VehicleDto> vehicles, List<PlanetDto> planets, List<SpeciesDto> species)
        {
            if (acFilm == null)
            {
                return null;
            }
            return new FilmsDto
            {
                Title = acFilm.properties.title,
                Description = acFilm.description,
                Created = acFilm.properties.created,
                Edited = acFilm.properties.edited,
                Starships = starships,
                Characters = characters,
                Vehicles = vehicles,
                Planets = planets,
                Species = species,
                Url = acFilm.properties.url
            };
        }

    }
}
