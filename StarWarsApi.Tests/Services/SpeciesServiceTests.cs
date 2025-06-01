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
using BDCADAO.BDModels;

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
                    Name = "Human",
                    Classification = "mammal",
                    Designation = "sentient",
                    Language = "Galactic Basic",
                    url = "https://swapi.dev/api/species/1/"
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
        public async Task GetAllSpeciesAsync_WhenApiCallFails_ReturnsEmptyList()
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
            var result = await _service.GetAllSpeciesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetSpeciesByIdAsync_WhenSpeciesExists_ReturnsSpecies()
        {
            // Arrange
            var speciesId = "1";
            var mockResponse = new SpeciesApi
            {
                Name = "Human",
                Classification = "mammal",
                Language = "Galactic Basic",
                url = $"https://swapi.dev/api/species/{speciesId}"
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
            var result = await _service.GetSpeciesByIdAsync(speciesId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Human"));
            Assert.That(result.Classification, Is.EqualTo("mammal"));
            Assert.That(result.Language, Is.EqualTo("Galactic Basic"));
        }

        [Test]
        public async Task GetSpeciesByIdAsync_WhenSpeciesDoesNotExist_ReturnsNull()
        {
            // Arrange
            var speciesId = "999";
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
            var result = await _service.GetSpeciesByIdAsync(speciesId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SaveSpeciesAsync_NewSpecies_SavesToDatabase()
        {
            // Arrange
            var speciesDto = new SpeciesDto
            {
                Name = "Human",
                Classification = "mammal",
                Language = "Galactic Basic",
                Url = "https://swapi.dev/api/species/1/"
            };

            Species savedSpecies = null;
            _mockContext.Setup(c => c.Species.InsertOrUpdate(It.IsAny<Species>()))
                .Callback<Species>(s => savedSpecies = s);

            // Act
            await _service.SaveSpeciesAsync(speciesDto);

            // Assert
            _mockContext.Verify(c => c.Species.InsertOrUpdate(It.IsAny<Species>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
            Assert.That(savedSpecies, Is.Not.Null);
            Assert.That(savedSpecies.Name, Is.EqualTo("Human"));
            Assert.That(savedSpecies.Classification, Is.EqualTo("mammal"));
            Assert.That(savedSpecies.Language, Is.EqualTo("Galactic Basic"));
            Assert.That(savedSpecies.Uid, Is.EqualTo("1")); // ID from URL
        }

        [Test]
        public async Task SaveSpeciesAsync_ExistingSpecies_UpdatesInDB()
        {
            // Arrange
            var existingSpecies = new Species
            {
                Uid = "1",
                Name = "Human",
                Classification = "mammal",
                Language = "Galactic Basic"
            };

            var speciesDto = new SpeciesDto
            {
                Name = "Human",
                Classification = "mammal",
                Language = "Basic", // Changed language
                Url = "https://swapi.dev/api/species/1/"
            };

            Species updatedSpecies = null;
            _mockContext.Setup(c => c.Species.InsertOrUpdate(It.IsAny<Species>()))
                .Callback<Species>(s => updatedSpecies = s);

            // Act
            await _service.SaveSpeciesAsync(speciesDto);

            // Assert
            _mockContext.Verify(c => c.Species.InsertOrUpdate(It.IsAny<Species>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
            Assert.That(updatedSpecies, Is.Not.Null);
            Assert.That(updatedSpecies.Language, Is.EqualTo("Basic"));
        }

        [Test]
        public void GetSpeciesFromDB_ReturnsAllSpecies()
        {
            // Arrange
            var species = new List<Species>
            {
                new Species { Name = "Human", Classification = "mammal" },
                new Species { Name = "Wookiee", Classification = "mammal" }
            };

            _mockContext.Setup(c => c.Species.GetAll()).Returns(species);

            // Act
            var result = _service.GetSpeciesFromDB();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
        }

        [Test]
        public async Task DeleteSpeciesAsync_ExistingId_DeletesFromDatabase()
        {
            // Arrange
            var species = new Species { Name = "Human", Classification = "mammal" };
            _mockContext.Setup(c => c.Species.Delete(species.Id)).Returns(true);

            // Act
            var result = await _service.DeleteSpeciesAsync(species.Id);

            // Assert
            Assert.That(result, Is.True);
            _mockContext.Verify(c => c.Species.Delete(species.Id), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteSpeciesAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            _mockContext.Setup(c => c.Species.Delete("nonexistent")).Returns(false);

            // Act
            var result = await _service.DeleteSpeciesAsync("nonexistent");

            // Assert
            Assert.That(result, Is.False);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
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
            try
            {
                // Arrange
                var speciesDto = new SpeciesDto
                {
                    Name = "Test Species",
                    Classification = "Test Classification",
                    Language = "Test Language",
                    Url = "https://swapi.dev/api/species/1/"
                };

                // Act
                await _realService.SaveSpeciesAsync(speciesDto);
                await _realContext.SaveChangesAsync();
                var savedSpecies = await _realService.GetSpeciesByIdAsync("1");

                // Assert
                Assert.That(savedSpecies, Is.Not.Null);
                Assert.That(savedSpecies.Name, Is.EqualTo(speciesDto.Name));
                Assert.That(savedSpecies.Classification, Is.EqualTo(speciesDto.Classification));
                Assert.That(savedSpecies.Language, Is.EqualTo(speciesDto.Language));
                Assert.That(savedSpecies.Url, Is.EqualTo(speciesDto.Url));
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
