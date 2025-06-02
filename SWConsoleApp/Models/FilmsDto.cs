using System.ComponentModel.DataAnnotations.Schema;

namespace SWConsoleApp.Models
{
    public class FilmsDto
    {
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public List<StarshipDto>? Starships { get; set; }
        public List<VehicleDto>? Vehicles { get; set; }
        public List<PlanetDto>? Planets { get; set; }
        public string? Producer { get; set; }
        public string? Title { get; set; }
        public int EpisodeId { get; set; }
        public string? Director { get; set; }
        public string? ReleaseDate { get; set; }
        public string? OpeningCrawl { get; set; }
        public List<CharacterDto>? Characters { get; set; }
        public List<SpeciesDto>? Species { get; set; }
        public string? Url { get; set; }
    }
}
