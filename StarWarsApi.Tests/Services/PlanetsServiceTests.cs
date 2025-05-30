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
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new PlanetsService(_httpClient, _mockContext.Object);
        }

        [Test]
        public async Task GetAllPlanetsAsync_WhenApiCallSucceeds_ReturnsPlanetsList()
        {
            // Arrange
            var planets = new List<PlanetApi>
            {
                new PlanetApi
                {
                    Name = "Tatooine",
                    url = "https://swapi.dev/api/planets/1/"
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
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
        }

        [Test]
        public async Task GetPlanetByIdAsync_WhenPlanetExists_ReturnsPlanet()
        {
            // Arrange
            var planetId = "1";
            var planet = new Planet
            {
                Id = planetId,
                Name = "Tatooine"
            };

            var planets = new List<Planet> { planet }.AsQueryable();

            var mockSet = new Mock<DbSet<Planet>>();
            mockSet.As<IQueryable<Planet>>().Setup(m => m.Provider).Returns(planets.Provider);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.Expression).Returns(planets.Expression);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.ElementType).Returns(planets.ElementType);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.GetEnumerator()).Returns(planets.GetEnumerator());

            _mockContext.Setup(c => c.Planets).Returns(mockSet.Object);

            // Act
            var result = await _service.GetPlanetByIdAsync(planetId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Tatooine"));
        }

        [Test]
        public async Task GetPlanetByIdAsync_WhenPlanetDoesNotExist_ReturnsNull()
        {
            // Arrange
            var planetId = "999";
            var planets = new List<Planet>().AsQueryable();

            var mockSet = new Mock<DbSet<Planet>>();
            mockSet.As<IQueryable<Planet>>().Setup(m => m.Provider).Returns(planets.Provider);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.Expression).Returns(planets.Expression);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.ElementType).Returns(planets.ElementType);
            mockSet.As<IQueryable<Planet>>().Setup(m => m.GetEnumerator()).Returns(planets.GetEnumerator());

            _mockContext.Setup(c => c.Planets).Returns(mockSet.Object);

            // Act
            var result = await _service.GetPlanetByIdAsync(planetId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllPlanetsAsync_WhenApiCallFails_ThrowsException()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(() => _service.GetAllPlanetsAsync());
        }
    }
}
