using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("species")]
    public class Species : BaseModel
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("edited")]
        public DateTime Edited { get; set; }

        [Column("classification")]
        public string? Classification { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }



        [Column("language")]
        public string? Language { get; set; }


        [Column("homeworld")]
        public string? Homeworld { get; set; }

        [Column("average_lifespan")]
        public string? AverageLifespan { get; set; }

        [Column("average_height")]
        public string? AverageHeight { get; set; }

        [Column("url")]
        public string? Url { get; set; }
    }
}
