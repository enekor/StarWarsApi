using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACSpecieMapperTests
    {
        private ACSpecieMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACSpecieMapper.Instance;
        }

        [Test]
        public void MapToController_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            SpeciesApi? apiModel = null;

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WhenApiModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var apiModel = new SpeciesApi
            {
                Name = "Human",
                Url = "https://swapi.dev/api/species/1/"
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
            List<SpeciesApi>? apiModels = null;

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var apiModels = new List<SpeciesApi>();

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void MapToControllerList_WhenListHasItems_ReturnsCorrectDtos()
        {
            // Arrange
            var apiModels = new List<SpeciesApi>
            {
                new SpeciesApi { Name = "Human", Url = "https://swapi.dev/api/species/1/" },
                new SpeciesApi { Name = "Wookiee", Url = "https://swapi.dev/api/species/2/" }
            };

            // Act
            var result = _mapper.MapToControllerList(apiModels);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Human"));
            Assert.That(result[1].Name, Is.EqualTo("Wookiee"));
        }
    }
}
