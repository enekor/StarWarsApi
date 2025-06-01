using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDFilmMapperTests
    {
        private CDFilmMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDFilmMapper.Instance;
        }

        [Test]
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Arrange
            FilmsDto? dto = null;

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var now = DateTime.Now;
            var dto = new FilmsDto
            {
                Title = "A New Hope",
                Characters = "1,2",
                Planets = "1,2",
                Starships = "1,2",
                Vehicles = "1,2",
                Species = "1,2",
                Description = "The first Star Wars film",
                Created = now,
                Edited = now
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(dto.Title));
            Assert.That(result.Characters, Is.EqualTo(dto.Characters));
            Assert.That(result.Planets, Is.EqualTo(dto.Planets));
            Assert.That(result.Starships, Is.EqualTo(dto.Starships));
            Assert.That(result.Vehicles, Is.EqualTo(dto.Vehicles));
            Assert.That(result.Species, Is.EqualTo(dto.Species));
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            Assert.That(result.Created, Is.EqualTo(dto.Created));
            Assert.That(result.Edited, Is.EqualTo(dto.Edited));
        }

        [Test]
        public void ToEntity_WhenDtoHasNullCollections_ReturnsEntityWithNullCollections()
        {
            // Arrange
            var dto = new FilmsDto
            {
                Title = "A New Hope",
                Characters = null,
                Planets = null,
                Starships = null,
                Vehicles = null,
                Species = null,
                Description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Characters, Is.Null);
            Assert.That(result.Planets, Is.Null);
            Assert.That(result.Starships, Is.Null);
            Assert.That(result.Vehicles, Is.Null);
            Assert.That(result.Species, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoHasEmptyCollections_ReturnsEntityWithEmptyCollections()
        {
            // Arrange
            var dto = new FilmsDto
            {
                Title = "A New Hope",
                Characters = string.Empty,
                Planets = string.Empty,
                Starships = string.Empty,
                Vehicles = string.Empty,
                Species = string.Empty,
                Description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Characters, Is.Empty);
            Assert.That(result.Planets, Is.Empty);
            Assert.That(result.Starships, Is.Empty);
            Assert.That(result.Vehicles, Is.Empty);
            Assert.That(result.Species, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<FilmsDto>? dtos = null;

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<FilmsDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var now = DateTime.Now;
            var dtos = new List<FilmsDto>
            {
                new FilmsDto
                {
                    Title = "A New Hope",
                    Description = "The first Star Wars film",
                    Created = now,
                    Edited = now,
                    Url = "https://swapi.dev/api/films/1/"
                },
                new FilmsDto
                {
                    Title = "The Empire Strikes Back",
                    Description = "The second Star Wars film",
                    Created = now,
                    Edited = now,
                    Url = "https://swapi.dev/api/films/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
            Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
            Assert.That(result[0].Description, Is.EqualTo("The first Star Wars film"));
            Assert.That(result[1].Description, Is.EqualTo("The second Star Wars film"));
            Assert.That(result[0].Created, Is.EqualTo(now));
            Assert.That(result[1].Created, Is.EqualTo(now));
            Assert.That(result[0].Edited, Is.EqualTo(now));
            Assert.That(result[1].Edited, Is.EqualTo(now));
            Assert.That(result[0].Url, Is.EqualTo("https://swapi.dev/api/films/1/"));
            Assert.That(result[1].Url, Is.EqualTo("https://swapi.dev/api/films/2/"));
        }
    }
}
