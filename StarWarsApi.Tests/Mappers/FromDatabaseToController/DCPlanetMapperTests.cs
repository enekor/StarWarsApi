using NUnit.Framework;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

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
        public void ToDto_WhenDatabaseModelIsNull_ReturnsNull()
        {
            // Arrange
            Planet? dbModel = null;

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WhenDatabaseModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var dbModel = new Planet
            {
                Name = "Tatooine",
                url = "https://swapi.dev/api/planets/1/"
            };

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dbModel.Name));
            Assert.That(result.url, Is.EqualTo(dbModel.url));
        }

        [Test]
        public void ToDtoList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<Planet>? dbModels = null;

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dbModels = new List<Planet>();

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var dbModels = new List<Planet>
            {
                new Planet
                {
                    Name = "Tatooine",
                    url = "https://swapi.dev/api/planets/1/"
                },
                new Planet
                {
                    Name = "Alderaan",
                    url = "https://swapi.dev/api/planets/2/"
                }
            };

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
        }
    }
}
