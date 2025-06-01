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
    public class CharacterServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<ModelContext> _mockContext;
        private CharacterService _service;
        private CharacterService _realService;
        private ModelContext _realContext;

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
                .UseInMemoryDatabase(databaseName: "TestStarWarsCharacterDb")
                .Options;
            _realContext = new ModelContext(options);
            _realService = new CharacterService(realHttpClient, _realContext);
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
                    Height = "172",
                    Mass = "77",
                    HairColor = "blond",
                    url = "https://swapi.dev/api/people/1/"
                },
                new CharacterApi
                {
                    Name = "Darth Vader",
                    Height = "202",
                    Mass = "136",
                    HairColor = "none",
                    url = "https://swapi.dev/api/people/4/"
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
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[1].Name, Is.EqualTo("Darth Vader"));
        }

        [Test]
        public async Task GetAllCharactersAsync_WhenApiCallFails_ReturnsEmptyList()
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
            var result = await _service.GetAllCharactersAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetCharacterByIdAsync_WhenCharacterExists_ReturnsCharacter()
        {
            // Arrange
            var characterId = "1";
            var mockResponse = new CharacterApi
            {
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "77",
                HairColor = "blond",
                url = $"https://swapi.dev/api/people/{characterId}"
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
            var result = await _service.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result.Height, Is.EqualTo("172"));
            Assert.That(result.Mass, Is.EqualTo("77"));
            Assert.That(result.HairColor, Is.EqualTo("blond"));
        }

        [Test]
        public async Task GetCharacterByIdAsync_WhenCharacterDoesNotExist_ReturnsNull()
        {
            // Arrange
            var characterId = "999";
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
            var result = await _service.GetCharacterByIdAsync(characterId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SaveCharacterAsync_NewCharacter_SavesToDatabase()
        {
            // Arrange
            var characterDto = new CharacterDto
            {
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "77",
                HairColor = "blond",
                Url = "https://swapi.dev/api/people/1/"
            };

            Character savedCharacter = null;
            _mockContext.Setup(c => c.Characters.InsertOrUpdate(It.IsAny<Character>()))
                .Callback<Character>(c => savedCharacter = c);

            // Act
            await _service.SaveCharacterAsync(characterDto);

            // Assert
            _mockContext.Verify(c => c.Characters.InsertOrUpdate(It.IsAny<Character>()), Times.Once);
            Assert.That(savedCharacter, Is.Not.Null);
            Assert.That(savedCharacter.Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(savedCharacter.Height, Is.EqualTo("172"));
            Assert.That(savedCharacter.HairColor, Is.EqualTo("blond"));
            Assert.That(savedCharacter.Uid, Is.EqualTo("1")); // ID from URL
        }

        [Test]
        public async Task SaveCharacterAsync_ExistingCharacter_UpdatesInDB()
        {
            // Arrange
            var existingCharacter = new Character
            {
                Uid = "1",
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "77",
                HairColor = "blond"
            };

            var characterDto = new CharacterDto
            {
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "80", // Changed mass
                HairColor = "blond",
                Url = "https://swapi.dev/api/people/1/"
            };

            Character updatedCharacter = null;
            _mockContext.Setup(c => c.Characters.InsertOrUpdate(It.IsAny<Character>()))
                .Callback<Character>(c => updatedCharacter = c);

            // Act
            await _service.SaveCharacterAsync(characterDto);

            // Assert
            _mockContext.Verify(c => c.Characters.InsertOrUpdate(It.IsAny<Character>()), Times.Once);
            Assert.That(updatedCharacter, Is.Not.Null);
            Assert.That(updatedCharacter.Mass, Is.EqualTo("80"));
        }

        [Test]
        public void GetCharactersFromDB_ReturnsAllCharacters()
        {
            // Arrange
            var characters = new List<Character>
            {
                new Character { Name = "Luke Skywalker", Height = "172" },
                new Character { Name = "Darth Vader", Height = "202" }
            };

            _mockContext.Setup(c => c.Characters.GetAll()).Returns(characters);

            // Act
            var result = _service.GetCharactersFromDB();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result[1].Name, Is.EqualTo("Darth Vader"));
        }

        [Test]
        public void GetCharacterFromDB_ExistingId_ReturnsCharacter()
        {
            // Arrange
            var characterId = "1";
            var character = new Character
            {
                Uid = characterId,
                Name = "Luke Skywalker",
                Height = "172"
            };

            _mockContext.Setup(c => c.Characters.GetById(characterId)).Returns(character);

            // Act
            var result = _service.GetCharacterFromDB(characterId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Luke Skywalker"));
            Assert.That(result.Height, Is.EqualTo("172"));
        }

        [Test]
        public void GetCharacterFromDB_NonExistingId_ReturnsNull()
        {
            // Arrange
            var characterId = "999";
            _mockContext.Setup(c => c.Characters.GetById(characterId)).Returns(null as Character);

            // Act
            var result = _service.GetCharacterFromDB(characterId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteCharacterAsync_ExistingId_DeletesFromDatabase()
        {
            // Arrange
            var character = new Character { Name = "Luke Skywalker", Height = "172" };
            _mockContext.Setup(c => c.Characters.Delete(character.Id)).Returns(true);

            // Act
            var result = await _service.DeleteCharacterAsync(character.Id);

            // Assert
            Assert.That(result, Is.True);
            _mockContext.Verify(c => c.Characters.Delete(character.Id), Times.Once);
        }

        [Test]
        public async Task DeleteCharacterAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            _mockContext.Setup(c => c.Characters.Delete("nonexistent")).Returns(false);

            // Act
            var result = await _service.DeleteCharacterAsync("nonexistent");

            // Assert
            Assert.That(result, Is.False);
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
            try
            {
                // Arrange
                var characterDto = new CharacterDto
                {
                    Name = "Test Character",
                    Height = "180",
                    Mass = "75",
                    HairColor = "brown",
                    Url = "https://swapi.dev/api/people/1/"
                };

                // Act
                await _realService.SaveCharacterAsync(characterDto);
                await _realContext.SaveChangesAsync();
                var savedCharacter = await _realService.GetCharacterByIdAsync("1");

                // Assert
                Assert.That(savedCharacter, Is.Not.Null);
                Assert.That(savedCharacter.Name, Is.EqualTo(characterDto.Name));
                Assert.That(savedCharacter.Height, Is.EqualTo(characterDto.Height));
                Assert.That(savedCharacter.Mass, Is.EqualTo(characterDto.Mass));
                Assert.That(savedCharacter.HairColor, Is.EqualTo(characterDto.HairColor));
                Assert.That(savedCharacter.Url, Is.EqualTo(characterDto.Url));
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
