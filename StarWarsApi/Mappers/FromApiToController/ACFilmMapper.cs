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
        public FilmsDto MapToController(FilmsApi acFilm)
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
                Starships = acFilm.properties.starships != null ? string.Join(",", acFilm.properties.starships.Select(v => v.Split("/").Last())) : string.Empty,
                Characters = acFilm.properties.characters != null ? string.Join(",", acFilm.properties.characters.Select(v => v.Split("/").Last())) : string.Empty,
                Vehicles = acFilm.properties.vehicles != null ? string.Join(",", acFilm.properties.vehicles.Select(v => v.Split("/").Last())) : string.Empty,
                Planets = acFilm.properties.planets != null ? string.Join(",", acFilm.properties.planets.Select(v => v.Split("/").Last())) : string.Empty,
                Species = acFilm.properties.species != null ? string.Join(",", acFilm.properties.species.Select(v => v.Split("/").Last())) : string.Empty
            };
        }

        public List<FilmsDto> MapToControllerList(List<FilmsApi> acFilms)
        {
            if (acFilms == null || !acFilms.Any())
            {
                return new List<FilmsDto>();
            }
            return acFilms.Select(ac => MapToController(ac)).ToList();
        } }
}
