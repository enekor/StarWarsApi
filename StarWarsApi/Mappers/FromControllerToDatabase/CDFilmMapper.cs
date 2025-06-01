using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;

namespace StarWarsApi.Mappers.FromControllerToDatabase
{
    public class CDFilmMapper
    {
        private static CDFilmMapper? instance;
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
        
        public Films? ToEntity(FilmsDto? filmDto)
        {
            if (filmDto == null)
                return null;

            return new Films
            {
                Description = filmDto.Description,
                Created = filmDto.Created,
                Edited = filmDto.Edited,
                Producer = filmDto.Producer,
                Title = filmDto.Title,
                EpisodeId = filmDto.EpisodeId,
                Director = filmDto.Director,
                ReleaseDate = filmDto.ReleaseDate,
                OpeningCrawl = filmDto.OpeningCrawl,
                Url = filmDto.Url,
                // Convertimos las listas de DTOs a strings con IDs
                Characters = filmDto.Characters?.Any() == true ? 
                    string.Join(",", filmDto.Characters.Where(c => c?.Url != null).Select(c => c!.Url!.Split("/").Last())) : null,
                Planets = filmDto.Planets?.Any() == true ? 
                    string.Join(",", filmDto.Planets.Where(p => p?.Url != null).Select(p => p!.Url!.Split("/").Last())) : null,
                Starships = filmDto.Starships?.Any() == true ? 
                    string.Join(",", filmDto.Starships.Where(s => s?.Url != null).Select(s => s!.Url!.Split("/").Last())) : null,
                Vehicles = filmDto.Vehicles?.Any() == true ? 
                    string.Join(",", filmDto.Vehicles.Where(v => v?.Url != null).Select(v => v!.Url!.Split("/").Last())) : null,
                Species = filmDto.Species?.Any() == true ? 
                    string.Join(",", filmDto.Species.Where(s => s?.Url != null).Select(s => s!.Url!.Split("/").Last())) : null
            };
        }

        public List<Films> ToEntityList(List<FilmsDto>? filmsDto)
        {
            if (filmsDto == null || !filmsDto.Any())
                return new List<Films>();

            return filmsDto.Select(f => ToEntity(f)).Where(f => f != null).ToList()!;
        }
    }
}