using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("planets")]
    public class Planet:BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("url")]
        public string url { get; set; }
    }
}
