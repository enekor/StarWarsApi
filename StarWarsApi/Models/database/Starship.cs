using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("starships")]
    public class Starship : BaseModel
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("edited")]
        public DateTime Edited { get; set; }

        [Column("consumables")]
        public string? Consumables { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("cargo_capacity")]
        public string? CargoCapacity { get; set; }

        [Column("passengers")]
        public string? Passengers { get; set; }

        [Column("max_atmosphering_speed")]
        public string? MaxAtmospheringSpeed { get; set; }

        [Column("crew")]
        public string? Crew { get; set; }

        [Column("length")]
        public string? Length { get; set; }

        [Column("model")]
        public string? Model { get; set; }

        [Column("cost_in_credits")]
        public string? CostInCredits { get; set; }

        [Column("manufacturer")]
        public string? Manufacturer { get; set; }



        [Column("mglt")]
        public string? MGLT { get; set; }

        [Column("starship_class")]
        public string? StarshipClass { get; set; }

        [Column("hyperdrive_rating")]
        public string? HyperdriveRating { get; set; }



        [Column("url")]
        public string? Url { get; set; }
    }
}
