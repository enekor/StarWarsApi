using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACPlanetMapperTests
    {
        private ACPlanetMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACPlanetMapper.Instance;
        }

        [Test]
        public void MapToController_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            PlanetApi? apiModel = null;

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WhenApiModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var apiModel = new PlanetApi
            {
                Name = "Tatooine",
                url = "https://swapi.dev/api/planets/1/"
            };

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(apiModel.Name));
            Assert.That(result.url, Is.EqualTo(apiModel.url));
        }

        [Test]
        public void MapToControllerList_WhenListIsNull_ReturnsEmptyList()
        {
            // Arrange
            List<PlanetApi>? apiModels = null;

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<PlanetApi>();

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var apiModels = new List<PlanetApi>
            {
                new PlanetApi { Name = "Tatooine", url = "https://swapi.dev/api/planets/1/" },
                new PlanetApi { Name = "Alderaan", url = "https://swapi.dev/api/planets/2/" }
            };

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tatooine"));
            Assert.That(result[1].Name, Is.EqualTo("Alderaan"));
        }
    }
}
