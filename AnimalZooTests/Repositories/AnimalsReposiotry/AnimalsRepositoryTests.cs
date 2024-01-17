using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalZoo.Repositories.AnimalsReposiotry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalZoo.Database;
using Microsoft.EntityFrameworkCore;
using AnimalZoo.Models;

namespace AnimalZoo.Repositories.AnimalsReposiotry.Tests
{
    [TestClass()]
    public class AnimalsRepositoryTests
    {
        private ZooDbContext _context;
        private IAnimals _animalsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ZooDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ZooDbContext(options);
            _animalsRepository = new AnimalsRepository(_context);
        }

        [TestMethod()]
        public void AddAnimalTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var animal = new Animal { Id = id, Species = "Lion", Food = "Carnivore" };

            // Act
            _animalsRepository.AddAnimal(animal);

            // Assert
            var retrievedAnimal = _context.Animals.Find(id);
            Assert.IsNotNull(retrievedAnimal);
            Assert.AreEqual("Lion", retrievedAnimal.Species);
            Assert.AreEqual("Carnivore", retrievedAnimal.Food);
        }

        [TestMethod()]
        public void AddAnimalRangeTest()
        {
            var animals = new List<Animal>
            {
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Lion", Food = "Carnivore" },
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Elephant", Food = "Herbivore" },
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Tiger", Food = "Carnivore" },
            };

            // Act
            _animalsRepository.AddAnimalRange(animals);

            // Assert
            var retrievedAnimals = _context.Animals.ToList();
            CollectionAssert.AreEquivalent(animals, retrievedAnimals);
        }

        [TestMethod()]
        public void DeleteAnimalTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var animal = new Animal { Id = id, Species = "Lion", Food = "Carnivore" };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            // Act
            _animalsRepository.DeleteAnimal(id);

            // Assert
            var retrievedAnimal = _context.Animals.Find(id);
            Assert.IsNull(retrievedAnimal);
        }

        [TestMethod()]
        public void GetAllAnimalsTest()
        {
            var animals = new List<Animal>
            {
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Lion", Food = "Carnivore" },
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Elephant", Food = "Herbivore" },
                new Animal { Id = Guid.NewGuid().GetHashCode(), Species = "Tiger", Food = "Carnivore" },
            };

            // Act
            _context.AddRange(animals);
            _context.SaveChanges();
            // Assert
            var retrievedAnimals = _animalsRepository.GetAllAnimals();
            CollectionAssert.AreEquivalent(animals, (System.Collections.ICollection?)retrievedAnimals);
        }

        [TestMethod()]
        public void GetAnimalByIdTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var animal = new Animal { Id = id, Species = "Lion", Food = "Carnivore" };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            // Act
            var retrievedAnimal = _animalsRepository.GetAnimalById(id);

            // Assert
            Assert.IsNotNull(retrievedAnimal);
            Assert.AreEqual("Lion", retrievedAnimal.Species);
            Assert.AreEqual("Carnivore", retrievedAnimal.Food);
        }

        [TestMethod()]
        public void UpdateAnimalTest()
        {

            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var originalAnimal = new Animal { Id = id, Species = "Lion", Food = "Carnivore" };
            _context.Animals.Add(originalAnimal);
            _context.SaveChanges();

            originalAnimal.Species = "Lion King";
            originalAnimal.Food = "Carnivore 2";

            // Act
            _animalsRepository.UpdateAnimal(originalAnimal);

            // Assert
            var retrievedAnimal = _context.Animals.Find(id);
            Assert.IsNotNull(retrievedAnimal);
            Assert.AreEqual("Lion King", retrievedAnimal.Species);
        }
    }
}