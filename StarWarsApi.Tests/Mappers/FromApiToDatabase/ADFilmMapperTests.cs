using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADFilmMapper.Instance;
            var instance2 = ADFilmMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            FilmApi? nullFilm = null;

            // Act
            var result = _mapper.MapToDatabase(nullFilm);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var filmApi = new FilmApi
            {
                result = new FilmResult
                {
                    uid = "1",
                    description = "Test Description",
                    properties = new FilmProperties
                    {
                        title = "A New Hope",
                        created = created,
                        edited = edited,
                        starships = new List<string> { "https://swapi.dev/api/starships/2", "https://swapi.dev/api/starships/3" },
                        vehicles = new List<string> { "https://swapi.dev/api/vehicles/4", "https://swapi.dev/api/vehicles/6" },
                        characters = new List<string> { "https://swapi.dev/api/people/1", "https://swapi.dev/api/people/2" },
                        planets = new List<string> { "https://swapi.dev/api/planets/1", "https://swapi.dev/api/planets/2" },
                        species = new List<string> { "https://swapi.dev/api/species/1", "https://swapi.dev/api/species/2" },
                        url = "https://swapi.dev/api/films/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(filmApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("1"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Title, Is.EqualTo("A New Hope"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Starships, Is.EqualTo("2,3"));
                Assert.That(result.Vehicles, Is.EqualTo("4,6"));
                Assert.That(result.Characters, Is.EqualTo("1,2"));
                Assert.That(result.Planets, Is.EqualTo("1,2"));
                Assert.That(result.Species, Is.EqualTo("1,2"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/films/1"));
            });
        }

        [Test]
        public void MapToDatabase_WithNullCollections_ReturnsEmptyStrings()
        {
            // Arrange
            var filmApi = new FilmApi
            {
                result = new FilmResult
                {
                    uid = "1",
                    description = "Test Description",
                    properties = new FilmProperties
                    {
                        title = "A New Hope",
                        starships = null,
                        vehicles = null,
                        characters = null,
                        planets = null,
                        species = null,
                        url = "https://swapi.dev/api/films/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(filmApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Starships, Is.Empty);
                Assert.That(result.Vehicles, Is.Empty);
                Assert.That(result.Characters, Is.Empty);
                Assert.That(result.Planets, Is.Empty);
                Assert.That(result.Species, Is.Empty);
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<FilmApi>? nullList = null;

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
            var emptyList = new List<FilmApi>();

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
            var films = new List<FilmApi>
            {
                new FilmApi
                {
                    result = new FilmResult
                    {
                        uid = "1",
                        description = "Test Description 1",
                        properties = new FilmProperties
                        {
                            title = "A New Hope",
                            starships = new List<string> { "https://swapi.dev/api/starships/2" }
                        }
                    }
                },
                new FilmApi
                {
                    result = new FilmResult
                    {
                        uid = "2",
                        description = "Test Description 2",
                        properties = new FilmProperties
                        {
                            title = "The Empire Strikes Back",
                            starships = new List<string> { "https://swapi.dev/api/starships/3" }
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(films);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
                Assert.That(result[0].Starships, Is.EqualTo("2"));
                Assert.That(result[0].Uid, Is.EqualTo("1"));
                Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
                Assert.That(result[1].Starships, Is.EqualTo("3"));
                Assert.That(result[1].Uid, Is.EqualTo("2"));
            });
        }
    }
}