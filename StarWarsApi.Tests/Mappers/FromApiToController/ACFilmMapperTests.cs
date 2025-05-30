using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACFilmMapperTests
    {
        private ACFilmMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACFilmMapper.Instance;
        }

        [Test]
        public void MapToController_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            FilmsApi? apiModel = null;

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WhenApiModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var apiModel = new FilmsApi
            {
                properties = new FilmsApi.Properties
                {
                    title = "A New Hope",
                    created = DateTime.Now,
                    edited = DateTime.Now,
                    starships = new List<string> { "https://swapi.dev/api/starships/1/" },
                    characters = new List<string> { "https://swapi.dev/api/people/1/" },
                    vehicles = new List<string> { "https://swapi.dev/api/vehicles/1/" },
                    planets = new List<string> { "https://swapi.dev/api/planets/1/" },
                    species = new List<string> { "https://swapi.dev/api/species/1/" }
                },
                description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(apiModel.properties.title));
            Assert.That(result.Description, Is.EqualTo(apiModel.description));
            Assert.That(result.Created, Is.EqualTo(apiModel.properties.created));
            Assert.That(result.Edited, Is.EqualTo(apiModel.properties.edited));
            Assert.That(result.Starships, Is.EqualTo("1"));
            Assert.That(result.Characters, Is.EqualTo("1"));
            Assert.That(result.Vehicles, Is.EqualTo("1"));
            Assert.That(result.Planets, Is.EqualTo("1"));
            Assert.That(result.Species, Is.EqualTo("1"));
        }

        [Test]
        public void MapToController_WhenCollectionsAreNull_ReturnsEmptyStrings()
        {
            // Arrange
            var apiModel = new FilmsApi
            {
                properties = new FilmsApi.Properties
                {
                    title = "A New Hope",
                    created = DateTime.Now,
                    edited = DateTime.Now,
                    starships = null,
                    characters = null,
                    vehicles = null,
                    planets = null,
                    species = null
                },
                description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Starships, Is.Empty);
            Assert.That(result.Characters, Is.Empty);
            Assert.That(result.Vehicles, Is.Empty);
            Assert.That(result.Planets, Is.Empty);
            Assert.That(result.Species, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<FilmsApi>? apiModels = null;

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var apiModels = new List<FilmsApi>
            {
                new FilmsApi
                {
                    properties = new FilmsApi.Properties
                    {
                        title = "A New Hope",
                        created = DateTime.Now,
                        edited = DateTime.Now
                    },
                    description = "The first Star Wars film"
                },
                new FilmsApi
                {
                    properties = new FilmsApi.Properties
                    {
                        title = "Empire Strikes Back",
                        created = DateTime.Now,
                        edited = DateTime.Now
                    },
                    description = "The second Star Wars film"
                }
            };

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
            Assert.That(result[1].Title, Is.EqualTo("Empire Strikes Back"));
        }
    }
}
