using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACSpecieMapperTests
    {
        private ACSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACSpecieMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACSpecieMapper.Instance;
            var instance2 = ACSpecieMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullSpecieApi_ReturnsNull()
        {
            // Arrange
            SpecieApi? nullSpecie = null;

            // Act
            var result = _mapper.MapToController(nullSpecie);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithNullResult_ReturnsNull()
        {
            // Arrange
            var specieApi = new SpecieApi
            {
                result = null
            };

            // Act
            var result = _mapper.MapToController(specieApi);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidSpecieApi_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);
            var people = new List<string> { "https://swapi.dev/api/people/1", "https://swapi.dev/api/people/2" };

            var specieApi = new SpecieApi
            {
                result = new SpecieResult
                {
                    description = "Test Description",
                    properties = new SpecieProperties
                    {
                        created = created,
                        edited = edited,
                        classification = "mammal",
                        name = "Human",
                        designation = "sentient",
                        eye_colors = "brown, blue, green",
                        people = people,
                        skin_colors = "pale, fair, light, dark",
                        language = "Galactic Basic",
                        hair_colors = "black, brown, blonde, red",
                        homeworld = "https://swapi.dev/api/planets/9",
                        average_lifespan = "120",
                        average_height = "180",
                        url = "https://swapi.dev/api/species/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(specieApi);

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
                Assert.That(result.EyeColors, Is.EqualTo("brown, blue, green"));
                Assert.That(result.People, Is.EqualTo(people));
                Assert.That(result.SkinColors, Is.EqualTo("pale, fair, light, dark"));
                Assert.That(result.Language, Is.EqualTo("Galactic Basic"));
                Assert.That(result.HairColors, Is.EqualTo("black, brown, blonde, red"));
                Assert.That(result.Homeworld, Is.EqualTo("https://swapi.dev/api/planets/9"));
                Assert.That(result.AverageLifespan, Is.EqualTo("120"));
                Assert.That(result.AverageHeight, Is.EqualTo("180"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/species/1"));
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<SpecieApi>? nullList = null;

            // Act
            var result = _mapper.MapToControllerList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<SpecieApi>();

            // Act
            var result = _mapper.MapToControllerList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var species = new List<SpecieApi>
            {
                new SpecieApi
                {
                    result = new SpecieResult
                    {
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
                        description = "Test Description 2",
                        properties = new SpecieProperties
                        {
                            name = "Wookiee",
                            classification = "mammal"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToControllerList(species);

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
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var species = new List<SpecieApi>
            {
                new SpecieApi
                {
                    result = new SpecieResult
                    {
                        description = "Test Description",
                        properties = new SpecieProperties
                        {
                            name = "Human",
                            classification = "mammal"
                        }
                    }
                },
                null,
                new SpecieApi { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(species);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
        }
    }
}