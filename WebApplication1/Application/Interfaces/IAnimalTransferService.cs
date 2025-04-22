namespace WebApplication1;

public interface IAnimalTransferService
{
    void TransferAnimal(Guid animalId, Guid newEnclosureId);
}