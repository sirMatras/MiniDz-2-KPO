using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class ZooStatisticsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsController(
        IAnimalRepository animalRepository,
        IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    [HttpGet("stats")]
    public IActionResult GetStatistics()
    {
        var stats = new {
            TotalAnimals = _animalRepository.GetAll().Count(),
            TotalEnclosures = _enclosureRepository.GetAll().Count(),
            AnimalsByType = _animalRepository.GetAll()
                .GroupBy(a => a.Type)
                .ToDictionary(g => g.Key.ToString(), g => g.Count())
        };
        
        return Ok(stats);
    }
}
