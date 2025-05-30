using NUnit.Framework;
using Moq;
using StarWarsApi.Services;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using System.Net;
using System.Text.Json;
using Moq.Protected;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace StarWarsApi.Tests.Services
{
    [TestFixture]
    public class SpeciesServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private SpeciesService _service;
        private SpeciesService _realService;
        private ModelContext _realContext;

        [SetUp]
        public void Setup()
        {
            // Setup for mocked tests
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new SpeciesService(_httpClient, _mockContext.Object);

            // Setup for real integration test
            var realHttpClient = new HttpClient();
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: "TestStarWarsSpeciesDb")
                .Options;
            _realContext = new ModelContext(options);
            _realService = new SpeciesService(realHttpClient, _realContext);
        }

        [Test]
        public async Task GetAllSpeciesAsync_WhenApiCallSucceeds_ReturnsSpeciesList()
        {
            // Arrange
            var speciesList = new List<SpeciesApi>
            {
                new SpeciesApi
                {
                    Uid = "1",
                    Name = "Human",
                    Url = "https://swapi.dev/api/species/1/"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(speciesList)
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
            var result = await _service.GetAllSpeciesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
        }

        [Test]
        public async Task GetAllSpeciesAsync_WhenApiCallFails_ThrowsException()
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
            Assert.ThrowsAsync<HttpRequestException>(() => _service.GetAllSpeciesAsync());
        }

        [Test]
        public async Task GetSpeciesByIdAsync_WhenSpeciesExists_ReturnsSpecies()
        {
            // Arrange
            var speciesId = "1";
            var species = new Species
            {
                Uid = speciesId,
                Name = "Human",
                Url = "https://swapi.dev/api/species/1/"
            };

            var mockSet = new Mock<DbSet<Species>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(species);

            _mockContext.Setup(c => c.Species).Returns(mockSet.Object);

            // Act
            var result = await _service.GetSpeciesByIdAsync(speciesId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Human"));
        }

        [Test]
        public async Task GetSpeciesByIdAsync_WhenSpeciesDoesNotExist_ReturnsNull()
        {
            // Arrange
            var speciesId = "999";
            
            var mockSet = new Mock<DbSet<Species>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((Species)null);

            _mockContext.Setup(c => c.Species).Returns(mockSet.Object);

            // Act
            var result = await _service.GetSpeciesByIdAsync(speciesId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SaveSpeciesAsync_ShouldSaveSpecies()
        {
            // Arrange
            var speciesDto = new SpeciesDto
            {
                Name = "Human",
                Url = "https://swapi.dev/api/species/1/"
            };

            var savedSpecies = new Species();
            _mockContext.Setup(c => c.Species.Add(It.IsAny<Species>())).Callback<Species>(s => savedSpecies = s);

            // Act
            _service.SaveSpeciesAsync(speciesDto);
            await _mockContext.Object.SaveChangesAsync();

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
            Assert.That(savedSpecies.Name, Is.EqualTo(speciesDto.Name));
            Assert.That(savedSpecies.Url, Is.EqualTo(speciesDto.Url));
        }

        [Test]
        public async Task RealIntegration_GetAllSpeciesAsync_ShouldReturnSpecies()
        {
            try
            {
                // Act
                var result = await _realService.GetAllSpeciesAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result[0].Name, Is.Not.Empty);
                Assert.That(result[0].Url, Does.StartWith("https://swapi.dev/api/species/"));
            }
            catch (HttpRequestException ex)
            {
                Assert.Inconclusive($"Test requires internet connection and API availability. Error: {ex.Message}");
            }
        }

        [Test]
        public async Task RealIntegration_SaveAndGetSpeciesById_ShouldWorkCorrectly()
        {
            // Arrange
            var speciesDto = new SpeciesDto
            {
                Name = "Test Species",
                Url = "https://swapi.dev/api/species/1/"
            };

            // Act
            _realService.SaveSpeciesAsync(speciesDto);
            var savedSpecies = await _realService.GetSpeciesByIdAsync("1");

            // Assert
            Assert.That(savedSpecies, Is.Not.Null);
            Assert.That(savedSpecies.Name, Is.EqualTo(speciesDto.Name));
            Assert.That(savedSpecies.Url, Is.EqualTo(speciesDto.Url));
        }

        [TearDown]
        public void TearDown()
        {
            // Limpiar la base de datos en memoria después de cada prueba
            if (_realContext != null)
            {
                _realContext.Database.EnsureDeleted();
            }
        }
    }
}
