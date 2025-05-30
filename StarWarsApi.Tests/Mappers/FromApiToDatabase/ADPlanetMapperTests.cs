using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADPlanetMapperTests
    {
        private ADPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADPlanetMapper.Instance;
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            PlanetApi? apiModel = null;

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsValid_ReturnsCorrectDatabaseModel()
        {
            // Arrange
            var apiModel = new PlanetApi
            {
                Uid = "1",
                Name = "Tatooine",
                url = "https://swapi.dev/api/planets/1/"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Uid, Is.EqualTo(apiModel.Uid));
            Assert.That(result.Name, Is.EqualTo(apiModel.Name));
            Assert.That(result.url, Is.EqualTo(apiModel.url));
        }

        [Test]
        public void MapToDatabaseList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<PlanetApi>? apiModels = null;

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<PlanetApi>();

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WhenListHasItems_ReturnsCorrectDatabaseModels()
        {
            // Arrange
            var apiModels = new List<PlanetApi>
            {
                new PlanetApi
                {
                    Uid = "1",
                    Name = "Tatooine",
                    url = "https://swapi.dev/api/planets/1/"
                },
                new PlanetApi
                {
                    Uid = "2",
                    Name = "Alderaan",
                    url = "https://swapi.dev/api/planets/2/"
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Uid, Is.EqualTo("1"));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[1].Uid, Is.EqualTo("2"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
        }
    }
}
