using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADSpecieMapperTests
    {
        private ADSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADSpecieMapper.Instance;
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            SpeciesApi? apiModel = null;

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsValid_ReturnsCorrectDatabaseModel()
        {
            // Arrange
            var apiModel = new SpeciesApi
            {
                Uid = "1",
                Name = "Human",
                Url = "https://swapi.dev/api/species/1/"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Uid, Is.EqualTo(apiModel.Uid));
            Assert.That(result.Name, Is.EqualTo(apiModel.Name));
            Assert.That(result.Url, Is.EqualTo(apiModel.Url));
        }

        [Test]
        public void MapToDatabaseList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<SpeciesApi>? apiModels = null;

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<SpeciesApi>();

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListHasItems_ReturnsCorrectDatabaseModels()
        {
            // Arrange
            var apiModels = new List<SpeciesApi>
            {
                new SpeciesApi
                {
                    Uid = "1",
                    Name = "Human",
                    Url = "https://swapi.dev/api/species/1/"
                },
                new SpeciesApi
                {
                    Uid = "2",
                    Name = "Wookiee",
                    Url = "https://swapi.dev/api/species/2/"
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Uid, Is.EqualTo("1"));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[1].Uid, Is.EqualTo("2"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
        }
    }
}
