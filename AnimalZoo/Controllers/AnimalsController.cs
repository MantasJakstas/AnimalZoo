using AnimalZoo.JsonData.JsonUtils;
using AnimalZoo.Models;
using AnimalZoo.Repositories.AnimalsReposiotry;
using AnimalZoo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnimalZoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimals _animalRepository;
        private readonly ISortingService _sortingService;

        public AnimalsController(IAnimals animalRepository, ISortingService sortingService)
        {
            _animalRepository = animalRepository;
            _sortingService = sortingService;
        }


        [HttpPost("AddAnimalRange")]
        public IActionResult Post()
        {
            string path = @"C:\Projects\AnimalZoo\AnimalZoo\JsonData\animals.json";
            var data = DeserializeJson.DeserializeRootJson<AnimalsRoot>(path);
            _animalRepository.AddAnimalRange(data.Animals);
            return Ok(data.Animals);
        }

        [HttpPost]
        public IActionResult Post(Animal animal)
        {
            _animalRepository.AddAnimal(animal);
            return Ok(animal);
        }

        [HttpGet]
        public ActionResult<List<Animal>> GetAnimals()
        {
            var animals = _animalRepository.GetAllAnimals();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> GetAnimal(int id)
        {
            var animal = _animalRepository.GetAnimalById(id);
            return Ok(animal);
        }

        [HttpPut]
        public IActionResult UpdateAnimal(Animal animal)
        {
            _animalRepository.UpdateAnimal(animal);
            return Ok(animal);
        }

        [HttpGet("SortAnimals")]
        public IActionResult SortAnimals()
        {
            _sortingService.SortAnimals();
            var animals = _animalRepository.GetAllAnimals();
            return Ok(animals);
        }
    }
}
