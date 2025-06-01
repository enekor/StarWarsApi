using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADCharacterMapperTests
    {
        private ADCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADCharacterMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADCharacterMapper.Instance;
            var instance2 = ADCharacterMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            CharacterApi? nullCharacter = null;

            // Act
            var result = _mapper.MapToDatabase(nullCharacter);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var characterApi = new CharacterApi
            {
                result = new CharacterResult
                {
                    uid = "1",
                    description = "Test Description",
                    properties = new CharacterProperties
                    {
                        created = created,
                        edited = edited,
                        name = "Luke Skywalker",
                        url = "https://swapi.dev/api/people/1",
                        birth_year = "19BBY",
                        eye_color = "blue",
                        gender = "male",
                        hair_color = "blond",
                        height = "172",
                        mass = "77",
                        homeworld = "https://swapi.dev/api/planets/1",
                        skin_color = "fair"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(characterApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("1"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Name, Is.EqualTo("Luke Skywalker"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/people/1"));
                Assert.That(result.BirthYear, Is.EqualTo("19BBY"));
                Assert.That(result.EyeColor, Is.EqualTo("blue"));
                Assert.That(result.Gender, Is.EqualTo("male"));
                Assert.That(result.HairColor, Is.EqualTo("blond"));
                Assert.That(result.Height, Is.EqualTo("172"));
                Assert.That(result.Mass, Is.EqualTo("77"));
                Assert.That(result.Homeworld, Is.EqualTo("https://swapi.dev/api/planets/1"));
                Assert.That(result.SkinColor, Is.EqualTo("fair"));
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<CharacterApi>? nullList = null;

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
            var emptyList = new List<CharacterApi>();

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
            var characters = new List<CharacterApi>
            {
                new CharacterApi
                {
                    result = new CharacterResult
                    {
                        uid = "1",
                        description = "Test Description 1",
                        properties = new CharacterProperties
                        {
                            name = "Luke Skywalker",
                            gender = "male"
                        }
                    }
                },
                new CharacterApi
                {
                    result = new CharacterResult
                    {
                        uid = "2",
                        description = "Test Description 2",
                        properties = new CharacterProperties
                        {
                            name = "Leia Organa",
                            gender = "female"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
                Assert.That(result[0].Gender, Is.EqualTo("male"));
                Assert.That(result[0].Uid, Is.EqualTo("1"));
                Assert.That(result[1].Name, Is.EqualTo("Leia Organa"));
                Assert.That(result[1].Gender, Is.EqualTo("female"));
                Assert.That(result[1].Uid, Is.EqualTo("2"));
            });
        }
    }
}