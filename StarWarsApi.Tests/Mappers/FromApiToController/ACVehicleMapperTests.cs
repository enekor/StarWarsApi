using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using System.Collections.Generic;
using System.Linq;

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
        public void MapToController_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            VehicleApi? apiModel = null;

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WhenApiModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var apiModel = new VehicleApi
            {
                Name = "Sand Crawler",
                Url = "https://swapi.dev/api/vehicles/1/"
            };

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(apiModel.Name));
            Assert.That(result.Url, Is.EqualTo(apiModel.Url));
        }

        [Test]
        public void MapToControllerList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<VehicleApi>? apiModels = null;

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<VehicleApi>();

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var apiModels = new List<VehicleApi>
            {
                new VehicleApi { Name = "Sand Crawler", Url = "https://swapi.dev/api/vehicles/1/" },
                new VehicleApi { Name = "T-16 skyhopper", Url = "https://swapi.dev/api/vehicles/2/" }
            };

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Sand Crawler"));
            Assert.That(result[1].Name, Is.EqualTo("T-16 skyhopper"));
        }
    }
}
