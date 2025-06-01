using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromControllerToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDCharacterMapperTests
    {
        private CDCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDCharacterMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CDCharacterMapper.Instance;
            var instance2 = CDCharacterMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToEntity_WithNullInput_ReturnsNull()
        {
            // Arrange
            CharacterDto? nullCharacter = null;

            // Act
            var result = _mapper.ToEntity(nullCharacter);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var characterDto = new CharacterDto
            {
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Name = "Luke Skywalker",
                Gender = "male",
                SkinColor = "fair",
                HairColor = "blond",
                Height = "172",
                EyeColor = "blue",
                Mass = "77",
                Homeworld = "https://swapi.dev/api/planets/1",
                BirthYear = "19BBY",
                Url = "https://swapi.dev/api/people/1"
            };

            // Act
            var result = _mapper.ToEntity(characterDto);

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
                Assert.That(result.Homeworld, Is.EqualTo("https://swapi.dev/api/planets/1"));
                Assert.That(result.BirthYear, Is.EqualTo("19BBY"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/people/1"));
            });
        }

        [Test]
        public void ToEntityList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<CharacterDto>? nullList = null;

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
            var emptyList = new List<CharacterDto>();

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
            var characters = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Description = "Test Description 1",
                    Name = "Luke Skywalker",
                    Gender = "male"
                },
                new CharacterDto
                {
                    Description = "Test Description 2",
                    Name = "Leia Organa",
                    Gender = "female"
                }
            };

            // Act
            var result = _mapper.ToEntityList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
                Assert.That(result[0].Gender, Is.EqualTo("male"));
                Assert.That(result[1].Name, Is.EqualTo("Leia Organa"));
                Assert.That(result[1].Gender, Is.EqualTo("female"));
            });
        }

        [Test]
        public void ToEntityList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var characters = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Description = "Test Description",
                    Name = "Luke Skywalker",
                    Gender = "male"
                },
                null
            };

            // Act
            var result = _mapper.ToEntityList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
        }
    }
}