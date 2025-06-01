using BDCADAO.BDModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Services;
using System.Net;
using System.Text.Json;

namespace StarWarsApi.Tests.Services
{
    [TestFixture]
    public class StarshipServiceTests
    {
    private StarshipService _service;
    private StarshipService _realService;
    private Mock<HttpMessageHandler> _mockHttpHandler;
    private HttpClient _httpClient;
    private ModelContext _context;
    private ModelContext _realContext;

    [SetUp]        public void Setup()
        {
            // Setup mock HTTP handler for unit tests
            _mockHttpHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpHandler.Object);
            
            // Setup in-memory database for unit tests
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: $"StarWarsDb_{Guid.NewGuid()}")
                .Options;
            _context = new ModelContext(options);
            _service = new StarshipService(_httpClient, _context);

            // Setup for real integration tests
            var realHttpClient = new HttpClient();
            var realOptions = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: "TestStarWarsStarshipDb")
                .Options;
            _realContext = new ModelContext(realOptions);
            _realService = new StarshipService(realHttpClient, _realContext);
        }

        [Test]
        public async Task GetAllStarshipsAsync_WhenApiCallFails_ReturnsEmptyList()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Server Error")
            };

            _mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetAllStarshipsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllStarshipsAsync_ReturnsStarships()
        {
            // Arrange
            var mockResponse = new List<StarshipApi>
            {
                new StarshipApi { Name = "X-wing", Model = "T-65" },
                new StarshipApi { Name = "TIE Fighter", Model = "Twin Ion Engine" }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(mockResponse))
            };

            _mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetAllStarshipsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("X-wing"));
            Assert.That(result[1].Name, Is.EqualTo("TIE Fighter"));
        }        [Test]
        public async Task GetStarshipByIdAsync_ExistingId_ReturnsStarship()
        {
            // Arrange
            var starshipId = "1";
            var mockResponse = new StarshipApi 
            {
                Name = "X-wing",
                Model = "T-65",
                url = $"https://swapi.dev/api/starships/{starshipId}"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(mockResponse))
            };

            _mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetStarshipByIdAsync(starshipId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("X-wing"));
            Assert.That(result.Model, Is.EqualTo("T-65"));
        }        [Test]
        public async Task GetStarshipByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var starshipId = "999";
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Not Found")
            };

            _mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetStarshipByIdAsync(starshipId);

            // Assert
            Assert.That(result, Is.Null);
        }        [Test]
        public async Task SaveStarshipAsync_NewStarship_SavesToDatabase()
        {
            // Arrange
            var starshipDto = new StarshipDto
            {
                Name = "X-wing",
                Model = "T-65",
                Url = "https://swapi.dev/api/starships/12/"
            };

            // Act
            await _service.SaveStarshipAsync(starshipDto);

            // Assert
            var savedStarship = await _context.Starships.FirstOrDefaultAsync(s => s.Name == "X-wing");
            Assert.That(savedStarship, Is.Not.Null);
            Assert.That(savedStarship.Model, Is.EqualTo("T-65"));
            Assert.That(savedStarship.Uid, Is.EqualTo("12")); // ID from URL
        }

        [Test]
        public async Task SaveStarshipAsync_ExistingStarship_UpdatesDatabase()
        {
            // Arrange
            var starship = new Starship { Name = "X-wing", Model = "T-65" };
            await _context.Starships.AddAsync(starship);
            await _context.SaveChangesAsync();

            var starshipDto = new StarshipDto
            {
                Name = "X-wing",
                Model = "T-70",
                Url = $"https://swapi.dev/api/starships/{starship.Uid}"
            };

            // Act
            await _service.SaveStarshipAsync(starshipDto);

            // Assert
            var updatedStarship = await _context.Starships.FirstOrDefaultAsync(s => s.Uid == starship.Uid);
            Assert.That(updatedStarship, Is.Not.Null);
            Assert.That(updatedStarship.Model, Is.EqualTo("T-70"));
        }

        [Test]
        public async Task DeleteStarshipAsync_ExistingId_DeletesFromDatabase()
        {
            // Arrange
            var starship = new Starship { Name = "X-wing", Model = "T-65" };
            await _context.Starships.AddAsync(starship);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteStarshipAsync(starship.Id);

            // Assert
            Assert.That(result, Is.True);
            var deletedStarship = await _context.Starships.FindAsync(starship.Id);
            Assert.That(deletedStarship, Is.Null);
        }

        [Test]
        public async Task DeleteStarshipAsync_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = await _service.DeleteStarshipAsync("nonexistent");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RealIntegration_GetAllStarshipsAsync_ShouldReturnStarships()
        {
            try
            {
                // Act
                var result = await _realService.GetAllStarshipsAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result[0].Name, Is.Not.Empty);
                Assert.That(result[0].Url, Does.StartWith("https://swapi.dev/api/starships/"));
            }
            catch (HttpRequestException ex)
            {
                Assert.Inconclusive($"Test requires internet connection and API availability. Error: {ex.Message}");
            }
        }

        [Test]
        public async Task RealIntegration_SaveAndGetStarshipById_ShouldWorkCorrectly()
        {
            // Arrange
            var starshipDto = new StarshipDto
            {
                Name = "Test Starship",
                Model = "Test Model",
                Url = "https://swapi.dev/api/starships/1/"
            };

            // Act
            await _realService.SaveStarshipAsync(starshipDto);
            await _realContext.SaveChangesAsync();
            var savedStarship = await _realService.GetStarshipByIdAsync("1");

            // Assert
            Assert.That(savedStarship, Is.Not.Null);
            Assert.That(savedStarship.Name, Is.EqualTo(starshipDto.Name));
            Assert.That(savedStarship.Model, Is.EqualTo(starshipDto.Model));
            Assert.That(savedStarship.Url, Is.EqualTo(starshipDto.Url));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _httpClient.Dispose();
            if (_realContext != null)
            {
                _realContext.Database.EnsureDeleted();
                _realContext.Dispose();
            }
        }
    }
}
