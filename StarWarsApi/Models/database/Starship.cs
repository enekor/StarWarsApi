using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("starships")]
    public class Starship:BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("url")]
        public string Url { get; set; }
    }
}
