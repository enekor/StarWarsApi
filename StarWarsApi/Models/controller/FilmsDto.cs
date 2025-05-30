using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.controller
{
    public class FilmsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Starships { get; set; }
        public string Vehicles { get; set; }
        public string Characters { get; set; }
        public string Planets { get; set; }
        public string Species { get; set; }
    }
}
