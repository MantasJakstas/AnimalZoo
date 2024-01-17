using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalZoo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalZoo.Repositories.AnimalsReposiotry;
using AnimalZoo.Repositories.EnclosureRepository;
using Moq;
using AnimalZoo.Models;

namespace AnimalZoo.Services.Tests
{


    [TestClass()]
    public class SortingServiceTests
    {
        private Mock<IAnimals> _animalsRepositoryMock;
        private Mock<IEnclosure> _enclosureRepositoryMock;
        private SortingService _sortingService;


        [TestInitialize]
        public void TestInitialize()
        {
            _animalsRepositoryMock = new Mock<IAnimals>();
            _enclosureRepositoryMock = new Mock<IEnclosure>();
            _sortingService = new SortingService(_animalsRepositoryMock.Object, _enclosureRepositoryMock.Object);
        }

        [TestMethod()]
        public void SortAnimals_NoAnimals_ThrowsException()
        {
            Assert.ThrowsException<Exception>(() => _sortingService.SortAnimals());
        }

        [TestMethod()]
        public void SortAnimals_NoEnclosure_ThrowsException()
        {
            var animals = new List<Animal> { new Animal { Food = "Herbivore" } };
            _animalsRepositoryMock.Setup(repo => repo.GetAllAnimals()).Returns(animals);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _sortingService.SortAnimals());
        }
    }
}