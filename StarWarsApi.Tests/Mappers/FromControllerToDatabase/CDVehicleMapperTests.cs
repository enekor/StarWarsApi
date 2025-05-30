using NUnit.Framework;
using StarWarsApi.Mappers.FromControllerToDatabase;
using StarWarsApi.Models.controller;
using StarWarsApi.Models.database;
using System.Collections.Generic;
using System.Linq;

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
        public void ToEntity_WhenDtoIsNull_ReturnsNull()
        {
            // Arrange
            VehicleDto? dto = null;

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ToEntity_WhenDtoIsValid_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new VehicleDto
            {
                Name = "Sand Crawler",
                Url = "https://swapi.dev/api/vehicles/1/"
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Url, Is.EqualTo(dto.Url));
        }

        [Test]
        public void ToEntityList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<VehicleDto>? dtos = null;

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var dtos = new List<VehicleDto>();

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ToEntityList_WhenListHasItems_ReturnsCorrectEntities()
        {
            // Arrange
            var dtos = new List<VehicleDto>
            {
                new VehicleDto
                {
                    Name = "Sand Crawler",
                    Url = "https://swapi.dev/api/vehicles/1/"
                },
                new VehicleDto
                {
                    Name = "T-16 skyhopper",
                    Url = "https://swapi.dev/api/vehicles/2/"
                }
            };

            // Act
            var result = _mapper.ToEntityList(dtos);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
            Assert.That(result[0].Url, Is.EqualTo("https://swapi.dev/api/vehicles/1/"));
            Assert.That(result[1].Name, Is.EqualTo("T-16 skyhopper"));
            Assert.That(result[1].Url, Is.EqualTo("https://swapi.dev/api/vehicles/2/"));
        }
    }
}
