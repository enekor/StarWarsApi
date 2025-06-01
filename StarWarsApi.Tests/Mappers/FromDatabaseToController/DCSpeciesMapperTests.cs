using NUnit.Framework;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Models.database;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCSpeciesMapperTests
    {
        private DCSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCSpecieMapper.Instance;
        }

        [Test]
        public void ToDto_WhenEntityIsNull_ReturnsNull()
        {
            // Act
            var result = _mapper.ToDto(null);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WhenEntityIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var entity = new Species
            {
                Name = "Human",
                Classification = "mammal",
                Designation = "sentient",
                Language = "Galactic Basic",
                AverageHeight = "180",
                AverageLifespan = "120",
                Url = "https://swapi.dev/api/species/1/"
            };

            // Act
            var result = _mapper.ToDto(entity);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(entity.Name));
            Assert.That(result.Classification, Is.EqualTo(entity.Classification));
            Assert.That(result.Designation, Is.EqualTo(entity.Designation));
            Assert.That(result.Language, Is.EqualTo(entity.Language));
            Assert.That(result.AverageHeight, Is.EqualTo(entity.AverageHeight));
            Assert.That(result.AverageLifespan, Is.EqualTo(entity.AverageLifespan));
            Assert.That(result.Url, Is.EqualTo(entity.Url));
        }

        [Test]
        public void ToDtoList_WhenListIsNull_ReturnsEmptyList()
        {
            // Act
            var result = _mapper.ToDtoList(null);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var entities = new List<Species>();

            // Act
            var result = _mapper.ToDtoList(entities);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var entities = new List<Species>
            {
                new Species
                {
                    Name = "Human",
                    Classification = "mammal",
                    Url = "https://swapi.dev/api/species/1/"
                },
                new Species
                {
                    Name = "Wookiee",
                    Classification = "mammal",
                    Url = "https://swapi.dev/api/species/2/"
                }
            };

            // Act
            var result = _mapper.ToDtoList(entities);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[0].Classification, Is.EqualTo("mammal"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
            Assert.That(result[1].Classification, Is.EqualTo("mammal"));
        }
    }
}
