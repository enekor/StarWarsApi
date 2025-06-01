using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADStarshipMapperTests
    {
        private ADStarshipMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADStarshipMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADStarshipMapper.Instance;
            var instance2 = ADStarshipMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            StarshipApi? nullStarship = null;

            // Act
            var result = _mapper.MapToDatabase(nullStarship);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var starshipApi = new StarshipApi
            {
                result = new StarshipResult
                {
                    uid = "10",
                    description = "Test Description",
                    properties = new StarshipProperties
                    {
                        name = "Millennium Falcon",
                        model = "YT-1300 light freighter",
                        manufacturer = "Corellian Engineering Corporation",
                        cost_in_credits = "100000",
                        length = "34.37",
                        max_atmosphering_speed = "1050",
                        crew = "4",
                        passengers = "6",
                        cargo_capacity = "100000",
                        consumables = "2 months",
                        hyperdrive_rating = "0.5",
                        MGLT = "75",
                        starship_class = "Light freighter",
                        created = created,
                        edited = edited,
                        url = "https://swapi.dev/api/starships/10"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(starshipApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("10"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Name, Is.EqualTo("Millennium Falcon"));
                Assert.That(result.Model, Is.EqualTo("YT-1300 light freighter"));
                Assert.That(result.Manufacturer, Is.EqualTo("Corellian Engineering Corporation"));
                Assert.That(result.CostInCredits, Is.EqualTo("100000"));
                Assert.That(result.Length, Is.EqualTo("34.37"));
                Assert.That(result.MaxAtmospheringSpeed, Is.EqualTo("1050"));
                Assert.That(result.Crew, Is.EqualTo("4"));
                Assert.That(result.Passengers, Is.EqualTo("6"));
                Assert.That(result.CargoCapacity, Is.EqualTo("100000"));
                Assert.That(result.Consumables, Is.EqualTo("2 months"));
                Assert.That(result.HyperdriveRating, Is.EqualTo("0.5"));
                Assert.That(result.MGLT, Is.EqualTo("75"));
                Assert.That(result.StarshipClass, Is.EqualTo("Light freighter"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/starships/10"));
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<StarshipApi>? nullList = null;

            // Act
            var result = _mapper.MapToDatabaseList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<StarshipApi>();

            // Act
            var result = _mapper.MapToDatabaseList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var starships = new List<StarshipApi>
            {
                new StarshipApi
                {
                    result = new StarshipResult
                    {
                        uid = "10",
                        description = "Test Description 1",
                        properties = new StarshipProperties
                        {
                            name = "Millennium Falcon",
                            model = "YT-1300"
                        }
                    }
                },
                new StarshipApi
                {
                    result = new StarshipResult
                    {
                        uid = "3",
                        description = "Test Description 2",
                        properties = new StarshipProperties
                        {
                            name = "Star Destroyer",
                            model = "Imperial I-class"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(starships);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Millennium Falcon"));
                Assert.That(result[0].Model, Is.EqualTo("YT-1300"));
                Assert.That(result[0].Uid, Is.EqualTo("10"));
                Assert.That(result[1].Name, Is.EqualTo("Star Destroyer"));
                Assert.That(result[1].Model, Is.EqualTo("Imperial I-class"));
                Assert.That(result[1].Uid, Is.EqualTo("3"));
            });
        }
    }
}