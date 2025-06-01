using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADPlanetMapperTests
    {
        private ADPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADPlanetMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADPlanetMapper.Instance;
            var instance2 = ADPlanetMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            PLanetApu? nullPlanet = null;

            // Act
            var result = _mapper.MapToDatabase(nullPlanet);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var planetApi = new PLanetApu
            {
                result = new PlanetResult
                {
                    uid = "1",
                    description = "Test Description",
                    properties = new PlanetProperties
                    {
                        name = "Tatooine",
                        climate = "arid",
                        surface_water = "1",
                        diameter = "10465",
                        rotation_period = "23",
                        terrain = "desert",
                        gravity = "1 standard",
                        orbital_period = "304",
                        population = "200000",
                        url = "https://swapi.dev/api/planets/1",
                        created = created,
                        edited = edited
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(planetApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("1"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Name, Is.EqualTo("Tatooine"));
                Assert.That(result.Climate, Is.EqualTo("arid"));
                Assert.That(result.SurfaceWater, Is.EqualTo("1"));
                Assert.That(result.Diameter, Is.EqualTo("10465"));
                Assert.That(result.RotationPeriod, Is.EqualTo("23"));
                Assert.That(result.Terrain, Is.EqualTo("desert"));
                Assert.That(result.Gravity, Is.EqualTo("1 standard"));
                Assert.That(result.OrbitalPeriod, Is.EqualTo("304"));
                Assert.That(result.Population, Is.EqualTo("200000"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/planets/1"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<PLanetApu>? nullList = null;

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
            var emptyList = new List<PLanetApu>();

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
            var planets = new List<PLanetApu>
            {
                new PLanetApu
                {
                    result = new PlanetResult
                    {
                        uid = "1",
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
                        uid = "2",
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
            var result = _mapper.MapToDatabaseList(planets);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
                Assert.That(result[0].Climate, Is.EqualTo("arid"));
                Assert.That(result[0].Uid, Is.EqualTo("1"));
                Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
                Assert.That(result[1].Climate, Is.EqualTo("temperate"));
                Assert.That(result[1].Uid, Is.EqualTo("2"));
            });
        }
    }
}