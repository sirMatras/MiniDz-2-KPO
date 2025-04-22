using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebApplication1
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosureController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosureRepository;

        public EnclosureController(IEnclosureRepository enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        [HttpGet]
        public IActionResult GetAllEnclosures()
        {
            var enclosures = _enclosureRepository.GetAll();
            return Ok(enclosures);
        }

        [HttpGet("{id}")]
        public IActionResult GetEnclosureById(Guid id)
        {
            var enclosure = _enclosureRepository.GetById(id);
            if (enclosure == null)
                return NotFound($"Вольер с ID {id} не найден.");
            return Ok(enclosure);
        }

        [HttpPost]
        public IActionResult AddEnclosure([FromBody] Enclosure enclosure)
        {
            _enclosureRepository.Add(enclosure);
            return CreatedAtAction(nameof(GetEnclosureById), new { id = enclosure.EnclosureId }, enclosure);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnclosure(Guid id)
        {
            var enclosure = _enclosureRepository.GetById(id);
            if (enclosure == null)
                return NotFound($"Вольер с ID {id} не найден.");

            _enclosureRepository.Remove(id);
            return NoContent(); // Успешное удаление
        }
    }
}