using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromDatabaseToController;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = DCSpecieMapper.Instance;
            var instance2 = DCSpecieMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToDto_WithNullInput_ReturnsNull()
        {
            // Arrange
            Species? nullSpecies = null;

            // Act
            var result = _mapper.ToDto(nullSpecies);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var species = new Species
            {
                Uid = "1",
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
                Url = "https://swapi.dev/api/species/1"
            };

            // Act
            var result = _mapper.ToDto(species);

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
                
                // Verificar que las propiedades no mapeadas son nulas
                Assert.That(result.EyeColors, Is.Null);
                Assert.That(result.People, Is.Null);
                Assert.That(result.SkinColors, Is.Null);
                Assert.That(result.HairColors, Is.Null);
            });
        }

        [Test]
        public void ToDtoList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<Species>? nullList = null;

            // Act
            var result = _mapper.ToDtoList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Species>();

            // Act
            var result = _mapper.ToDtoList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var speciesList = new List<Species>
            {
                new Species
                {
                    Uid = "1",
                    Description = "Test Description 1",
                    Name = "Human",
                    Classification = "mammal",
                    Language = "Galactic Basic"
                },
                new Species
                {
                    Uid = "2",
                    Description = "Test Description 2",
                    Name = "Wookiee",
                    Classification = "mammal",
                    Language = "Shyriiwook"
                }
            };

            // Act
            var result = _mapper.ToDtoList(speciesList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Human"));
                Assert.That(result[0].Classification, Is.EqualTo("mammal"));
                Assert.That(result[0].Language, Is.EqualTo("Galactic Basic"));
                Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
                Assert.That(result[1].Classification, Is.EqualTo("mammal"));
                Assert.That(result[1].Language, Is.EqualTo("Shyriiwook"));
            });
        }

        [Test]
        public void ToDtoList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var speciesList = new List<Species>
            {
                null,
                new Species
                {
                    Uid = "1",
                    Description = "Test Description",
                    Name = "Human",
                    Classification = "mammal"
                }
            };

            // Act
            var result = _mapper.ToDtoList(speciesList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
        }
    }
}