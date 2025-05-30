using NUnit.Framework;
using StarWarsApi.Mappers.FromApiToController;
using StarWarsApi.Models.api;
using StarWarsApi.Models.controller;

namespace StarWarsApi.Tests.Mappers.FromApiToController
{
    [TestFixture]
    public class ACCharacterMapperTests
    {
        private ACCharacterMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = ACCharacterMapper.Instance;
        }

        [Test]
        public void MapToController_WhenApiModelIsNull_ReturnsNull()
        {
            // Arrange
            CharacterApi? apiModel = null;

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void MapToController_WhenApiModelIsValid_ReturnsCorrectDto()
        {
            // Arrange
            var apiModel = new CharacterApi
            {
                Name = "Luke Skywalker",
                Url = "https://swapi.dev/api/people/1/"
            };

            // Act
            var result = _mapper.MapToController(apiModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(apiModel.Name));
            Assert.That(result.Url, Is.EqualTo(apiModel.Url));
        }
    }
}
