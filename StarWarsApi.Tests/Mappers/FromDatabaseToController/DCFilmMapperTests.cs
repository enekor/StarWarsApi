using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromDatabaseToController;
using System;
using System.Collections.Generic;

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
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = DCFilmMapper.Instance;
            var instance2 = DCFilmMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToDto_WithNullInput_ReturnsNull()
        {
            // Arrange
            Films? nullFilm = null;

            // Act
            var result = _mapper.ToDto(nullFilm);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WithValidInputNoRelations_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var film = new Films
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
                Url = "https://swapi.dev/api/films/1"
            };

            // Act
            var result = _mapper.ToDto(film);

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
                
                // Verificar que las listas de relaciones están vacías pero inicializadas
                Assert.That(result.Starships, Is.Not.Null.And.Empty);
                Assert.That(result.Characters, Is.Not.Null.And.Empty);
                Assert.That(result.Vehicles, Is.Not.Null.And.Empty);
                Assert.That(result.Planets, Is.Not.Null.And.Empty);
                Assert.That(result.Species, Is.Not.Null.And.Empty);
            });
        }

        [Test]
        public void ToDto_WithValidInputAndRelations_ReturnsCorrectMapping()
        {
            // Arrange
            var film = new Films
            {
                Title = "A New Hope",
                EpisodeId = 4
            };

            var starships = new List<StarshipDto> 
            { 
                new StarshipDto { Name = "X-wing" } 
            };
            var characters = new List<CharacterDto> 
            { 
                new CharacterDto { Name = "Luke Skywalker" } 
            };
            var vehicles = new List<VehicleDto> 
            { 
                new VehicleDto { Name = "Snowspeeder" } 
            };
            var planets = new List<PlanetDto> 
            { 
                new PlanetDto { Name = "Tatooine" } 
            };
            var species = new List<SpeciesDto> 
            { 
                new SpeciesDto { Name = "Human" } 
            };

            // Act
            var result = _mapper.ToDto(film, starships, characters, vehicles, planets, species);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Title, Is.EqualTo("A New Hope"));
                Assert.That(result.EpisodeId, Is.EqualTo(4));
                Assert.That(result.Starships, Is.EqualTo(starships));
                Assert.That(result.Characters, Is.EqualTo(characters));
                Assert.That(result.Vehicles, Is.EqualTo(vehicles));
                Assert.That(result.Planets, Is.EqualTo(planets));
                Assert.That(result.Species, Is.EqualTo(species));
            });
        }

        [Test]
        public void ToDto_WithNullRelations_InitializesEmptyLists()
        {
            // Arrange
            var film = new Films
            {
                Title = "A New Hope",
                EpisodeId = 4
            };

            // Act
            var result = _mapper.ToDto(film, null, null, null, null, null);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Starships, Is.Not.Null.And.Empty);
                Assert.That(result.Characters, Is.Not.Null.And.Empty);
                Assert.That(result.Vehicles, Is.Not.Null.And.Empty);
                Assert.That(result.Planets, Is.Not.Null.And.Empty);
                Assert.That(result.Species, Is.Not.Null.And.Empty);
            });
        }

        [Test]
        public void ToDtoList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<Films>? nullList = null;

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
            var emptyList = new List<Films>();

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
            var films = new List<Films>
            {
                new Films
                {
                    Title = "A New Hope",
                    EpisodeId = 4,
                    Director = "George Lucas"
                },
                new Films
                {
                    Title = "The Empire Strikes Back",
                    EpisodeId = 5,
                    Director = "Irvin Kershner"
                }
            };

            // Act
            var result = _mapper.ToDtoList(films);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
                Assert.That(result[0].EpisodeId, Is.EqualTo(4));
                Assert.That(result[0].Director, Is.EqualTo("George Lucas"));
                Assert.That(result[1].Title, Is.EqualTo("The Empire Strikes Back"));
                Assert.That(result[1].EpisodeId, Is.EqualTo(5));
                Assert.That(result[1].Director, Is.EqualTo("Irvin Kershner"));
            });
        }
    }
}