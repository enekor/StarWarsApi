using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.controller
{
    public class PlanetDto
    {
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string? Climate { get; set; }
        public string? SurfaceWater { get; set; }
        public string? Name { get; set; }
        public string? Diameter { get; set; }
        public string? RotationPeriod { get; set; }
        public string? Terrain { get; set; }
        public string? Gravity { get; set; }
        public string? OrbitalPeriod { get; set; }
        public string? Population { get; set; }
        public string? Url { get; set; }
    }
}
