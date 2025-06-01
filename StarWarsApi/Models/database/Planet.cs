using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("planets")]
    public class Planet : BaseModel
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("edited")]
        public DateTime Edited { get; set; }

        [Column("climate")]
        public string? Climate { get; set; }

        [Column("surface_water")]
        public string? SurfaceWater { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("diameter")]
        public string? Diameter { get; set; }

        [Column("rotation_period")]
        public string? RotationPeriod { get; set; }

        [Column("terrain")]
        public string? Terrain { get; set; }

        [Column("gravity")]
        public string? Gravity { get; set; }

        [Column("orbital_period")]
        public string? OrbitalPeriod { get; set; }

        [Column("population")]
        public string? Population { get; set; }

        [Column("url")]
        public string? Url { get; set; }
    }
}
