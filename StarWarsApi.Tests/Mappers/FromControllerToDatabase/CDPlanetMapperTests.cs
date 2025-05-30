using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDPlanetMapperTests
    {
        private CDPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDPlanetMapper.Instance;
        }

        [Test]
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Arrange
            PlanetDto? dto = null;

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new PlanetDto
            {
                Name = "Tatooine",
                url = "https://swapi.dev/api/planets/1/"
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.url, Is.EqualTo(dto.url));
        }

        [Test]
        public void ToEntityList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<PlanetDto>? dtos = null;

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<PlanetDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var dtos = new List<PlanetDto>
            {
                new PlanetDto
                {
                    Name = "Tatooine",
                    url = "https://swapi.dev/api/planets/1/"
                },
                new PlanetDto
                {
                    Name = "Alderaan",
                    url = "https://swapi.dev/api/planets/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[0].url, Is.EqualTo("https://swapi.dev/api/planets/1/"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
            Assert.That(result[1].url, Is.EqualTo("https://swapi.dev/api/planets/2/"));
        }
    }
}
