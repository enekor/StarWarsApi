using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADSpecieMapper.Instance;
            var instance2 = ADSpecieMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            SpecieApi? nullSpecie = null;

            // Act
            var result = _mapper.MapToDatabase(nullSpecie);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var specieApi = new SpecieApi
            {
                result = new SpecieResult
                {
                    uid = "1",
                    description = "Test Description",
                    properties = new SpecieProperties
                    {
                        average_height = "180",
                        average_lifespan = "120",
                        classification = "mammal",
                        created = created,
                        designation = "sentient",
                        edited = edited,
                        homeworld = "https://swapi.dev/api/planets/1",
                        language = "Galactic Basic",
                        name = "Human"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(specieApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("1"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.AverageHeight, Is.EqualTo("180"));
                Assert.That(result.AverageLifespan, Is.EqualTo("120"));
                Assert.That(result.Classification, Is.EqualTo("mammal"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Designation, Is.EqualTo("sentient"));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Homeworld, Is.EqualTo("https://swapi.dev/api/planets/1"));
                Assert.That(result.Language, Is.EqualTo("Galactic Basic"));
                Assert.That(result.Name, Is.EqualTo("Human"));
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<SpecieApi>? nullList = null;

            // Act
            var result = _mapper.MapToDatabaseList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<SpecieApi>();

            // Act
            var result = _mapper.MapToDatabaseList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var species = new List<SpecieApi>
            {
                new SpecieApi
                {
                    result = new SpecieResult
                    {
                        uid = "1",
                        description = "Test Description 1",
                        properties = new SpecieProperties
                        {
                            name = "Human",
                            classification = "mammal"
                        }
                    }
                },
                new SpecieApi
                {
                    result = new SpecieResult
                    {
                        uid = "2",
                        description = "Test Description 2",
                        properties = new SpecieProperties
                        {
                            name = "Droid",
                            classification = "artificial"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(species);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Human"));
                Assert.That(result[0].Classification, Is.EqualTo("mammal"));
                Assert.That(result[0].Uid, Is.EqualTo("1"));
                Assert.That(result[1].Name, Is.EqualTo("Droid"));
                Assert.That(result[1].Classification, Is.EqualTo("artificial"));
                Assert.That(result[1].Uid, Is.EqualTo("2"));
            });
        }
    }
}