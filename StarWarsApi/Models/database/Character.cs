using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("characters")]
    public class Character:BaseModel
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("url")]
        public string Url { get; set; }

        
    }
}
