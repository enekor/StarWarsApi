using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Mappers.FromApiToController
{
    public class ACFilmMapper
    {
        private static ACFilmMapper? instance;
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

        public FilmsDto? MapToController(FilmApi? acFilm)
        {
            if (acFilm?.result == null)
            {
                return null;
            }

            return new FilmsDto
            {
                Description = acFilm.result.description,
                Created = acFilm.result.properties.created,
                Edited = acFilm.result.properties.edited,
                Producer = acFilm.result.properties.producer,
                Title = acFilm.result.properties.title,
                EpisodeId = acFilm.result.properties.episode_id,
                Director = acFilm.result.properties.director,
                ReleaseDate = acFilm.result.properties.release_date,
                OpeningCrawl = acFilm.result.properties.opening_crawl,
                Url = acFilm.result.properties.url,
                Starships = new(),
                Characters = new(),
                Vehicles = new(),
                Planets = new(),
                Species = new()
            };
        }
        
        public FilmsDto? MapToController(FilmResult? acFilm)
        {
            if (acFilm == null)
            {
                return null;
            }

            return new FilmsDto
            {
                Description = acFilm.description,
                Created = acFilm.properties.created,
                Edited = acFilm.properties.edited,
                Producer = acFilm.properties.producer,
                Title = acFilm.properties.title,
                EpisodeId = acFilm.properties.episode_id,
                Director = acFilm.properties.director,
                ReleaseDate = acFilm.properties.release_date,
                OpeningCrawl = acFilm.properties.opening_crawl,
                Url = acFilm.properties.url,
                Starships = new(),
                Characters = new(),
                Vehicles = new(),
                Planets = new(),
                Species = new()
            };
        }
    }
}
