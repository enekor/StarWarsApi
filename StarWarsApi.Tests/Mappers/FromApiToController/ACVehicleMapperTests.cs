using NUnit.Framework;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using StarWarsApi.Mappers.FromApiToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACVehicleMapperTests
    {
        private ACVehicleMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACVehicleMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = ACVehicleMapper.Instance;
            var instance2 = ACVehicleMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void MapToController_WithNullVehicleApi_ReturnsNull()
        {
            // Arrange
            VehicleApi? nullVehicle = null;

            // Act
            var result = _mapper.MapToController(nullVehicle);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithNullResult_ReturnsNull()
        {
            // Arrange
            var vehicleApi = new VehicleApi
            {
                result = null
            };

            // Act
            var result = _mapper.MapToController(vehicleApi);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WithValidVehicleApi_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var vehicleApi = new VehicleApi
            {
                result = new VehicleResult
                {
                    description = "Test Description",
                    properties = new VehicleProperties
                    {
                        created = created,
                        edited = edited,
                        consumables = "2 days",
                        name = "Sand Crawler",
                        cargo_capacity = "50000",
                        passengers = "30",
                        max_atmosphering_speed = "30",
                        crew = "46",
                        length = "36.8",
                        model = "Digger Crawler",
                        cost_in_credits = "150000",
                        manufacturer = "Corellia Mining Corporation",
                        vehicle_class = "wheeled",
                        url = "https://swapi.dev/api/vehicles/4"
                    }
                }
            };

            // Act
            var result = _mapper.MapToController(vehicleApi);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Consumables, Is.EqualTo("2 days"));
                Assert.That(result.Name, Is.EqualTo("Sand Crawler"));
                Assert.That(result.CargoCapacity, Is.EqualTo("50000"));
                Assert.That(result.Passengers, Is.EqualTo("30"));
                Assert.That(result.MaxAtmospheringSpeed, Is.EqualTo("30"));
                Assert.That(result.Crew, Is.EqualTo("46"));
                Assert.That(result.Length, Is.EqualTo("36.8"));
                Assert.That(result.Model, Is.EqualTo("Digger Crawler"));
                Assert.That(result.CostInCredits, Is.EqualTo("150000"));
                Assert.That(result.Manufacturer, Is.EqualTo("Corellia Mining Corporation"));
                Assert.That(result.VehicleClass, Is.EqualTo("wheeled"));
                Assert.That(result.Url, Is.EqualTo("https://swapi.dev/api/vehicles/4"));
            });
        }

        [Test]
        public void MapToControllerList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<VehicleApi>? nullList = null;

            // Act
            var result = _mapper.MapToControllerList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<VehicleApi>();

            // Act
            var result = _mapper.MapToControllerList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var vehicles = new List<VehicleApi>
            {
                new VehicleApi
                {
                    result = new VehicleResult
                    {
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
                        description = "Test Description 2",
                        properties = new VehicleProperties
                        {
                            name = "T-16 skyhopper",
                            model = "T-16 skyhopper"
                        }
                    }
                }
            };

            // Act
            var result = _mapper.MapToControllerList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
                Assert.That(result[0].Model, Is.EqualTo("Digger Crawler"));
                Assert.That(result[1].Name, Is.EqualTo("T-16 skyhopper"));
                Assert.That(result[1].Model, Is.EqualTo("T-16 skyhopper"));
            });
        }

        [Test]
        public void MapToControllerList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var vehicles = new List<VehicleApi>
            {
                new VehicleApi
                {
                    result = new VehicleResult
                    {
                        description = "Test Description",
                        properties = new VehicleProperties
                        {
                            name = "Sand Crawler",
                            model = "Digger Crawler"
                        }
                    }
                },
                null,
                new VehicleApi { result = null }
            };

            // Act
            var result = _mapper.MapToControllerList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
        }
    }
}