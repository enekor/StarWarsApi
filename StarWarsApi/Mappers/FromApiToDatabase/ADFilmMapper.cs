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
        public Films MapToDatabase(FilmsApi acFilm)
        {
            if (acFilm == null)
            {
                return null;
            }
            return new Films
            {
                Uid = acFilm._id,
                Title = acFilm.properties.title,
                Description = acFilm.description,
                Created = acFilm.properties.created,
                Edited = acFilm.properties.edited,
                Starships = acFilm.properties.starships != null ? string.Join(",", acFilm.properties.starships.Select(v => v.Split("/").Last())) : string.Empty,
                Vehicles = acFilm.properties.vehicles != null ? string.Join(",", acFilm.properties.vehicles.Select(v => v.Split("/").Last())) : string.Empty,
                Characters = acFilm.properties.characters != null ? string.Join(",", acFilm.properties.characters.Select(v => v.Split("/").Last())) : string.Empty,
                Planets = acFilm.properties.planets != null ? string.Join(",", acFilm.properties.planets.Select(v => v.Split("/").Last())) : string.Empty,
                Species = acFilm.properties.species != null ? string.Join(",", acFilm.properties.species.Select(v => v.Split("/").Last())) : string.Empty
            };
        }

        public List<Films> MapToDatabaseList(List<FilmsApi> acFilms)
        {
            if (acFilms == null || !acFilms.Any())
            {
                return new List<Films>();
            }
            return acFilms.Select(ac => MapToDatabase(ac)).ToList();
        }
    }
}
