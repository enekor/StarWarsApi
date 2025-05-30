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
        public void ToDto_WhenDatabaseModelHasNullCollections_ReturnsNullCollections()
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
            Assert.That(result.Characters, Is.Null);
            Assert.That(result.Planets, Is.Null);
            Assert.That(result.Species, Is.Null);
            Assert.That(result.Starships, Is.Null);
            Assert.That(result.Vehicles, Is.Null);
        }
    }
}
