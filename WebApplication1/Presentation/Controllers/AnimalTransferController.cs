using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class AnimalTransferController : ControllerBase
{
    private readonly IAnimalTransferService _animalTransferService;

    public AnimalTransferController(IAnimalTransferService animalTransferService)
    {
        _animalTransferService = animalTransferService;
    }
    
    [HttpPost("transfer")]
    public IActionResult TransferAnimal([FromQuery] Guid animalId, [FromQuery] Guid newEnclosureId)
    {
        _animalTransferService.TransferAnimal(animalId, newEnclosureId);
        return Ok("Животное перемещено.");
    }
}