using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADStarshipMapperTests
    {
        private ADStarshipMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADStarshipMapper.Instance;
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            StarshipApi? apiModel = null;

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsValid_ReturnsCorrectDatabaseModel()
        {
            // Arrange
            var apiModel = new StarshipApi
            {
                Uid = "1",
                Name = "X-wing",
                Url = "https://swapi.dev/api/starships/1/"
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
            List<StarshipApi>? apiModels = null;

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<StarshipApi>();

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListHasItems_ReturnsCorrectDatabaseModels()
        {
            // Arrange
            var apiModels = new List<StarshipApi>
            {
                new StarshipApi
                {
                    Uid = "1",
                    Name = "X-wing",
                    Url = "https://swapi.dev/api/starships/1/"
                },
                new StarshipApi
                {
                    Uid = "2",
                    Name = "Star Destroyer",
                    Url = "https://swapi.dev/api/starships/2/"
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Uid, Is.EqualTo("1"));
            Assert.That(result[0].Name, Is.EqualTo("X-wing"));
            Assert.That(result[1].Uid, Is.EqualTo("2"));
            Assert.That(result[1].Name, Is.EqualTo("Star Destroyer"));
        }
    }
}
