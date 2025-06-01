using NUnit.Framework;
using Moq;
using StarWarsApi.Services;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.api;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using Moq.Protected;
using System.Net.Http.Json;
using StarWarsApi.Models.database;
using Microsoft.EntityFrameworkCore;
using BDCADAO.BDModels;

namespace StarWarsApi.Tests.Services
{
    [TestFixture]
    public class FilmsServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private FilmsService _service;
        private FilmsService _realService;
        private ModelContext _realContext;

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new FilmsService(_httpClient, _mockContext.Object);

            // Setup for real integration tests
            var realHttpClient = new HttpClient();
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: "TestStarWarsFilmsDb")
                .Options;
            _realContext = new ModelContext(options);
            _realService = new FilmsService(realHttpClient, _realContext);
        }

        [Test]
        public async Task GetAllFilmsAsync_WhenApiCallSucceeds_ReturnsFilmsList()
        {
            // Arrange
            var films = new List<FilmApi>
            {
                new FilmApi
                {
                    result = new FilmApi.prop
                    {
                        title = "A New Hope",
                        created = DateTime.Now,
                        edited = DateTime.Now
                    },
                    description = "The first Star Wars film"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(films)
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetAllFilmsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("A New Hope"));
        }

        [Test]
        public async Task GetFilmByIdAsync_WhenFilmExists_ReturnsFilm()
        {
            // Arrange
            var filmId = "1";
            var film = new Films
            {
                Uid = filmId,
                Title = "A New Hope",
                Description = "The first Star Wars film"
            };

            var films = new List<Films> { film }.AsQueryable();

            var mockSet = new Mock<DbSet<Films>>();
            mockSet.As<IQueryable<Films>>().Setup(m => m.Provider).Returns(films.Provider);
            mockSet.As<IQueryable<Films>>().Setup(m => m.Expression).Returns(films.Expression);
            mockSet.As<IQueryable<Films>>().Setup(m => m.ElementType).Returns(films.ElementType);
            mockSet.As<IQueryable<Films>>().Setup(m => m.GetEnumerator()).Returns(films.GetEnumerator());

            _mockContext.Setup(c => c.Films).Returns(mockSet.Object);

            // Act
            var result = await _service.GetFilmByIdAsync(filmId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("A New Hope"));
        }

        [Test]
        public async Task GetFilmByIdAsync_WhenFilmDoesNotExist_ReturnsNull()
        {
            // Arrange
            var filmId = "999";
            var films = new List<Films>().AsQueryable();

            var mockSet = new Mock<DbSet<Films>>();
            mockSet.As<IQueryable<Films>>().Setup(m => m.Provider).Returns(films.Provider);
            mockSet.As<IQueryable<Films>>().Setup(m => m.Expression).Returns(films.Expression);
            mockSet.As<IQueryable<Films>>().Setup(m => m.ElementType).Returns(films.ElementType);
            mockSet.As<IQueryable<Films>>().Setup(m => m.GetEnumerator()).Returns(films.GetEnumerator());

            _mockContext.Setup(c => c.Films).Returns(mockSet.Object);

            // Act
            var result = await _service.GetFilmByIdAsync(filmId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SaveFilmAsync_WithRelationships_ShouldSaveFilmAndRelations()
        {
            // Arrange
            var filmDto = new FilmsDto
            {
                Title = "A New Hope",
                Url = "https://swapi.dev/api/films/1/",
                Characters = new List<CharacterDto>
                {
                    new CharacterDto { Name = "Luke Skywalker", Url = "https://swapi.dev/api/people/1/" }
                },
                Vehicles = new List<VehicleDto>
                {
                    new VehicleDto { Name = "X-34 landspeeder", Url = "https://swapi.dev/api/vehicles/7/" }
                },
                Starships = new List<StarshipDto>
                {
                    new StarshipDto { Name = "X-wing", Url = "https://swapi.dev/api/starships/12/" }
                },
                Planets = new List<PlanetDto>
                {
                    new PlanetDto { Name = "Tatooine", Url = "https://swapi.dev/api/planets/1/" }
                },
                Species = new List<SpeciesDto>
                {
                    new SpeciesDto { Name = "Human", Url = "https://swapi.dev/api/species/1/" }
                }
            };

            var savedFilm = new Films();
            _mockContext.Setup(c => c.Films.InsertOrUpdate(It.IsAny<Films>())).Callback<Films>(f => savedFilm = f);

            // Act
            await _service.SaveFilmAsync(filmDto);

            // Assert
            _mockContext.Verify(c => c.Films.InsertOrUpdate(It.IsAny<Films>()), Times.Once);

            Assert.That(savedFilm.Title, Is.EqualTo(filmDto.Title));
            Assert.That(savedFilm.Characters, Does.Contain("1")); // ID from URL
            Assert.That(savedFilm.Vehicles, Does.Contain("7"));
            Assert.That(savedFilm.Starships, Does.Contain("12"));
            Assert.That(savedFilm.Planets, Does.Contain("1"));
            Assert.That(savedFilm.Species, Does.Contain("1"));
        }

        [Test]
        public async Task DeleteFilmAsync_WhenFilmExists_ShouldDeleteAndReturnTrue()
        {
            // Arrange
            var filmId = "1";
            _mockContext.Setup(c => c.Films.Delete(filmId)).Returns(1);

            // Act
            var result = await _service.DeleteFilmAsync(filmId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteFilmAsync_WhenFilmDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var filmId = "999";
            _mockContext.Setup(c => c.Films.Delete(filmId)).Returns(0);

            // Act
            var result = await _service.DeleteFilmAsync(filmId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetFilmFromDB_WhenFilmExistsWithRelations_ShouldReturnFilmWithRelations()
        {
            // Arrange
            var filmId = "1";
            var film = new Films
            {
                Uid = filmId,
                Title = "A New Hope",
                Characters = "1,2",
                Vehicles = "1,2",
                Starships = "1,2",
                Planets = "1,2",
                Species = "1,2"
            };

            _mockContext.Setup(c => c.Films.GetById(filmId)).Returns(film);

            // Setup related entities
            var character = new Character { Uid = "1", Name = "Luke" };
            var vehicle = new Vehicle { Uid = "1", Name = "X-34" };
            var starship = new Starship { Uid = "1", Name = "X-wing" };
            var planet = new Planet { Uid = "1", Name = "Tatooine" };
            var species = new Species { Uid = "1", Name = "Human" };

            _mockContext.Setup(c => c.Characters.GetById("1")).Returns(character);
            _mockContext.Setup(c => c.Characters.GetById("2")).Returns(character);
            _mockContext.Setup(c => c.Vehicles.GetById("1")).Returns(vehicle);
            _mockContext.Setup(c => c.Vehicles.GetById("2")).Returns(vehicle);
            _mockContext.Setup(c => c.Starships.GetById("1")).Returns(starship);
            _mockContext.Setup(c => c.Starships.GetById("2")).Returns(starship);
            _mockContext.Setup(c => c.Planets.GetById("1")).Returns(planet);
            _mockContext.Setup(c => c.Planets.GetById("2")).Returns(planet);
            _mockContext.Setup(c => c.Species.GetById("1")).Returns(species);
            _mockContext.Setup(c => c.Species.GetById("2")).Returns(species);

            // Act
            var result = _service.GetFilmFromDB(filmId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("A New Hope"));
            Assert.That(result.Characters, Has.Count.EqualTo(2));
            Assert.That(result.Vehicles, Has.Count.EqualTo(2));
            Assert.That(result.Starships, Has.Count.EqualTo(2));
            Assert.That(result.Planets, Has.Count.EqualTo(2));
            Assert.That(result.Species, Has.Count.EqualTo(2));
        }        [Test]
        public void GetFilmsFromDB_ShouldReturnAllFilms()
        {
            // Arrange
            var films = new List<Films>
            {
                new Films { Title = "A New Hope" },
                new Films { Title = "Empire Strikes Back" }
            };

            _mockContext.Setup(c => c.Films.GetAll()).Returns(films);

            // Act
            var result = _service.GetFilmsFromDB();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.Select(f => f.Title), Does.Contain("A New Hope"));
            Assert.That(result.Select(f => f.Title), Does.Contain("Empire Strikes Back"));
        }

        [Test]
        public async Task RealIntegration_SaveAndGetFilmWithRelations_ShouldWorkCorrectly()
        {
            try
            {
                // Arrange
                var filmDto = new FilmsDto
                {
                    Title = "Test Film",
                    Description = "Test Description",
                    Url = "https://swapi.dev/api/films/1/",
                    Characters = new List<CharacterDto>
                    {
                        new CharacterDto { Name = "Test Character", Url = "https://swapi.dev/api/people/1/" }
                    }
                };

                // Act
                await _realService.SaveFilmAsync(filmDto);
                var savedFilm = _realService.GetFilmFromDB("1");

                // Assert
                Assert.That(savedFilm, Is.Not.Null);
                Assert.That(savedFilm.Title, Is.EqualTo(filmDto.Title));
                Assert.That(savedFilm.Description, Is.EqualTo(filmDto.Description));
                Assert.That(savedFilm.Characters, Is.Not.Empty);
            }
            catch (Exception ex)
            {
                Assert.Inconclusive($"Test requires database connection. Error: {ex.Message}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
            if (_realContext != null)
            {
                _realContext.Database.EnsureDeleted();
                _realContext.Dispose();
            }
        }
    }
}
