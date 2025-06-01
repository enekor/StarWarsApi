using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromDatabaseToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCCharacterMapperTests
    {
        private DCCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCCharacterMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = DCCharacterMapper.Instance;
            var instance2 = DCCharacterMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToDto_WithNullInput_ReturnsNull()
        {
            // Arrange
            Character? nullCharacter = null;

            // Act
            var result = _mapper.ToDto(nullCharacter);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var character = new Character
            {
                Uid = "1",
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
            var result = _mapper.ToDto(character);

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
        public void ToDtoList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<Character>? nullList = null;

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
            var emptyList = new List<Character>();

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
            var characters = new List<Character>
            {
                new Character
                {
                    Uid = "1",
                    Description = "Test Description 1",
                    Name = "Luke Skywalker",
                    Gender = "male"
                },
                new Character
                {
                    Uid = "2",
                    Description = "Test Description 2",
                    Name = "Leia Organa",
                    Gender = "female"
                }
            };

            // Act
            var result = _mapper.ToDtoList(characters);

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
        public void ToDtoList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var characters = new List<Character>
            {
                null,
                new Character
                {
                    Uid = "1",
                    Description = "Test Description",
                    Name = "Luke Skywalker",
                    Gender = "male"
                }
            };

            // Act
            var result = _mapper.ToDtoList(characters);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
        }
    }
}