using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACFilmMapper.Instance;
            var instance2 = ACFilmMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullFilmApi_ReturnsNull()
        {
            // Arrange
            FilmApi? nullFilm = null;

            // Act
            var result = _mapper.MapToController(nullFilm);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidFilmApi_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var filmApi = new FilmApi
            {
                result = new FilmResult
                {
                    description = "Test Description",
                    properties = new FilmProperties
                    {
                        created = created,
                        edited = edited,
                        producer = "George Lucas",
                        title = "A New Hope",
                        episode_id = 4,
                        director = "George Lucas",
                        release_date = "1977-05-25",
                        opening_crawl = "It is a period of civil war...",
                        url = "https://swapi.dev/api/films/1"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(filmApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Producer, Is.EqualTo("George Lucas"));
                Assert.That(result.Title, Is.EqualTo("A New Hope"));
                Assert.That(result.EpisodeId, Is.EqualTo(4));
                Assert.That(result.Director, Is.EqualTo("George Lucas"));
                Assert.That(result.ReleaseDate, Is.EqualTo("1977-05-25"));
                Assert.That(result.OpeningCrawl, Is.EqualTo("It is a period of civil war..."));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/films/1"));
                Assert.That(result.Starships, Is.Empty);
                Assert.That(result.Characters, Is.Empty);
                Assert.That(result.Vehicles, Is.Empty);
                Assert.That(result.Planets, Is.Empty);
                Assert.That(result.Species, Is.Empty);
            });
        }

        [Test]
        public void MapToController_WithNullFilmResult_ReturnsNull()
        {
            // Arrange
            FilmResult? nullFilm = null;

            // Act
            var result = _mapper.MapToController(nullFilm);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidFilmResult_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var filmResult = new FilmResult
            {
                description = "Test Description",
                properties = new FilmProperties
                {
                    created = created,
                    edited = edited,
                    producer = "George Lucas",
                    title = "A New Hope",
                    episode_id = 4,
                    director = "George Lucas",
                    release_date = "1977-05-25",
                    opening_crawl = "It is a period of civil war...",
                    url = "https://swapi.dev/api/films/1"
                }
            };

            // Act
            var result = _mapper.MapToController(filmResult);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Producer, Is.EqualTo("George Lucas"));
                Assert.That(result.Title, Is.EqualTo("A New Hope"));
                Assert.That(result.EpisodeId, Is.EqualTo(4));
                Assert.That(result.Director, Is.EqualTo("George Lucas"));
                Assert.That(result.ReleaseDate, Is.EqualTo("1977-05-25"));
                Assert.That(result.OpeningCrawl, Is.EqualTo("It is a period of civil war..."));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/films/1"));
                Assert.That(result.Starships, Is.Empty);
                Assert.That(result.Characters, Is.Empty);
                Assert.That(result.Vehicles, Is.Empty);
                Assert.That(result.Planets, Is.Empty);
                Assert.That(result.Species, Is.Empty);
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<FilmApi>? nullList = null;

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
            var emptyList = new List<FilmApi>();

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
            var films = new List<FilmApi>
            {
                new FilmApi
                {
                    result = new FilmResult
                    {
                        description = "Test Description 1",
                        properties = new FilmProperties
                        {
                            title = "A New Hope",
                            episode_id = 4
                        }
                    }
                },
                new FilmApi
                {
                    result = new FilmResult
                    {
                        description = "Test Description 2",
                        properties = new FilmProperties
                        {
                            title = "The Empire Strikes Back",
                            episode_id = 5
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToControllerList(films);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
                Assert.That(result[0].EpisodeId, Is.EqualTo(4));
                Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
                Assert.That(result[1].EpisodeId, Is.EqualTo(5));
            });
        }

        [Test]
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var films = new List<FilmApi>
            {
                new FilmApi
                {
                    result = new FilmResult
                    {
                        description = "Test Description",
                        properties = new FilmProperties
                        {
                            title = "A New Hope",
                            episode_id = 4
                        }
                    }
                },
                null,
                new FilmApi { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(films);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
        }
    }
}