using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromControllerToDatabase;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CDFilmMapper.Instance;
            var instance2 = CDFilmMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToEntity_WithNullInput_ReturnsNull()
        {
            // Arrange
            FilmsDto? nullFilm = null;

            // Act
            var result = _mapper.ToEntity(nullFilm);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var filmDto = new FilmsDto
            {
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Producer = "George Lucas",
                Title = "A New Hope",
                EpisodeId = 4,
                Director = "George Lucas",
                ReleaseDate = "1977-05-25",
                OpeningCrawl = "It is a period of civil war...",
                Url = "https://swapi.dev/api/films/1",
                Characters = new List<CharacterDto>
                {
                    new CharacterDto { Url = "https://swapi.dev/api/people/1" },
                    new CharacterDto { Url = "https://swapi.dev/api/people/2" }
                },
                Planets = new List<PlanetDto>
                {
                    new PlanetDto { Url = "https://swapi.dev/api/planets/1" },
                    new PlanetDto { Url = "https://swapi.dev/api/planets/2" }
                },
                Starships = new List<StarshipDto>
                {
                    new StarshipDto { Url = "https://swapi.dev/api/starships/2" },
                    new StarshipDto { Url = "https://swapi.dev/api/starships/3" }
                },
                Vehicles = new List<VehicleDto>
                {
                    new VehicleDto { Url = "https://swapi.dev/api/vehicles/4" },
                    new VehicleDto { Url = "https://swapi.dev/api/vehicles/6" }
                },
                Species = new List<SpeciesDto>
                {
                    new SpeciesDto { Url = "https://swapi.dev/api/species/1" },
                    new SpeciesDto { Url = "https://swapi.dev/api/species/2" }
                }
            };

            // Act
            var result = _mapper.ToEntity(filmDto);

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
                Assert.That(result.Characters, Is.EqualTo("1,2"));
                Assert.That(result.Planets, Is.EqualTo("1,2"));
                Assert.That(result.Starships, Is.EqualTo("2,3"));
                Assert.That(result.Vehicles, Is.EqualTo("4,6"));
                Assert.That(result.Species, Is.EqualTo("1,2"));
            });
        }

        [Test]
        public void ToEntity_WithNullCollections_ReturnsNullForRelatedFields()
        {
            // Arrange
            var filmDto = new FilmsDto
            {
                Title = "A New Hope",
                Characters = null,
                Planets = null,
                Starships = null,
                Vehicles = null,
                Species = null
            };

            // Act
            var result = _mapper.ToEntity(filmDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Characters, Is.Null);
                Assert.That(result.Planets, Is.Null);
                Assert.That(result.Starships, Is.Null);
                Assert.That(result.Vehicles, Is.Null);
                Assert.That(result.Species, Is.Null);
            });
        }

        [Test]
        public void ToEntity_WithEmptyCollections_ReturnsNullForRelatedFields()
        {
            // Arrange
            var filmDto = new FilmsDto
            {
                Title = "A New Hope",
                Characters = new List<CharacterDto>(),
                Planets = new List<PlanetDto>(),
                Starships = new List<StarshipDto>(),
                Vehicles = new List<VehicleDto>(),
                Species = new List<SpeciesDto>()
            };

            // Act
            var result = _mapper.ToEntity(filmDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Characters, Is.Null);
                Assert.That(result.Planets, Is.Null);
                Assert.That(result.Starships, Is.Null);
                Assert.That(result.Vehicles, Is.Null);
                Assert.That(result.Species, Is.Null);
            });
        }

        [Test]
        public void ToEntityList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<FilmsDto>? nullList = null;

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
            var emptyList = new List<FilmsDto>();

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
            var films = new List<FilmsDto>
            {
                new FilmsDto
                {
                    Title = "A New Hope",
                    EpisodeId = 4,
                    Characters = new List<CharacterDto>
                    {
                        new CharacterDto { Url = "https://swapi.dev/api/people/1" }
                    }
                },
                new FilmsDto
                {
                    Title = "The Empire Strikes Back",
                    EpisodeId = 5,
                    Characters = new List<CharacterDto>
                    {
                        new CharacterDto { Url = "https://swapi.dev/api/people/2" }
                    }
                }
            };

            // Act
            var result = _mapper.ToEntityList(films);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
                Assert.That(result[0].EpisodeId, Is.EqualTo(4));
                Assert.That(result[0].Characters, Is.EqualTo("1"));
                Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
                Assert.That(result[1].EpisodeId, Is.EqualTo(5));
                Assert.That(result[1].Characters, Is.EqualTo("2"));
            });
        }
    }
}