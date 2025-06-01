using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACCharacterMapperTests
    {
        private ACCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACCharacterMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACCharacterMapper.Instance;
            var instance2 = ACCharacterMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullInput_ReturnsNull()
        {
            // Arrange
            CharacterApi? nullCharacter = null;

            // Act
            var result = _mapper.MapToController(nullCharacter);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var characterApi = new CharacterApi
            {
                result = new CharacterResult
                {
                    description = "Test Description",
                    properties = new CharacterProperties
                    {
                        created = created,
                        edited = edited,
                        name = "Luke Skywalker",
                        gender = "male",
                        skin_color = "fair",
                        hair_color = "blond",
                        height = "172",
                        eye_color = "blue",
                        mass = "77",
                        homeworld = "tatooine",
                        birth_year = "19BBY",
                        url = "https://swapi.dev/api/people/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(characterApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Name, Is.EqualTo("Luke Skywalker"));
                Assert.That(result.Gender, Is.EqualTo("male"));
                Assert.That(result.SkinColor, Is.EqualTo("fair"));
                Assert.That(result.HairColor, Is.EqualTo("blond"));
                Assert.That(result.Height, Is.EqualTo("172"));
                Assert.That(result.EyeColor, Is.EqualTo("blue"));
                Assert.That(result.Mass, Is.EqualTo("77"));
                Assert.That(result.Homeworld, Is.EqualTo("tatooine"));
                Assert.That(result.BirthYear, Is.EqualTo("19BBY"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/people/1"));
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<CharacterApi>? nullList = null;

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
            var emptyList = new List<CharacterApi>();

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
            var characters = new List<CharacterApi>
            {
                new CharacterApi
                {
                    result = new CharacterResult
                    {
                        description = "Test Description 1",
                        properties = new CharacterProperties
                        {
                            name = "Luke Skywalker",
                            height = "172"
                        }
                    }
                },
                new CharacterApi
                {
                    result = new CharacterResult
                    {
                        description = "Test Description 2",
                        properties = new CharacterProperties
                        {
                            name = "Darth Vader",
                            height = "202"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToControllerList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
                Assert.That(result[0].Height, Is.EqualTo("172"));
                Assert.That(result[1].Name, Is.EqualTo("Darth Vader"));
                Assert.That(result[1].Height, Is.EqualTo("202"));
            });
        }

        [Test]
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var characters = new List<CharacterApi>
            {
                new CharacterApi
                {
                    result = new CharacterResult
                    {
                        description = "Test Description",
                        properties = new CharacterProperties
                        {
                            name = "Luke Skywalker",
                            height = "172"
                        }
                    }
                },
                null,
                new CharacterApi { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
        }
    }
}