using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromDatabaseToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCPlanetMapperTests
    {
        private DCPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCPlanetMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = DCPlanetMapper.Instance;
            var instance2 = DCPlanetMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToDto_WithNullInput_ReturnsNull()
        {
            // Arrange
            Planet? nullPlanet = null;

            // Act
            var result = _mapper.ToDto(nullPlanet);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var planet = new Planet
            {
                Uid = "1",
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Climate = "arid",
                SurfaceWater = "1",
                Name = "Tatooine",
                Diameter = "10465",
                RotationPeriod = "23",
                Terrain = "desert",
                Gravity = "1 standard",
                OrbitalPeriod = "304",
                Population = "200000",
                Url = "https://swapi.dev/api/planets/1"
            };

            // Act
            var result = _mapper.ToDto(planet);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Climate, Is.EqualTo("arid"));
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
        public void ToDtoList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<Planet>? nullList = null;

            // Act
            var result = _mapper.ToDtoList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Planet>();

            // Act
            var result = _mapper.ToDtoList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var planets = new List<Planet>
            {
                new Planet
                {
                    Uid = "1",
                    Description = "Test Description 1",
                    Name = "Tatooine",
                    Climate = "arid",
                    Terrain = "desert"
                },
                new Planet
                {
                    Uid = "2",
                    Description = "Test Description 2",
                    Name = "Alderaan",
                    Climate = "temperate",
                    Terrain = "grasslands, mountains"
                }
            };

            // Act
            var result = _mapper.ToDtoList(planets);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
                Assert.That(result[0].Climate, Is.EqualTo("arid"));
                Assert.That(result[0].Terrain, Is.EqualTo("desert"));
                Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
                Assert.That(result[1].Climate, Is.EqualTo("temperate"));
                Assert.That(result[1].Terrain, Is.EqualTo("grasslands, mountains"));
            });
        }

        [Test]
        public void ToDtoList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var planets = new List<Planet>
            {
                null,
                new Planet
                {
                    Uid = "1",
                    Description = "Test Description",
                    Name = "Tatooine",
                    Climate = "arid"
                }
            };

            // Act
            var result = _mapper.ToDtoList(planets);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
        }
    }
}