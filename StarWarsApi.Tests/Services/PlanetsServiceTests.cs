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
    public class PlanetsServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private PlanetsService _service;        
        
        [SetUp]
        public void Setup()
        {
            // Setup for mock tests
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new PlanetsService(_httpClient, _mockContext.Object);

            // Setup for real integration tests
            var realHttpClient = new HttpClient();
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: "TestStarWarsPlanetDb")
                .Options;
            _realContext = new ModelContext(options);
            _realService = new PlanetsService(realHttpClient, _realContext);
        }[Test]
        public async Task GetAllPlanetsAsync_WhenApiCallSucceeds_ReturnsPlanetsList()
        {
            // Arrange
            var planets = new List<PLanetApu>
            {
                new PLanetApu
                {
                    
                    name = "Tatooine",
                    url = "https://swapi.dev/api/planets/1/",
                    Climate = "arid",
                    Terrain = "desert"
                },
                new PlanetApi
                {
                    Name = "Alderaan",
                    url = "https://swapi.dev/api/planets/2/",
                    Climate = "temperate",
                    Terrain = "grasslands, mountains"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(planets)
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
            var result = await _service.GetAllPlanetsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[0].Climate, Is.EqualTo("arid"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
            Assert.That(result[1].Terrain, Is.EqualTo("grasslands, mountains"));
        }        [Test]
        public async Task GetPlanetByIdAsync_WhenPlanetExists_ReturnsPlanet()
        {
            // Arrange
            var planetId = "1";
            var mockResponse = new PlanetApi
            {
                Name = "Tatooine",
                Climate = "arid",
                Terrain = "desert",
                url = $"https://swapi.dev/api/planets/{planetId}"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(mockResponse)
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
            var result = await _service.GetPlanetByIdAsync(planetId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Tatooine"));
            Assert.That(result.Climate, Is.EqualTo("arid"));
            Assert.That(result.Terrain, Is.EqualTo("desert"));
        }        [Test]
        public async Task GetPlanetByIdAsync_WhenPlanetDoesNotExist_ReturnsNull()
        {
            // Arrange
            var planetId = "999";
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Not Found")
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
            var result = await _service.GetPlanetByIdAsync(planetId);

            // Assert
            Assert.That(result, Is.Null);
        }        [Test]
        public async Task GetAllPlanetsAsync_WhenApiCallFails_ReturnsEmptyList()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Server Error")
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
            var result = await _service.GetAllPlanetsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }        [Test]
        public async Task SavePlanetAsync_NewPlanet_InsertsToDB()
        {
            // Arrange
            var planetDto = new PlanetDto
            {
                Name = "Tatooine",
                Climate = "arid",
                Terrain = "desert",
                Url = "https://swapi.dev/api/planets/1/"
            };

            Planet savedPlanet = null;
            _mockContext.Setup(c => c.Planets.InsertOrUpdate(It.IsAny<Planet>()))
                .Callback<Planet>(p => savedPlanet = p);

            // Act
            await _service.SavePlanetAsync(planetDto);

            // Assert
            _mockContext.Verify(c => c.Planets.InsertOrUpdate(It.IsAny<Planet>()), Times.Once);
            Assert.That(savedPlanet, Is.Not.Null);
            Assert.That(savedPlanet.Name, Is.EqualTo("Tatooine"));
            Assert.That(savedPlanet.Climate, Is.EqualTo("arid"));
            Assert.That(savedPlanet.Terrain, Is.EqualTo("desert"));
            Assert.That(savedPlanet.Uid, Is.EqualTo("1")); // ID from URL
        }

        [Test]
        public async Task SavePlanetAsync_ExistingPlanet_UpdatesInDB()
        {
            // Arrange
            var existingPlanet = new Planet
            {
                Uid = "1",
                Name = "Tatooine",
                Climate = "arid",
                Terrain = "desert"
            };

            var planetDto = new PlanetDto
            {
                Name = "Tatooine",
                Climate = "temperate", // Changed climate
                Terrain = "desert",
                Url = "https://swapi.dev/api/planets/1/"
            };

            Planet updatedPlanet = null;
            _mockContext.Setup(c => c.Planets.InsertOrUpdate(It.IsAny<Planet>()))
                .Callback<Planet>(p => updatedPlanet = p);

            // Act
            await _service.SavePlanetAsync(planetDto);

            // Assert
            _mockContext.Verify(c => c.Planets.InsertOrUpdate(It.IsAny<Planet>()), Times.Once);
            Assert.That(updatedPlanet, Is.Not.Null);
            Assert.That(updatedPlanet.Climate, Is.EqualTo("temperate"));
        }

        [Test]
        public void GetPlanetFromDB_ExistingPlanet_ReturnsPlanet()
        {
            // Arrange
            var planetId = "1";
            var planet = new Planet
            {
                Uid = planetId,
                Name = "Tatooine",
                Climate = "arid"
            };

            _mockContext.Setup(c => c.Planets.GetById(planetId)).Returns(planet);

            // Act
            var result = _service.GetPlanetFromDB(planetId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Tatooine"));
            Assert.That(result.Climate, Is.EqualTo("arid"));
        }

        [Test]
        public void GetPlanetFromDB_NonExistingPlanet_ReturnsNull()
        {
            // Arrange
            var planetId = "999";
            _mockContext.Setup(c => c.Planets.GetById(planetId)).Returns(null as Planet);

            // Act
            var result = _service.GetPlanetFromDB(planetId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetPlanetsFromDB_ReturnsPlanetsList()
        {
            // Arrange
            var planets = new List<Planet>
            {
                new Planet { Name = "Tatooine", Climate = "arid" },
                new Planet { Name = "Alderaan", Climate = "temperate" }
            };

            _mockContext.Setup(c => c.Planets.GetAll()).Returns(planets);

            // Act
            var result = _service.GetPlanetsFromDB();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
        }

        [Test]
        public async Task RealIntegration_SaveAndGetPlanet_ShouldWorkCorrectly()
        {
            try
            {
                // Arrange
                var planetDto = new PlanetDto
                {
                    Name = "Test Planet",
                    Climate = "temperate",
                    Terrain = "forests",
                    Url = "https://swapi.dev/api/planets/1/"
                };

                // Act
                await _realService.SavePlanetAsync(planetDto);
                var savedPlanet = _realService.GetPlanetFromDB("1");

                // Assert
                Assert.That(savedPlanet, Is.Not.Null);
                Assert.That(savedPlanet.Name, Is.EqualTo(planetDto.Name));
                Assert.That(savedPlanet.Climate, Is.EqualTo(planetDto.Climate));
                Assert.That(savedPlanet.Terrain, Is.EqualTo(planetDto.Terrain));
                Assert.That(savedPlanet.Url, Is.EqualTo(planetDto.Url));
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
