using NUnit.Framework;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCSpecieMapperTests
    {
        private DCSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCSpecieMapper.Instance;
        }

        [Test]
        public void ToDto_WhenDatabaseModelIsNull_ReturnsNull()
        {
            // Arrange
            Species? dbModel = null;

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WhenDatabaseModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var dbModel = new Species
            {
                Name = "Human",
                Url = "https://swapi.dev/api/species/1/"
            };

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dbModel.Name));
            Assert.That(result.Url, Is.EqualTo(dbModel.Url));
        }

        [Test]
        public void ToDtoList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<Species>? dbModels = null;

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dbModels = new List<Species>();

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var dbModels = new List<Species>
            {
                new Species
                {
                    Name = "Human",
                    Url = "https://swapi.dev/api/species/1/"
                },
                new Species
                {
                    Name = "Wookiee",
                    Url = "https://swapi.dev/api/species/2/"
                }
            };

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
        }
    }
}
