using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsApi.Models.database
{
    [Table("vehicles")]
    public class Vehicle : BaseModel
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

        [Column("vehicle_class")]
        public string? VehicleClass { get; set; }


        [Column("url")]
        public string? Url { get; set; }
    }
}
