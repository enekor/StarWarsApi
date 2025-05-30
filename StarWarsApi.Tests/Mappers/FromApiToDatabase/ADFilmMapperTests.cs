using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToDatabase;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADFilmMapperTests
    {
        private ADFilmMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADFilmMapper.Instance;
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            FilmsApi? apiModel = null;

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WhenApiModelIsValid_ReturnsCorrectDatabaseModel()
        {
            // Arrange
            var apiModel = new FilmsApi
            {
                _id = "1",
                properties = new FilmsApi.Properties
                {
                    title = "A New Hope",
                    created = DateTime.Now,
                    edited = DateTime.Now,
                    starships = new List<string> { "https://swapi.dev/api/starships/1/" },
                    vehicles = new List<string> { "https://swapi.dev/api/vehicles/1/" },
                    characters = new List<string> { "https://swapi.dev/api/people/1/" },
                    planets = new List<string> { "https://swapi.dev/api/planets/1/" },
                    species = new List<string> { "https://swapi.dev/api/species/1/" }
                },
                description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Uid, Is.EqualTo(apiModel._id));
            Assert.That(result.Title, Is.EqualTo(apiModel.properties.title));
            Assert.That(result.Description, Is.EqualTo(apiModel.description));
            Assert.That(result.Created, Is.EqualTo(apiModel.properties.created));
            Assert.That(result.Edited, Is.EqualTo(apiModel.properties.edited));
            Assert.That(result.Starships, Is.EqualTo("1"));
            Assert.That(result.Vehicles, Is.EqualTo("1"));
            Assert.That(result.Characters, Is.EqualTo("1"));
            Assert.That(result.Planets, Is.EqualTo("1"));
            Assert.That(result.Species, Is.EqualTo("1"));
        }

        [Test]
        public void MapToDatabase_WhenCollectionsAreNull_ReturnsEmptyStrings()
        {
            // Arrange
            var apiModel = new FilmsApi
            {
                _id = "1",
                properties = new FilmsApi.Properties
                {
                    title = "A New Hope",
                    created = DateTime.Now,
                    edited = DateTime.Now,
                    starships = null,
                    vehicles = null,
                    characters = null,
                    planets = null,
                    species = null
                },
                description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Starships, Is.Empty);
            Assert.That(result.Vehicles, Is.Empty);
            Assert.That(result.Characters, Is.Empty);
            Assert.That(result.Planets, Is.Empty);
            Assert.That(result.Species, Is.Empty);
        }

        [Test]
        public void MapToDatabase_WhenCollectionsHaveMultipleItems_JoinsIdsCorrectly()
        {
            // Arrange
            var apiModel = new FilmsApi
            {
                _id = "1",
                properties = new FilmsApi.Properties
                {
                    title = "A New Hope",
                    created = DateTime.Now,
                    edited = DateTime.Now,
                    starships = new List<string> 
                    { 
                        "https://swapi.dev/api/starships/1/",
                        "https://swapi.dev/api/starships/2/"
                    },
                    vehicles = new List<string>
                    {
                        "https://swapi.dev/api/vehicles/1/",
                        "https://swapi.dev/api/vehicles/2/"
                    }
                },
                description = "The first Star Wars film"
            };

            // Act
            var result = _mapper.MapToDatabase(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Starships, Is.EqualTo("1,2"));
            Assert.That(result.Vehicles, Is.EqualTo("1,2"));
        }
    }
}
