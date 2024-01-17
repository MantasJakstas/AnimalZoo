using AnimalZoo.Database;
using AnimalZoo.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalZoo.Repositories.AnimalsReposiotry
{
    public class AnimalsRepository : IAnimals
    {
        private readonly ZooDbContext _context;
        public AnimalsRepository(ZooDbContext context)
        {
            _context = context;
        }

        public void AddAnimal(Animal animal)
        {
            _context.Add(animal);
            _context.SaveChanges();
        }

        public void AddAnimalRange(IEnumerable<Animal> animals)
        {
            _context.Animals.AddRange(animals);
            _context.SaveChanges();
        }

        public void DeleteAnimal(int id)
        {
            var toBeRemoved = GetAnimalById(id);
            _context.Remove(toBeRemoved);
            _context.SaveChanges();
        }

        public List<Animal> GetAllAnimals()
        {
            return _context.Animals.ToList();
        }

        public Animal GetAnimalById(int id)
        {
            Animal? animal = _context.Animals.Find(id);
            if (animal == null)
            {
                throw new ArgumentNullException(nameof(animal));
            }
            return animal;

        }

        public void UpdateAnimal(Animal animal)
        {
            _context.Entry(animal).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
