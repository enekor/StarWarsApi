using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("characters")]
    public class Character : BaseModel
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("edited")]
        public DateTime Edited { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("skin_color")]
        public string? SkinColor { get; set; }

        [Column("hair_color")]
        public string? HairColor { get; set; }

        [Column("height")]
        public string? Height { get; set; }

        [Column("eye_color")]
        public string? EyeColor { get; set; }

        [Column("mass")]
        public string? Mass { get; set; }

        [Column("homeworld")]
        public string? Homeworld { get; set; }

        [Column("birth_year")]
        public string? BirthYear { get; set; }

        [Column("url")]
        public string? Url { get; set; }
    }
}
