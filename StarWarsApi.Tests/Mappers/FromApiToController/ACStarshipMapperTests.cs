using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACStarshipMapperTests
    {
        private ACStarshipMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACStarshipMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACStarshipMapper.Instance;
            var instance2 = ACStarshipMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullStarshipApi_ReturnsNull()
        {
            // Arrange
            StarshipApi? nullStarship = null;

            // Act
            var result = _mapper.MapToController(nullStarship);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithNullResult_ReturnsNull()
        {
            // Arrange
            var starshipApi = new StarshipApi
            {
                result = null
            };

            // Act
            var result = _mapper.MapToController(starshipApi);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidStarshipApi_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var starshipApi = new StarshipApi
            {
                result = new StarshipResult
                {
                    description = "Test Description",
                    properties = new StarshipProperties
                    {
                        created = created,
                        edited = edited,
                        consumables = "2 months",
                        name = "Millennium Falcon",
                        cargo_capacity = "100000",
                        passengers = "6",
                        max_atmosphering_speed = "1050",
                        crew = "4",
                        length = "34.37",
                        model = "YT-1300 light freighter",
                        cost_in_credits = "100000",
                        manufacturer = "Corellian Engineering Corporation",
                        MGLT = "75",
                        starship_class = "Light freighter",
                        hyperdrive_rating = "0.5",
                        url = "https://swapi.dev/api/starships/10"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(starshipApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Consumables, Is.EqualTo("2 months"));
                Assert.That(result.Name, Is.EqualTo("Millennium Falcon"));
                Assert.That(result.CargoCapacity, Is.EqualTo("100000"));
                Assert.That(result.Passengers, Is.EqualTo("6"));
                Assert.That(result.MaxAtmospheringSpeed, Is.EqualTo("1050"));
                Assert.That(result.Crew, Is.EqualTo("4"));
                Assert.That(result.Length, Is.EqualTo("34.37"));
                Assert.That(result.Model, Is.EqualTo("YT-1300 light freighter"));
                Assert.That(result.CostInCredits, Is.EqualTo("100000"));
                Assert.That(result.Manufacturer, Is.EqualTo("Corellian Engineering Corporation"));
                Assert.That(result.MGLT, Is.EqualTo("75"));
                Assert.That(result.StarshipClass, Is.EqualTo("Light freighter"));
                Assert.That(result.HyperdriveRating, Is.EqualTo("0.5"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/starships/10"));
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<StarshipApi>? nullList = null;

            // Act
            var result = _mapper.MapToControllerList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<StarshipApi>();

            // Act
            var result = _mapper.MapToControllerList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var starships = new List<StarshipApi>
            {
                new StarshipApi
                {
                    result = new StarshipResult
                    {
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
            var result = _mapper.MapToControllerList(starships);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Millennium Falcon"));
                Assert.That(result[0].Model, Is.EqualTo("YT-1300"));
                Assert.That(result[1].Name, Is.EqualTo("Star Destroyer"));
                Assert.That(result[1].Model, Is.EqualTo("Imperial I-class"));
            });
        }

        [Test]
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var starships = new List<StarshipApi>
            {
                new StarshipApi
                {
                    result = new StarshipResult
                    {
                        description = "Test Description",
                        properties = new StarshipProperties
                        {
                            name = "Millennium Falcon",
                            model = "YT-1300"
                        }
                    }
                },
                null,
                new StarshipApi { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(starships);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Millennium Falcon"));
        }
    }
}