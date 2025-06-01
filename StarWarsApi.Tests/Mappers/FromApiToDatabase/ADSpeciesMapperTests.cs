using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADSpeciesMapperTests
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
            // Act
            var result = _mapper.MapToDatabase(null);

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
                Classification = "mammal",
                Designation = "sentient",
                Language = "Galactic Basic",
                AverageHeight = "180",
                AverageLifespan = "120",
                Url = "https://swapi.dev/api/species/1/"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Uid, Is.EqualTo("1"));
            Assert.That(result.Name, Is.EqualTo("Human"));
            Assert.That(result.Classification, Is.EqualTo("mammal"));
            Assert.That(result.Designation, Is.EqualTo("sentient"));
            Assert.That(result.Language, Is.EqualTo("Galactic Basic"));
            Assert.That(result.AverageHeight, Is.EqualTo("180"));
            Assert.That(result.AverageLifespan, Is.EqualTo("120"));
            Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/species/1/"));
        }

        [Test]
        public void MapToDatabaseList_WhenListIsNull_ReturnsEmptyList()
        {
            // Act
            var result = _mapper.MapToDatabaseList(null);

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
                    Classification = "mammal",
                    Url = "https://swapi.dev/api/species/1/"
                },
                new SpeciesApi
                {
                    Uid = "2",
                    Name = "Wookiee",
                    Classification = "mammal",
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
