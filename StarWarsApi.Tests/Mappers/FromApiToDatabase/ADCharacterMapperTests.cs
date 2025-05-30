using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADCharacterMapperTests
    {
        private ADCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADCharacterMapper.Instance;
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            CharacterApi? apiModel = null;

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsValid_ReturnsCorrectDatabaseModel()
        {
            // Arrange
            var apiModel = new CharacterApi
            {
                Uid = "1",
                Name = "Luke Skywalker",
                Url = "https://swapi.dev/api/people/1/"
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
            List<CharacterApi>? apiModels = null;

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<CharacterApi>();

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListHasItems_ReturnsCorrectDatabaseModels()
        {
            // Arrange
            var apiModels = new List<CharacterApi>
            {
                new CharacterApi 
                { 
                    Uid = "1",
                    Name = "Luke Skywalker",
                    Url = "https://swapi.dev/api/people/1/"
                },
                new CharacterApi
                {
                    Uid = "2",
                    Name = "C-3PO",
                    Url = "https://swapi.dev/api/people/2/"
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Uid, Is.EqualTo("1"));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[1].Uid, Is.EqualTo("2"));
            Assert.That(result[1].Name, Is.EqualTo("C-3PO"));
        }
    }
}
