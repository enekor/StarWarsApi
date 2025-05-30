using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDCharacterMapperTests
    {
        private CDCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDCharacterMapper.Instance;
        }

        [Test]
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Arrange
            CharacterDto? dto = null;

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new CharacterDto
            {
                Name = "Luke Skywalker",
                Url = "https://swapi.dev/api/people/1/"
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
            List<CharacterDto>? dtos = null;

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<CharacterDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var dtos = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Name = "Luke Skywalker",
                    Url = "https://swapi.dev/api/people/1/"
                },
                new CharacterDto
                {
                    Name = "C-3PO",
                    Url = "https://swapi.dev/api/people/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[0].Url, Is.EqualTo("https://swapi.dev/api/people/1/"));
            Assert.That(result[1].Name, Is.EqualTo("C-3PO"));
            Assert.That(result[1].Url, Is.EqualTo("https://swapi.dev/api/people/2/"));
        }
    }
}
