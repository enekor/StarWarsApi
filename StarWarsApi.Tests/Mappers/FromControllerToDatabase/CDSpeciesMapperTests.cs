using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDSpeciesMapperTests
    {
        private CDSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDSpecieMapper.Instance;
        }

        [Test]
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Act
            var result = _mapper.ToEntity(null);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new SpeciesDto
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
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Classification, Is.EqualTo(dto.Classification));
            Assert.That(result.Designation, Is.EqualTo(dto.Designation));
            Assert.That(result.Language, Is.EqualTo(dto.Language));
            Assert.That(result.AverageHeight, Is.EqualTo(dto.AverageHeight));
            Assert.That(result.AverageLifespan, Is.EqualTo(dto.AverageLifespan));
            Assert.That(result.Url, Is.EqualTo(dto.Url));
        }

        [Test]
        public void ToEntityList_WhenListIsNull_ReturnsEmptyList()
        {
            // Act
            var result = _mapper.ToEntityList(null);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<SpeciesDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var dtos = new List<SpeciesDto>
            {
                new SpeciesDto
                {
                    Name = "Human",
                    Classification = "mammal",
                    Url = "https://swapi.dev/api/species/1/"
                },
                new SpeciesDto
                {
                    Name = "Wookiee",
                    Classification = "mammal",
                    Url = "https://swapi.dev/api/species/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[0].Classification, Is.EqualTo("mammal"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
            Assert.That(result[1].Classification, Is.EqualTo("mammal"));
        }
    }
}
