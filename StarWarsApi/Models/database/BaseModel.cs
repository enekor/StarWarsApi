using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    public class BaseModel
    {
        [Column("_id")]
        public string Uid { get; set; }
    }
}
