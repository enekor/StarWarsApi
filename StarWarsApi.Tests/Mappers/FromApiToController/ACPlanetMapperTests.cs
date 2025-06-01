using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACPlanetMapperTests
    {
        private ACPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACPlanetMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACPlanetMapper.Instance;
            var instance2 = ACPlanetMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullPlanetApi_ReturnsNull()
        {
            // Arrange
            PLanetApu? nullPlanet = null;

            // Act
            var result = _mapper.MapToController(nullPlanet);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithNullResult_ReturnsNull()
        {
            // Arrange
            var planetApi = new PLanetApu
            {
                result = null
            };

            // Act
            var result = _mapper.MapToController(planetApi);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidPlanetApi_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var planetApi = new PLanetApu
            {
                result = new PlanetResult
                {
                    description = "Test Description",
                    properties = new PlanetProperties
                    {
                        created = created,
                        edited = edited,
                        climate = "temperate",
                        surface_water = "1",
                        name = "Tatooine",
                        diameter = "10465",
                        rotation_period = "23",
                        terrain = "desert",
                        gravity = "1 standard",
                        orbital_period = "304",
                        population = "200000",
                        url = "https://swapi.dev/api/planets/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(planetApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Climate, Is.EqualTo("temperate"));
                Assert.That(result.SurfaceWater, Is.EqualTo("1"));
                Assert.That(result.Name, Is.EqualTo("Tatooine"));
                Assert.That(result.Diameter, Is.EqualTo("10465"));
                Assert.That(result.RotationPeriod, Is.EqualTo("23"));
                Assert.That(result.Terrain, Is.EqualTo("desert"));
                Assert.That(result.Gravity, Is.EqualTo("1 standard"));
                Assert.That(result.OrbitalPeriod, Is.EqualTo("304"));
                Assert.That(result.Population, Is.EqualTo("200000"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/planets/1"));
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<PLanetApu>? nullList = null;

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
            var emptyList = new List<PLanetApu>();

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
            var planets = new List<PLanetApu>
            {
                new PLanetApu
                {
                    result = new PlanetResult
                    {
                        description = "Test Description 1",
                        properties = new PlanetProperties
                        {
                            name = "Tatooine",
                            climate = "arid"
                        }
                    }
                },
                new PLanetApu
                {
                    result = new PlanetResult
                    {
                        description = "Test Description 2",
                        properties = new PlanetProperties
                        {
                            name = "Alderaan",
                            climate = "temperate"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToControllerList(planets);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
                Assert.That(result[0].Climate, Is.EqualTo("arid"));
                Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
                Assert.That(result[1].Climate, Is.EqualTo("temperate"));
            });
        }

        [Test]
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var planets = new List<PLanetApu>
            {
                new PLanetApu
                {
                    result = new PlanetResult
                    {
                        description = "Test Description",
                        properties = new PlanetProperties
                        {
                            name = "Tatooine",
                            climate = "arid"
                        }
                    }
                },
                null,
                new PLanetApu { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(planets);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
        }
    }
}