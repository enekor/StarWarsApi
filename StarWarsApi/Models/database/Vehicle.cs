using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("vehicles")]
    public class Vehicle:BaseModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("url")]
        public string Url { get; set; }
    }
}
