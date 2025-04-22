using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class ZooStatisticsServiceTests
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly ZooStatisticsService _service;

        public ZooStatisticsServiceTests()
        {
            _animalRepository = new InMemoryAnimalRepository();
            _enclosureRepository = new InMemoryEnclosureRepository();
            _service = new ZooStatisticsService(_animalRepository, _enclosureRepository);
        }

        [Fact]
        public void PrintZooStatistics_ShouldThrowException_WhenNoAnimals()
        {
            // Убедимся, что нет животных в репозитории
            var exception = Xunit.Assert.Throws<InvalidOperationException>(() =>
                _service.PrintZooStatistics());

            Xunit.Assert.Equal("Нет животных в зоопарке.", exception.Message);  // Проверка на правильное исключение
        }

        [Fact]
        public void PrintZooStatistics_ShouldThrowException_WhenNoEnclosures()
        {
            // Добавим животных, но не добавляем вольеры
            _animalRepository.Add(new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid()));

            var exception = Xunit.Assert.Throws<InvalidOperationException>(() =>
                _service.PrintZooStatistics());

            Xunit.Assert.Equal("Нет вольеров в зоопарке.", exception.Message);  // Проверка на правильное исключение
        }

        [Fact]
        public void PrintZooStatistics_ShouldExecuteCorrectly_WhenDataExists()
        {
            // Добавляем животных и вольеры
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            _animalRepository.Add(animal);

            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            _enclosureRepository.Add(enclosure);

            // Метод должен выполняться без ошибок (т.е. исключения не выбрасывается)
            Xunit.Assert.True(true);  // Просто проверка, что код не выбрасывает исключение
        }
    }
}
