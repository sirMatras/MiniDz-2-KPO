namespace WebApplication1
{
    public class AnimalTransferService : IAnimalTransferService
    {
        private readonly IAnimalRepository _animalRepository;  
        private readonly IEnclosureRepository _enclosureRepository;  
        
        public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }

        public void TransferAnimal(Guid animalId, Guid newEnclosureId)
        {
            var animal = _animalRepository.GetById(animalId);
            var newEnclosure = _enclosureRepository.GetById(newEnclosureId);

            if (animal == null)
            {
                return;
            }

            if (newEnclosure == null)
            {
                return;
            }
            
            if ((animal.Type == Animal.AnimalType.Predator && newEnclosure.Type != Enclosure.EnclosureType.Predator) ||
                (animal.Type == Animal.AnimalType.Herbivore && newEnclosure.Type != Enclosure.EnclosureType.Herbivore) ||
                (animal.Type == Animal.AnimalType.Bird && newEnclosure.Type != Enclosure.EnclosureType.Bird) ||
                (animal.Type == Animal.AnimalType.Fish && newEnclosure.Type != Enclosure.EnclosureType.Aquarium))
            
            animal.EnclosureChange(newEnclosureId);
        }
    }
}