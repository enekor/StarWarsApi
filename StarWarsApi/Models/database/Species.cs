using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("species")]
    public class Species:BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("url")]
        public string Url { get; set; }

    }
}
