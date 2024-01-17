using AnimalZoo.Models;

namespace AnimalZoo.Repositories.AnimalsReposiotry
{
    public interface IAnimals
    {
        List<Animal> GetAllAnimals();
        Animal GetAnimalById(int id);
        void AddAnimal(Animal animal);
        void UpdateAnimal(Animal animal);
        void DeleteAnimal(int id);
        void AddAnimalRange(IEnumerable<Animal> animals);
    }
}
