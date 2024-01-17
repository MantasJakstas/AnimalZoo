using AnimalZoo.JsonData.JsonUtils;
using AnimalZoo.Models;
using AnimalZoo.Repositories;
using AnimalZoo.Repositories.AnimalsReposiotry;
using AnimalZoo.Repositories.EnclosureRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnimalZoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosure _enclosureRepository;

        public EnclosuresController(IEnclosure enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        [HttpPost("AddEnclosureRange")]
        public IActionResult Post()
        {
            string path = @"C:\Projects\AnimalZoo\AnimalZoo\JsonData\enclosures.json";
            var data = DeserializeJson.DeserializeRootJson<EnclosureRoot>(path);
            _enclosureRepository.AddEnlosureRange(data.Enclosures);
            return Ok(data.Enclosures);
        }

        [HttpPost]
        public IActionResult Post(Enclosure enclosure)
        {
            _enclosureRepository.AddEnclosure(enclosure);
            return Ok(enclosure);
        }

        [HttpGet]
        public ActionResult<List<Enclosure>> GetEnclosures()
        {
            var enclosures = _enclosureRepository.GetAllEnclosure();
            return Ok(enclosures);
        }

        [HttpGet("{id}")]
        public ActionResult<Enclosure> GetEnclosure(int id)
        {
            var animal = _enclosureRepository.GetEnlosureById(id);
            return Ok(animal);
        }

        [HttpPut]
        public IActionResult UpdateEnclosure(Enclosure enclosure)
        {
            _enclosureRepository.UpdateEnclosure(enclosure);
            return Ok(enclosure);
        }

    }
}
