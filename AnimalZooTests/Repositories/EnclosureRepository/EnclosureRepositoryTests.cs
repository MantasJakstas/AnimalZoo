using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalZoo.Repositories.EnclosureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalZoo.Database;
using AnimalZoo.Repositories.AnimalsReposiotry;
using Microsoft.EntityFrameworkCore;
using AnimalZoo.Models;

namespace AnimalZoo.Repositories.EnclosureRepository.Tests
{
    [TestClass()]
    public class EnclosureRepositoryTests
    {
        private ZooDbContext _context;
        private IEnclosure _enclosureRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ZooDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ZooDbContext(options);
            _enclosureRepository = new EnclosureRepository(_context);
        }

        [TestMethod()]
        public void AddEnclosureTest()
        {
            int id = Guid.NewGuid().GetHashCode();
            var enclosure = new Enclosure
            {
                Id = id,
                Name = "Enclosure 1",
                Size = "Large"
            };

            // Act
            _enclosureRepository.AddEnclosure(enclosure);

            // Assert
            var retrievedEnclosure = _context.Enclosures.Find(id);
            Assert.IsNotNull(retrievedEnclosure);
            Assert.AreEqual("Enclosure 1", retrievedEnclosure.Name);
            Assert.AreEqual("Large", retrievedEnclosure.Size);
        }

        [TestMethod()]
        public void AddEnlosureRangeTest()
        {

            // Arrange
            var enclosures = new List<Enclosure>
            {
                new Enclosure { Id = Guid.NewGuid().GetHashCode(), Name = "Enclosure 1"},
                new Enclosure { Id = Guid.NewGuid().GetHashCode(), Name = "Enclosure 2"},
            };

            // Act
            _enclosureRepository.AddEnlosureRange(enclosures);

            // Assert
            var retrievedEnclosures = _context.Enclosures.ToList();
            CollectionAssert.AreEquivalent(enclosures, retrievedEnclosures);
        }

        [TestMethod()]
        public void DeleteEnclosureTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var enclosure = new Enclosure { Id = id, Name = "Enclosure to delete" };
            _context.Enclosures.Add(enclosure);
            _context.SaveChanges();

            // Act
            _enclosureRepository.DeleteEnclosure(id);

            // Assert
            var retrievedEnclosure = _context.Enclosures.Find(id);
            Assert.IsNull(retrievedEnclosure);
        }

        [TestMethod()]
        public void GetAllEnclosureTest()
        {
            // Arrange
            var enclosures = new List<Enclosure>
            {
                new Enclosure { Id = 1, Name = "Enclosure X", Size = "Large", Location = "Outside" },
                new Enclosure { Id = 2, Name = "Enclosure Y", Size = "Medium", Location = "Inside" },
            };
            _context.Enclosures.AddRange(enclosures);
            _context.SaveChanges();

            // Act
            var retrievedEnclosures = _enclosureRepository.GetAllEnclosure();

            // Assert
            CollectionAssert.AreEquivalent(enclosures, (System.Collections.ICollection?)retrievedEnclosures);
        }

        [TestMethod()]
        public void GetEnlosureByIdTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var enclosure = new Enclosure { Id = id, Name = "Enclosure 1" };
            _context.Enclosures.Add(enclosure);
            _context.SaveChanges();

            // Act
            var retrievedEnclosure = _enclosureRepository.GetEnlosureById(id);

            // Assert
            Assert.IsNotNull(retrievedEnclosure);
            Assert.AreEqual("Enclosure 1", retrievedEnclosure.Name);
        }

        [TestMethod()]
        public void UpdateEnclosureTest()
        {
            // Arrange
            int id = Guid.NewGuid().GetHashCode();
            var enclosure = new Enclosure { Id = id, Name = "Original Enclosure", Size = "Large" };
            _context.Enclosures.Add(enclosure);
            _context.SaveChanges();

            enclosure.Name = "New Enclosure";
            enclosure.Size = "Small";

            // Act
            _enclosureRepository.UpdateEnclosure(enclosure);

            // Assert
            var retrievedEnclosure = _context.Enclosures.Find(id);
            Assert.IsNotNull(retrievedEnclosure);
            Assert.AreEqual("New Enclosure", retrievedEnclosure.Name);
            Assert.AreEqual("Small", retrievedEnclosure.Size);
        }
    }
}