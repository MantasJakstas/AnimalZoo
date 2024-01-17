using AnimalZoo.Controllers;
using AnimalZoo.Models;
using AnimalZoo.Repositories.AnimalsReposiotry;
using AnimalZoo.Repositories.EnclosureRepository;
using Newtonsoft.Json;
using System.Linq;

namespace AnimalZoo.Services
{
    public class SortingService : ISortingService
    {
        private readonly IAnimals _animalsRespository;
        private readonly IEnclosure _enclosureRepository;

        public SortingService(IAnimals animals, IEnclosure enclosure)
        {
            _animalsRespository = animals;
            _enclosureRepository = enclosure;
        }

        public void SortAnimals()
        {
            List<Animal> animals = _animalsRespository.GetAllAnimals();
            if (animals == null || animals.Count == 0)
            {
                throw new Exception("No Animals Found");
            }
            List<Enclosure> enclosures = (List<Enclosure>)_enclosureRepository.GetAllEnclosure();
            if (enclosures == null || enclosures.Count == 0)
            {
                throw new Exception("No enclosures Found");
            }
            var vegans = animals.Where((animal) => animal.Food == "Herbivore").ToList();
            var nonVegans = animals.Where((animal) => animal.Food == "Carnivore").ToList();

            var veganSorted = FindEnclosureForVegan(vegans, enclosures);

            foreach (var animal in veganSorted)
            {
                _animalsRespository.UpdateAnimal(animal);
            }

            var leftEnclosures = enclosures.Where(enclosure => !veganSorted.Any(taken => taken.Enclosure == enclosure)).ToList();
            var nonVegansSorted = FindEnclosureForNonVegans(nonVegans, leftEnclosures);

            foreach (var animal in nonVegansSorted)
            {
                _animalsRespository.UpdateAnimal(animal);
            }
        }

        private List<Animal> FindEnclosureForVegan(List<Animal> vegans, List<Enclosure> enclosures)
        {
            var enclosureWithSwing = enclosures.FirstOrDefault((enclosure) => enclosure.Objects.Contains("Swing"));
            var gorrilas = vegans.First((animal) => animal.Species == "Gorilla");
            gorrilas.Enclosure = enclosureWithSwing;

            var hugeEnclosureOutside = enclosures.FirstOrDefault((enclosure) => enclosure.Size == "Huge" && enclosure.Location == "Outside");
            var everyoneElse = vegans.Where((animals) => animals.Enclosure == null).ToList();

            foreach (var animal in everyoneElse)
            {
                animal.Enclosure = hugeEnclosureOutside;
            }

            return vegans;
        }

        private List<Animal> FindEnclosureForNonVegans(List<Animal> nonVegans, List<Enclosure> enclosures)
        {
            var polarBears = nonVegans.First((animal) => animal.Species == "Polar Bear");
            var LionsTigers = nonVegans.Where((animal) => animal.Species == "Tiger" || animal.Species == "Lion").ToList();
            var CheetahJaguar = nonVegans.Where((animal) => animal.Species == "Cheetah" || animal.Species == "Jaguar").ToList();
            var WolfHyena = nonVegans.Where((animal) => animal.Species == "Wolf" || animal.Species == "Hyena").ToList();

            foreach (var enclosure in enclosures)
            {
                if (enclosure.Objects.Contains("Enrichment Toys"))
                {
                    WolfHyena[0].Enclosure = enclosure;
                    WolfHyena[1].Enclosure = enclosure;
                }
                else if (enclosure.Objects.Contains("Tall Trees"))
                {
                    CheetahJaguar[0].Enclosure = enclosure;
                    CheetahJaguar[1].Enclosure = enclosure;
                }
                else if (enclosure.Objects.Contains("Pool"))
                {
                    polarBears.Enclosure = enclosure;
                }
                else
                {
                    LionsTigers[0].Enclosure = enclosure;
                    LionsTigers[1].Enclosure = enclosure;
                }
            }

            return nonVegans;
        }

    }
}
