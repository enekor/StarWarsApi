using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    gender = table.Column<string>(type: "TEXT", nullable: true),
                    skin_color = table.Column<string>(type: "TEXT", nullable: true),
                    hair_color = table.Column<string>(type: "TEXT", nullable: true),
                    height = table.Column<string>(type: "TEXT", nullable: true),
                    eye_color = table.Column<string>(type: "TEXT", nullable: true),
                    mass = table.Column<string>(type: "TEXT", nullable: true),
                    homeworld = table.Column<string>(type: "TEXT", nullable: true),
                    birth_year = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "films",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    starships = table.Column<string>(type: "TEXT", nullable: true),
                    vehicles = table.Column<string>(type: "TEXT", nullable: true),
                    planets = table.Column<string>(type: "TEXT", nullable: true),
                    producer = table.Column<string>(type: "TEXT", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    episode_id = table.Column<int>(type: "INTEGER", nullable: false),
                    director = table.Column<string>(type: "TEXT", nullable: true),
                    release_date = table.Column<string>(type: "TEXT", nullable: true),
                    opening_crawl = table.Column<string>(type: "TEXT", nullable: true),
                    characters = table.Column<string>(type: "TEXT", nullable: true),
                    species = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_films", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    climate = table.Column<string>(type: "TEXT", nullable: true),
                    surface_water = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    diameter = table.Column<string>(type: "TEXT", nullable: true),
                    rotation_period = table.Column<string>(type: "TEXT", nullable: true),
                    terrain = table.Column<string>(type: "TEXT", nullable: true),
                    gravity = table.Column<string>(type: "TEXT", nullable: true),
                    orbital_period = table.Column<string>(type: "TEXT", nullable: true),
                    population = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planets", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    classification = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    designation = table.Column<string>(type: "TEXT", nullable: true),
                    language = table.Column<string>(type: "TEXT", nullable: true),
                    homeworld = table.Column<string>(type: "TEXT", nullable: true),
                    average_lifespan = table.Column<string>(type: "TEXT", nullable: true),
                    average_height = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_species", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "starships",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    consumables = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    cargo_capacity = table.Column<string>(type: "TEXT", nullable: true),
                    passengers = table.Column<string>(type: "TEXT", nullable: true),
                    max_atmosphering_speed = table.Column<string>(type: "TEXT", nullable: true),
                    crew = table.Column<string>(type: "TEXT", nullable: true),
                    length = table.Column<string>(type: "TEXT", nullable: true),
                    model = table.Column<string>(type: "TEXT", nullable: true),
                    cost_in_credits = table.Column<string>(type: "TEXT", nullable: true),
                    manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    mglt = table.Column<string>(type: "TEXT", nullable: true),
                    starship_class = table.Column<string>(type: "TEXT", nullable: true),
                    hyperdrive_rating = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_starships", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    _id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    edited = table.Column<DateTime>(type: "TEXT", nullable: false),
                    consumables = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    cargo_capacity = table.Column<string>(type: "TEXT", nullable: true),
                    passengers = table.Column<string>(type: "TEXT", nullable: true),
                    max_atmosphering_speed = table.Column<string>(type: "TEXT", nullable: true),
                    crew = table.Column<string>(type: "TEXT", nullable: true),
                    length = table.Column<string>(type: "TEXT", nullable: true),
                    model = table.Column<string>(type: "TEXT", nullable: true),
                    cost_in_credits = table.Column<string>(type: "TEXT", nullable: true),
                    manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    vehicle_class = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x._id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "films");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropTable(
                name: "starships");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
