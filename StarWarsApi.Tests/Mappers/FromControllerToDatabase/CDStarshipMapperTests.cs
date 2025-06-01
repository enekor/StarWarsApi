using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromControllerToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDStarshipMapperTests
    {
        private CDStarshipMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDStarshipMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CDStarshipMapper.Instance;
            var instance2 = CDStarshipMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToEntity_WithNullInput_ReturnsNull()
        {
            // Arrange
            StarshipDto? nullStarship = null;

            // Act
            var result = _mapper.ToEntity(nullStarship);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var starshipDto = new StarshipDto
            {
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Consumables = "2 months",
                Name = "Millennium Falcon",
                CargoCapacity = "100000",
                Passengers = "6",
                MaxAtmospheringSpeed = "1050",
                Crew = "4",
                Length = "34.37",
                Model = "YT-1300 light freighter",
                CostInCredits = "100000",
                Manufacturer = "Corellian Engineering Corporation",
                MGLT = "75",
                StarshipClass = "Light freighter",
                HyperdriveRating = "0.5",
                Url = "https://swapi.dev/api/starships/10"
            };

            // Act
            var result = _mapper.ToEntity(starshipDto);

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
        public void ToEntityList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<StarshipDto>? nullList = null;

            // Act
            var result = _mapper.ToEntityList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<StarshipDto>();

            // Act
            var result = _mapper.ToEntityList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var starships = new List<StarshipDto>
            {
                new StarshipDto
                {
                    Description = "Test Description 1",
                    Name = "Millennium Falcon",
                    Model = "YT-1300",
                    StarshipClass = "Light freighter"
                },
                new StarshipDto
                {
                    Description = "Test Description 2",
                    Name = "Star Destroyer",
                    Model = "Imperial I-class",
                    StarshipClass = "Star Destroyer"
                }
            };

            // Act
            var result = _mapper.ToEntityList(starships);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Millennium Falcon"));
                Assert.That(result[0].Model, Is.EqualTo("YT-1300"));
                Assert.That(result[0].StarshipClass, Is.EqualTo("Light freighter"));
                Assert.That(result[1].Name, Is.EqualTo("Star Destroyer"));
                Assert.That(result[1].Model, Is.EqualTo("Imperial I-class"));
                Assert.That(result[1].StarshipClass, Is.EqualTo("Star Destroyer"));
            });
        }

        [Test]
        public void ToEntityList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var starships = new List<StarshipDto>
            {
                new StarshipDto
                {
                    Description = "Test Description",
                    Name = "Millennium Falcon",
                    Model = "YT-1300"
                },
                null
            };

            // Act
            var result = _mapper.ToEntityList(starships);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Millennium Falcon"));
        }
    }
}