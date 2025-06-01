using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromDatabaseToController;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromDatabaseToController
{
    [TestFixture]
    public class DCVehicleMapperTests
    {
        private DCVehicleMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = DCVehicleMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = DCVehicleMapper.Instance;
            var instance2 = DCVehicleMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToDto_WithNullInput_ReturnsNull()
        {
            // Arrange
            Vehicle? nullVehicle = null;

            // Act
            var result = _mapper.ToDto(nullVehicle);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToDto_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var vehicle = new Vehicle
            {
                Uid = "4",
                Description = "Test Description",
                Created = created,
                Edited = edited,
                Consumables = "2 months",
                Name = "Sand Crawler",
                CargoCapacity = "50000",
                Passengers = "30",
                MaxAtmospheringSpeed = "30",
                Crew = "46",
                Length = "36.8",
                Model = "Digger Crawler",
                CostInCredits = "150000",
                Manufacturer = "Corellia Mining Corporation",
                VehicleClass = "wheeled",
                Url = "https://swapi.dev/api/vehicles/4"
            };

            // Act
            var result = _mapper.ToDto(vehicle);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Description, Is.EqualTo("Test Description"));
                Assert.That(result.Created, Is.EqualTo(created));
                Assert.That(result.Edited, Is.EqualTo(edited));
                Assert.That(result.Consumables, Is.EqualTo("2 months"));
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
        public void ToDtoList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<Vehicle>? nullList = null;

            // Act
            var result = _mapper.ToDtoList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Vehicle>();

            // Act
            var result = _mapper.ToDtoList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToDtoList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Uid = "4",
                    Description = "Test Description 1",
                    Name = "Sand Crawler",
                    Model = "Digger Crawler",
                    VehicleClass = "wheeled"
                },
                new Vehicle
                {
                    Uid = "6",
                    Description = "Test Description 2",
                    Name = "T-16 skyhopper",
                    Model = "T-16 skyhopper",
                    VehicleClass = "repulsorcraft"
                }
            };

            // Act
            var result = _mapper.ToDtoList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
                Assert.That(result[0].Model, Is.EqualTo("Digger Crawler"));
                Assert.That(result[0].VehicleClass, Is.EqualTo("wheeled"));
                Assert.That(result[1].Name, Is.EqualTo("T-16 skyhopper"));
                Assert.That(result[1].Model, Is.EqualTo("T-16 skyhopper"));
                Assert.That(result[1].VehicleClass, Is.EqualTo("repulsorcraft"));
            });
        }

        [Test]
        public void ToDtoList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                null,
                new Vehicle
                {
                    Uid = "4",
                    Description = "Test Description",
                    Name = "Sand Crawler",
                    Model = "Digger Crawler"
                }
            };

            // Act
            var result = _mapper.ToDtoList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
        }
    }
}