using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromDatabaseToController
{
    public class DCFilmMapper
    {
        private static DCFilmMapper? instance;
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
        
        public FilmsDto? ToDto(Films? film, List<StarshipDto>? starships = null, List<CharacterDto>? characters = null, 
            List<VehicleDto>? vehicles = null, List<PlanetDto>? planets = null, List<SpeciesDto>? species = null)
        {
            if (film == null)
                return null;

            return new FilmsDto
            {
                Description = film.Description,
                Created = film.Created,
                Edited = film.Edited,
                Producer = film.Producer,
                Title = film.Title,
                EpisodeId = film.EpisodeId,
                Director = film.Director,
                ReleaseDate = film.ReleaseDate,
                OpeningCrawl = film.OpeningCrawl,
                Url = film.Url,
                Starships = starships ?? new List<StarshipDto>(),
                Characters = characters ?? new List<CharacterDto>(),
                Vehicles = vehicles ?? new List<VehicleDto>(),
                Planets = planets ?? new List<PlanetDto>(),
                Species = species ?? new List<SpeciesDto>()
            };
        }

        public List<FilmsDto> ToDtoList(List<Films>? films)
        {
            if (films == null || !films.Any())
                return new List<FilmsDto>();

            return films.Select(f => ToDto(f)).Where(f => f != null).ToList()!;
        }
    }
}