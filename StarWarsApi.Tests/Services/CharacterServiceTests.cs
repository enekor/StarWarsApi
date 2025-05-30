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

namespace StarWarsApi.Tests.Services
{
    [TestFixture]
    public class CharacterServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private CharacterService _service;
        private CharacterService _realService;

        [SetUp]
        public void Setup()
        {
            // Setup for mocked tests
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockContext = new Mock<ModelContext>();
            _service = new CharacterService(_httpClient, _mockContext.Object);

            // Setup for real integration test
            var realHttpClient = new HttpClient();
            var options = new DbContextOptionsBuilder<ModelContext>()
                .UseInMemoryDatabase(databaseName: "TestStarWarsDb")
                .Options;
            var realContext = new ModelContext(options);
            _realService = new CharacterService(realHttpClient, realContext);
        }

        [Test]
        public async Task GetAllCharactersAsync_WhenApiCallSucceeds_ReturnsCharactersList()
        {
            // Arrange
            var characters = new List<CharacterApi>
            {
                new CharacterApi
                {
                    Name = "Luke Skywalker",
                    Url = "https://swapi.dev/api/people/1/"
                }
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(characters)
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
            var result = await _service.GetAllCharactersAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
        }

        [Test]
        public async Task GetCharacterByIdAsync_WhenCharacterExists_ReturnsCharacter()
        {
            // Arrange
            var characterId = "1";
            var character = new Character
            {
                Uid = characterId,
                Name = "Luke Skywalker",
                Url = "https://swapi.dev/api/people/1/"
            };

            var mockSet = new Mock<DbSet<Character>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(character);

            _mockContext.Setup(c => c.Characters).Returns(mockSet.Object);

            // Act
            var result = await _service.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Luke Skywalker"));
        }

        [Test]
        public async Task GetCharacterByIdAsync_WhenCharacterDoesNotExist_ReturnsNull()
        {
            // Arrange
            var characterId = "999";
            
            var mockSet = new Mock<DbSet<Character>>();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((Character)null);

            _mockContext.Setup(c => c.Characters).Returns(mockSet.Object);

            // Act
            var result = await _service.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SaveCharacterAsync_ShouldSaveCharacter()
        {
            // Arrange
            var characterDto = new CharacterDto
            {
                Name = "Luke Skywalker",
                Url = "https://swapi.dev/api/people/1/"
            };

            var savedCharacter = new Character();
            _mockContext.Setup(c => c.Characters.Add(It.IsAny<Character>())).Callback<Character>(c => savedCharacter = c);

            // Act
            _service.SaveCharacterAsync(characterDto);
            await _mockContext.Object.SaveChangesAsync();

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
            Assert.That(savedCharacter.Name, Is.EqualTo(characterDto.Name));
            Assert.That(savedCharacter.Url, Is.EqualTo(characterDto.Url));
        }

        [Test]
        public async Task RealIntegration_GetAllCharactersAsync_ShouldReturnCharacters()
        {
            try
            {
                // Act
                var result = await _realService.GetAllCharactersAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result[0].Name, Is.Not.Empty);
                Assert.That(result[0].Url, Does.StartWith("https://swapi.dev/api/people/"));
            }
            catch (HttpRequestException ex)
            {
                Assert.Inconclusive($"Test requires internet connection and API availability. Error: {ex.Message}");
            }
        }

        [Test]
        public async Task RealIntegration_SaveAndGetCharacterById_ShouldWorkCorrectly()
        {
            // Arrange
            var characterDto = new CharacterDto
            {
                Name = "Test Character",
                Url = "https://swapi.dev/api/people/1/"
            };

            // Act
            _realService.SaveCharacterAsync(characterDto);
            var result = await _realService.GetCharacterByIdAsync("1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(characterDto.Name));
            Assert.That(result.Url, Is.EqualTo(characterDto.Url));
        }
    }
}
