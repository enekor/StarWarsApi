using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.controller
{
    public class FilmsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public List<StarshipDto> Starships { get; set; }
        public List<VehicleDto> Vehicles { get; set; }
        public List<CharacterDto> Characters { get; set; }
        public List<PlanetDto> Planets { get; set; }
        public List<SpeciesDto> Species { get; set; }
        public string Url { get; set; }
    }
}
