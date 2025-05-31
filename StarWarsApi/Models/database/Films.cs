using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("films")]
    public class Films : BaseModel
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("edited")]
        public DateTime Edited { get; set; }
        [Column("starships")]
        public string Starships { get; set; }
        [Column("vehicles")]
        public string Vehicles { get; set; }
        [Column("characters")]
        public string Characters { get; set; }
        [Column("planets")]
        public string Planets { get; set; }
        [Column("species")]
        public string Species { get; set; }
        [Column("url")]
        public string Url { get; set; }


    }
}
