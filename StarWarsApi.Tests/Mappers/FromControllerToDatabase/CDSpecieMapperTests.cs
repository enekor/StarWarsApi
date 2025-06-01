using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromControllerToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDSpecieMapperTests
    {
        private CDSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDSpecieMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CDSpecieMapper.Instance;
            var instance2 = CDSpecieMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToEntity_WithNullInput_ReturnsNull()
        {
            // Arrange
            SpeciesDto? nullSpecie = null;

            // Act
            var result = _mapper.ToEntity(nullSpecie);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var specieDto = new SpeciesDto
            {
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Classification = "mammal",
                Name = "Human",
                Designation = "sentient",
                Language = "Galactic Basic",
                Homeworld = "https://swapi.dev/api/planets/9",
                AverageLifespan = "120",
                AverageHeight = "180",
                Url = "https://swapi.dev/api/species/1",
                // Estos campos no se mapean al modelo de base de datos
                EyeColors = "brown, blue, green",
                People = new List<string> { "1", "2" },
                SkinColors = "fair, light, dark",
                HairColors = "black, brown, blonde"
            };

            // Act
            var result = _mapper.ToEntity(specieDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Classification, Is.EqualTo("mammal"));
                Assert.That(result.Name, Is.EqualTo("Human"));
                Assert.That(result.Designation, Is.EqualTo("sentient"));
                Assert.That(result.Language, Is.EqualTo("Galactic Basic"));
                Assert.That(result.Homeworld, Is.EqualTo("https://swapi.dev/api/planets/9"));
                Assert.That(result.AverageLifespan, Is.EqualTo("120"));
                Assert.That(result.AverageHeight, Is.EqualTo("180"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/species/1"));
            });
        }

        [Test]
        public void ToEntityList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<SpeciesDto>? nullList = null;

            // Act
            var result = _mapper.ToEntityList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<SpeciesDto>();

            // Act
            var result = _mapper.ToEntityList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var species = new List<SpeciesDto>
            {
                new SpeciesDto
                {
                    Description = "Test Description 1",
                    Name = "Human",
                    Classification = "mammal"
                },
                new SpeciesDto
                {
                    Description = "Test Description 2",
                    Name = "Wookiee",
                    Classification = "mammal"
                }
            };

            // Act
            var result = _mapper.ToEntityList(species);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Human"));
                Assert.That(result[0].Classification, Is.EqualTo("mammal"));
                Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
                Assert.That(result[1].Classification, Is.EqualTo("mammal"));
            });
        }

        [Test]
        public void ToEntityList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var species = new List<SpeciesDto>
            {
                new SpeciesDto
                {
                    Description = "Test Description",
                    Name = "Human",
                    Classification = "mammal"
                },
                null
            };

            // Act
            var result = _mapper.ToEntityList(species);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
        }
    }
}