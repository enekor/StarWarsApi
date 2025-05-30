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
    public class FilmsServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private FilmsService _service;

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new FilmsService(_httpClient, _mockContext.Object);
        }

        [Test]
        public async Task GetAllFilmsAsync_WhenApiCallSucceeds_ReturnsFilmsList()
        {
            // Arrange
            var films = new List<FilmsApi>
            {
                new FilmsApi
                {
                    properties = new FilmsApi.Properties
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
                Id = filmId,
                Title = "A New Hope"
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
    }
}
