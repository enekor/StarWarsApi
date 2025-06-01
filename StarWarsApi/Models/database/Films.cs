using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("films")]
    public class Films : BaseModel
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("edited")]
        public DateTime Edited { get; set; }

        [Column("starships")]
        public string? Starships { get; set; }

        [Column("vehicles")]
        public string? Vehicles { get; set; }

        [Column("planets")]
        public string? Planets { get; set; }

        [Column("producer")]
        public string? Producer { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("episode_id")]
        public int EpisodeId { get; set; }

        [Column("director")]
        public string? Director { get; set; }

        [Column("release_date")]
        public string? ReleaseDate { get; set; }

        [Column("opening_crawl")]
        public string? OpeningCrawl { get; set; }

        [Column("characters")]
        public string? Characters { get; set; }

        [Column("species")]
        public string? Species { get; set; }

        [Column("url")]
        public string? Url { get; set; }
    }
}
