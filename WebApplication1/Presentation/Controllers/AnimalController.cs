using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebApplication1
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;  // Используем интерфейс

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;  // Получаем зависимость через интерфейс
        }

        [HttpGet]
        public IActionResult GetAllAnimals()
        {
            var animals = _animalRepository.GetAll();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById(Guid id)
        {
            var animal = _animalRepository.GetById(id);
            if (animal == null)
                return NotFound($"Животное с ID {id} не найдено.");
            return Ok(animal);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            _animalRepository.Add(animal);
            return CreatedAtAction(nameof(GetAnimalById), new { id = animal.AnimalId }, animal);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(Guid id)
        {
            var animal = _animalRepository.GetById(id);
            if (animal == null)
                return NotFound($"Животное с ID {id} не найдено.");

            _animalRepository.Remove(id);
            return NoContent();
        }
    }
}