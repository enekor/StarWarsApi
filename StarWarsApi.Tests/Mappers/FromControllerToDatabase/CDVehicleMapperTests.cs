using NUnit.Framework;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using StarWarsApi.Mappers.FromControllerToDatabase;
using System;
using System.Collections.Generic;

namespace StarWarsApi.Tests.Mappers.FromControllerToDatabase
{
    [TestFixture]
    public class CDVehicleMapperTests
    {
        private CDVehicleMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = CDVehicleMapper.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CDVehicleMapper.Instance;
            var instance2 = CDVehicleMapper.Instance;

            // Assert
            Assert.That(instance2, Is.EqualTo(instance1));
            Assert.That(instance2, Is.SameAs(instance1));
        }

        [Test]
        public void ToEntity_WithNullInput_ReturnsNull()
        {
            // Arrange
            VehicleDto? nullVehicle = null;

            // Act
            var result = _mapper.ToEntity(nullVehicle);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var created = DateTime.Now;
            var edited = DateTime.Now.AddDays(1);

            var vehicleDto = new VehicleDto
            {
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
            var result = _mapper.ToEntity(vehicleDto);

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
        public void ToEntityList_WithNullInput_ReturnsEmptyList()
        {
            // Arrange
            List<VehicleDto>? nullList = null;

            // Act
            var result = _mapper.ToEntityList(nullList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithEmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<VehicleDto>();

            // Act
            var result = _mapper.ToEntityList(emptyList);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WithValidInput_ReturnsCorrectMapping()
        {
            // Arrange
            var vehicles = new List<VehicleDto>
            {
                new VehicleDto
                {
                    Description = "Test Description 1",
                    Name = "Sand Crawler",
                    Model = "Digger Crawler",
                    VehicleClass = "wheeled"
                },
                new VehicleDto
                {
                    Description = "Test Description 2",
                    Name = "T-16 skyhopper",
                    Model = "T-16 skyhopper",
                    VehicleClass = "repulsorcraft"
                }
            };

            // Act
            var result = _mapper.ToEntityList(vehicles);

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
        public void ToEntityList_WithSomeNullElements_ReturnsOnlyValidMappings()
        {
            // Arrange
            var vehicles = new List<VehicleDto>
            {
                new VehicleDto
                {
                    Description = "Test Description",
                    Name = "Sand Crawler",
                    Model = "Digger Crawler"
                },
                null
            };

            // Act
            var result = _mapper.ToEntityList(vehicles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
        }
    }
}