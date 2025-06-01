using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromApiToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToDatabase
{
    [TestFixture]
    public class ADVehicleMapperTests
    {
        private ADVehicleMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ADVehicleMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ADVehicleMapper.Instance;
            var instance2 = ADVehicleMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToDatabase_WithNullInput_ReturnsNull()
        {
            // Arrange
            VehicleApi? nullVehicle = null;

            // Act
            var result = _mapper.MapToDatabase(nullVehicle);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToDatabase_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var vehicleApi = new VehicleApi
            {
                result = new VehicleResult
                {
                    uid = "4",
                    description = "Test Description",
                    properties = new VehicleProperties
                    {
                        created = created,
                        edited = edited,
                        name = "Sand Crawler",
                        model = "Digger Crawler",
                        cargo_capacity = "50000",
                        consumables = "2 months",
                        cost_in_credits = "150000",
                        crew = "46",
                        passengers = "30",
                        max_atmosphering_speed = "30",
                        length = "36.8",
                        manufacturer = "Corellia Mining Corporation",
                        url = "https://swapi.dev/api/vehicles/4",
                        vehicle_class = "wheeled"
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabase(vehicleApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Uid, Is.EqualTo("4"));
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Name, Is.EqualTo("Sand Crawler"));
                Assert.That(result.Model, Is.EqualTo("Digger Crawler"));
                Assert.That(result.CargoCapacity, Is.EqualTo("50000"));
                Assert.That(result.Consumables, Is.EqualTo("2 months"));
                Assert.That(result.CostInCredits, Is.EqualTo("150000"));
                Assert.That(result.Crew, Is.EqualTo("46"));
                Assert.That(result.Passengers, Is.EqualTo("30"));
                Assert.That(result.MaxAtmospheringSpeed, Is.EqualTo("30"));
                Assert.That(result.Length, Is.EqualTo("36.8"));
                Assert.That(result.Manufacturer, Is.EqualTo("Corellia Mining Corporation"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/vehicles/4"));
                Assert.That(result.VehicleClass, Is.EqualTo("wheeled"));
            });
        }

        [Test]
        public void MapToDatabaseList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<VehicleApi>? nullList = null;

            // Act
            var result = _mapper.MapToDatabaseList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<VehicleApi>();

            // Act
            var result = _mapper.MapToDatabaseList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToDatabaseList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var vehicles = new List<VehicleApi>
            {
                new VehicleApi
                {
                    result = new VehicleResult
                    {
                        uid = "4",
                        description = "Test Description 1",
                        properties = new VehicleProperties
                        {
                            name = "Sand Crawler",
                            model = "Digger Crawler"
                        }
                    }
                },
                new VehicleApi
                {
                    result = new VehicleResult
                    {
                        uid = "14",
                        description = "Test Description 2",
                        properties = new VehicleProperties
                        {
                            name = "Snowspeeder",
                            model = "t-47 airspeeder"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToDatabaseList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
                Assert.That(result[0].Model, Is.EqualTo("Digger Crawler"));
                Assert.That(result[0].Uid, Is.EqualTo("4"));
                Assert.That(result[1].Name, Is.EqualTo("Snowspeeder"));
                Assert.That(result[1].Model, Is.EqualTo("t-47 airspeeder"));
                Assert.That(result[1].Uid, Is.EqualTo("14"));
            });
        }
    }
}