using NUnit.Framework;
using StarWarsApi.Mappers.FromDatabaseToController;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCFilmMapperTests
    {
        private DCFilmMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCFilmMapper.Instance;
        }

        [Test]
        public void ToDto_WhenDatabaseModelIsNull_ReturnsNull()
        {
            // Arrange
            Films? dbModel = null;

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WhenDatabaseModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var now = DateTime.Now;
            var dbModel = new Films
            {
                Title = "A New Hope",
                Characters = "1,2",
                Planets = "1,2",
                Species = "1,2",
                Starships = "1,2",
                Vehicles = "1,2",
                Description = "The first Star Wars film",
                Created = now,
                Edited = now
            };

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(dbModel.Title));
            Assert.That(result.Characters, Is.EqualTo(dbModel.Characters));
            Assert.That(result.Planets, Is.EqualTo(dbModel.Planets));
            Assert.That(result.Species, Is.EqualTo(dbModel.Species));
            Assert.That(result.Starships, Is.EqualTo(dbModel.Starships));
            Assert.That(result.Vehicles, Is.EqualTo(dbModel.Vehicles));
            Assert.That(result.Description, Is.EqualTo(dbModel.Description));
            Assert.That(result.Created, Is.EqualTo(dbModel.Created));
            Assert.That(result.Edited, Is.EqualTo(dbModel.Edited));
        }

        [Test]
        public void ToDto_WhenDatabaseModelHasNullCollections_ReturnsEmptyCollections()
        {
            // Arrange
            var dbModel = new Films
            {
                Title = "A New Hope",
                Characters = null,
                Planets = null,
                Species = null,
                Starships = null,
                Vehicles = null,
                Description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.ToDto(dbModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Characters, Is.Not.Null.And.Empty);
            Assert.That(result.Planets, Is.Not.Null.And.Empty);
            Assert.That(result.Species, Is.Not.Null.And.Empty);
            Assert.That(result.Starships, Is.Not.Null.And.Empty);
            Assert.That(result.Vehicles, Is.Not.Null.And.Empty);
        }

        [Test]
        public void ToDtoList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<Films>? dbModels = null;

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dbModels = new List<Films>();

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var now = DateTime.Now;
            var dbModels = new List<Films>
            {
                new Films
                {
                    Title = "A New Hope",
                    Description = "The first Star Wars film",
                    Created = now,
                    Edited = now,
                    Uid = "1"
                },
                new Films
                {
                    Title = "The Empire Strikes Back",
                    Description = "The second Star Wars film",
                    Created = now,
                    Edited = now,
                    Uid = "2"
                }
            };

            // Act
            var result = _mapper.ToDtoList(dbModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
            Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
            Assert.That(result[0].Description, Is.EqualTo("The first Star Wars film"));
            Assert.That(result[1].Description, Is.EqualTo("The second Star Wars film"));
        }
    }
}
