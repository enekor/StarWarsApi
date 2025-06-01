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
    public class VehicleServiceTests
    {
    private VehicleService _service;
    private VehicleService _realService;
    private Mock<HttpMessageHandler> _mockHttpHandler;
    private HttpClient _httpClient;
    private ModelContext _context;
    private ModelContext _realContext;

    [SetUp]
    public void Setup()
    {
        // Setup mock HTTP handler for unit tests
        _mockHttpHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpHandler.Object);
        
        // Setup in-memory database for unit tests
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(databaseName: $"StarWarsDb_{Guid.NewGuid()}")
            .Options;
        _context = new ModelContext(options);
        _service = new VehicleService(_httpClient, _context);

        // Setup for real integration tests
        var realHttpClient = new HttpClient();
        var realOptions = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(databaseName: "TestStarWarsVehicleDb")
            .Options;
        _realContext = new ModelContext(realOptions);
        _realService = new VehicleService(realHttpClient, _realContext);
        }

        [Test]
        public async Task GetAllVehiclesAsync_ReturnsVehicles()
        {
            // Arrange
            var mockResponse = new List<VehicleApi>
            {
                new VehicleApi { Name = "Snowspeeder", Model = "T-47 airspeeder" },
                new VehicleApi { Name = "AT-AT", Model = "All Terrain Armored Transport" }
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
            var result = await _service.GetAllVehiclesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Snowspeeder"));
            Assert.That(result[1].Name, Is.EqualTo("AT-AT"));
        }        [Test]
        public async Task GetVehicleByIdAsync_ExistingId_ReturnsVehicle()
        {
            // Arrange
            var vehicleId = "14";
            var mockResponse = new VehicleApi 
            {
                Name = "Snowspeeder",
                Model = "t-47 airspeeder",
                url = $"https://swapi.dev/api/vehicles/{vehicleId}"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(mockResponse)
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
            var result = await _service.GetVehicleByIdAsync(vehicleId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Snowspeeder"));
            Assert.That(result.Model, Is.EqualTo("t-47 airspeeder"));
            Assert.That(result.Url, Is.EqualTo($"https://swapi.dev/api/vehicles/{vehicleId}"));
        }

        [Test]
        public async Task GetVehicleByIdAsync_NonExistingId_ReturnsNull()
        {
            // Act
            var result = await _service.GetVehicleByIdAsync("nonexistent");

            // Assert
            Assert.That(result, Is.Null);
        }        [Test]
        public async Task SaveVehicleAsync_NewVehicle_SavesToDatabase()
        {
            // Arrange
            var vehicleDto = new VehicleDto
            {
                Name = "Snowspeeder",
                Model = "t-47 airspeeder",
                Url = "https://swapi.dev/api/vehicles/14/"
            };

            // Act
            await _service.SaveVehicleAsync(vehicleDto);

            // Assert
            var savedVehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Name == "Snowspeeder");
            Assert.That(savedVehicle, Is.Not.Null);
            Assert.That(savedVehicle.Model, Is.EqualTo("t-47 airspeeder"));
            Assert.That(savedVehicle.Uid, Is.EqualTo("14")); // ID from URL
        }

        [Test]
        public async Task SaveVehicleAsync_ExistingVehicle_UpdatesDatabase()
        {
            // Arrange
            var vehicle = new Vehicle { Name = "Snowspeeder", Model = "t-47 airspeeder" };
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            var vehicleDto = new VehicleDto
            {
                Name = "Snowspeeder",
                Model = "t-48 airspeeder", // Updated model
                Url = $"https://swapi.dev/api/vehicles/{vehicle.Uid}"
            };

            // Act
            await _service.SaveVehicleAsync(vehicleDto);

            // Assert
            var updatedVehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Uid == vehicle.Uid);
            Assert.That(updatedVehicle, Is.Not.Null);
            Assert.That(updatedVehicle.Model, Is.EqualTo("t-48 airspeeder"));
        }

        [Test]
        public async Task DeleteVehicleAsync_ExistingId_DeletesFromDatabase()
        {
            // Arrange
            var vehicle = new Vehicle { Name = "Snowspeeder", Model = "T-47 airspeeder" };
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteVehicleAsync(vehicle.Id);

            // Assert
            Assert.That(result, Is.True);
            var deletedVehicle = await _context.Vehicles.FindAsync(vehicle.Id);
            Assert.That(deletedVehicle, Is.Null);
        }

        [Test]
        public async Task DeleteVehicleAsync_NonExistingId_ReturnsFalse()
        {
            // Act
            var result = await _service.DeleteVehicleAsync("nonexistent");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RealIntegration_GetAllVehiclesAsync_ShouldReturnVehicles()
        {
            try
            {
                // Act
                var result = await _realService.GetAllVehiclesAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result[0].Name, Is.Not.Empty);
                Assert.That(result[0].Url, Does.StartWith("https://swapi.dev/api/vehicles/"));
            }
            catch (HttpRequestException ex)
            {
                Assert.Inconclusive($"Test requires internet connection and API availability. Error: {ex.Message}");
            }
        }

        [Test]
        public async Task RealIntegration_SaveAndGetVehicleById_ShouldWorkCorrectly()
        {
            // Arrange
            var vehicleDto = new VehicleDto
            {
                Name = "Test Vehicle",
                Model = "Test Model",
                Url = "https://swapi.dev/api/vehicles/1/"
            };

            // Act
            await _realService.SaveVehicleAsync(vehicleDto);
            await _realContext.SaveChangesAsync();
            var savedVehicle = await _realService.GetVehicleByIdAsync("1");

            // Assert
            Assert.That(savedVehicle, Is.Not.Null);
            Assert.That(savedVehicle.Name, Is.EqualTo(vehicleDto.Name));
            Assert.That(savedVehicle.Model, Is.EqualTo(vehicleDto.Model));
            Assert.That(savedVehicle.Url, Is.EqualTo(vehicleDto.Url));
        }

        [TearDown]        [Test]
        public void GetVehiclesFromDB_ReturnsAllVehicles()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Name = "Snowspeeder", Model = "t-47 airspeeder" },
                new Vehicle { Name = "AT-AT", Model = "All Terrain Armored Transport" }
            };
            _context.Vehicles.AddRange(vehicles);
            _context.SaveChanges();

            // Act
            var result = _service.GetVehiclesFromDB();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Snowspeeder"));
            Assert.That(result[1].Name, Is.EqualTo("AT-AT"));
        }

        [Test]
        public void GetVehicleFromDB_ExistingId_ReturnsVehicle()
        {
            // Arrange
            var vehicle = new Vehicle { Name = "Snowspeeder", Model = "t-47 airspeeder" };
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            // Act
            var result = _service.GetVehicleFromDB(vehicle.Uid);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Snowspeeder"));
            Assert.That(result.Model, Is.EqualTo("t-47 airspeeder"));
        }

        [Test]
        public void GetVehicleFromDB_NonExistingId_ReturnsNull()
        {
            // Act
            var result = _service.GetVehicleFromDB("nonexistent");

            // Assert
            Assert.That(result, Is.Null);
        }

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
