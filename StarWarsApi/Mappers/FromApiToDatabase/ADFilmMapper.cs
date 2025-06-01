using StarWarsApi.Models.api;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromApiToDatabase
{
    public class ADFilmMapper
    {
        private static ADFilmMapper instance;
        private ADFilmMapper() { }

        public static ADFilmMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADFilmMapper();
                }
                return instance;
            }
        }
        public Films MapToDatabase(FilmApi acFilm)
        {
            if (acFilm == null)
            {
                return null;
            }
            return new Films
            {
                Uid = acFilm.result.uid,
                Title = acFilm.result.properties.title,
                Description = acFilm.result.description,
                Created = acFilm.result.properties.created,
                Edited = acFilm.result.properties.edited,
                Starships = acFilm.result.properties.starships != null ? string.Join(",", acFilm.result.properties.starships.Select(v => v.Split("/").Last())) : string.Empty,
                Vehicles = acFilm.result.properties.vehicles != null ? string.Join(",", acFilm.result.properties.vehicles.Select(v => v.Split("/").Last())) : string.Empty,
                Characters = acFilm.result.properties.characters != null ? string.Join(",", acFilm.result.properties.characters.Select(v => v.Split("/").Last())) : string.Empty,
                Planets = acFilm.result.properties.planets != null ? string.Join(",", acFilm.result.properties.planets.Select(v => v.Split("/").Last())) : string.Empty,
                Species = acFilm.result.properties.species != null ? string.Join(",", acFilm.result.properties.species.Select(v => v.Split("/").Last())) : string.Empty,
                Url = acFilm.result.properties.url
            };
        }

        public List<Films> MapToDatabaseList(List<FilmApi> acFilms)
        {
            if (acFilms == null || !acFilms.Any())
            {
                return new List<Films>();
            }
            return acFilms.Select(ac => MapToDatabase(ac)).ToList();
        }
    }
}
