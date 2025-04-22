using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class AnimalTransferServiceTests
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly AnimalTransferService _service;

        public AnimalTransferServiceTests()
        {
            _animalRepository = new InMemoryAnimalRepository();
            _enclosureRepository = new InMemoryEnclosureRepository();
            
            _service = new AnimalTransferService(_animalRepository, _enclosureRepository);
        }

        [Fact]
        public void TransferAnimal_ShouldDoNothing_WhenAnimalNotFound()
        {
            var animalId = Guid.NewGuid();
            var newEnclosureId = Guid.NewGuid();

            // Вызываем метод с несуществующим животным
            _service.TransferAnimal(animalId, newEnclosureId);

            // Проверяем, что животное не было перемещено (должно остаться в базе данных)
            var animal = _animalRepository.GetById(animalId);
            Xunit.Assert.Null(animal);  // Животное не должно быть найдено в репозитории
        }

        [Fact]
        public void TransferAnimal_ShouldDoNothing_WhenEnclosureNotFound()
        {
            var animalId = Guid.NewGuid();
            var newEnclosureId = Guid.NewGuid();
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), animalId);

            _animalRepository.Add(animal);

            // Вызываем метод с несуществующим вольером
            _service.TransferAnimal(animalId, newEnclosureId);

            // Проверяем, что животное не было перемещено (оно должно остаться в старом вольере)
            var updatedAnimal = _animalRepository.GetById(animalId);
            Xunit.Assert.Equal(animal.EnclosureId, updatedAnimal.EnclosureId);  // Вольер не должен измениться
        }

        [Fact]
        public void TransferAnimal_ShouldDoNothing_WhenAnimalAndEnclosureTypeDoNotMatch()
        {
            var animalId = Guid.NewGuid();
            var newEnclosureId = Guid.NewGuid();
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), animalId);
            var enclosure = new Enclosure(Enclosure.EnclosureType.Herbivore, 100, 150);  // Убираем логгер

            _animalRepository.Add(animal);
            _enclosureRepository.Add(enclosure);

            // Вызываем метод, где типы животного и вольера не совпадают
            _service.TransferAnimal(animalId, newEnclosureId);

            // Проверяем, что животное не было перемещено (оно должно остаться в старом вольере)
            var updatedAnimal = _animalRepository.GetById(animalId);
            Xunit.Assert.Equal(animal.EnclosureId, updatedAnimal.EnclosureId);  // Вольер не должен измениться
        }

        [Fact]
        public void TransferAnimal_ShouldTransferAnimal_WhenValidData()
        {
            var animalId = Guid.NewGuid();
            var newEnclosureId = Guid.NewGuid();
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), animalId);
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);  // Убираем логгер
            var newEnclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);  // Убираем логгер

            _animalRepository.Add(animal);
            _enclosureRepository.Add(enclosure);
            _enclosureRepository.Add(newEnclosure);

            // Вызываем метод с валидными данными
            _service.TransferAnimal(animalId, newEnclosureId);

            // Проверяем, что животное было перемещено в новый вольер
            var updatedAnimal = _animalRepository.GetById(animalId);
            Xunit.Assert.Equal(newEnclosureId, updatedAnimal.EnclosureId);  // Вольер должен быть обновлен
        }
    }
}
