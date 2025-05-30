using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

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
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Arrange
            StarshipDto? dto = null;

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new StarshipDto
            {
                Name = "X-wing",
                Url = "https://swapi.dev/api/starships/1/"
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Url, Is.EqualTo(dto.Url));
        }

        [Test]
        public void ToEntityList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<StarshipDto>? dtos = null;

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<StarshipDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var dtos = new List<StarshipDto>
            {
                new StarshipDto
                {
                    Name = "X-wing",
                    Url = "https://swapi.dev/api/starships/1/"
                },
                new StarshipDto
                {
                    Name = "Star Destroyer",
                    Url = "https://swapi.dev/api/starships/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("X-wing"));
            Assert.That(result[0].Url, Is.EqualTo("https://swapi.dev/api/starships/1/"));
            Assert.That(result[1].Name, Is.EqualTo("Star Destroyer"));
            Assert.That(result[1].Url, Is.EqualTo("https://swapi.dev/api/starships/2/"));
        }
    }
}
