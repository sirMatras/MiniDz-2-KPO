using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class InMemoryAnimalRepositoryTests
    {
        private readonly InMemoryAnimalRepository _repository;

        public InMemoryAnimalRepositoryTests()
        {
            _repository = new InMemoryAnimalRepository();
        }

        [Fact]
        public void Add_ShouldAddAnimalToRepository()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            _repository.Add(animal);

            var retrievedAnimal = _repository.GetById(animal.AnimalId);

            Xunit.Assert.Equal(animal, retrievedAnimal);  // Проверяем, что добавленное животное правильно извлекается
        }

        [Fact]
        public void Remove_ShouldRemoveAnimalFromRepository()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            _repository.Add(animal);
            _repository.Remove(animal.AnimalId);

            var retrievedAnimal = _repository.GetById(animal.AnimalId);

            Xunit.Assert.Null(retrievedAnimal);  // Проверяем, что животное удалено и не найдено в репозитории
        }

        [Fact]
        public void GetById_ShouldReturnCorrectAnimal_WhenAnimalExists()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            _repository.Add(animal);

            var retrievedAnimal = _repository.GetById(animal.AnimalId);

            Xunit.Assert.Equal(animal, retrievedAnimal);  // Проверяем, что метод возвращает правильное животное
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenAnimalDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();  // Идентификатор животного, которого нет в репозитории

            var retrievedAnimal = _repository.GetById(nonExistentId);

            Xunit.Assert.Null(retrievedAnimal);  // Проверяем, что метод возвращает null, если животное не найдено
        }

        [Fact]
        public void GetAll_ShouldReturnAllAnimalsInRepository()
        {
            var animal1 = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var animal2 = new Animal(Animal.AnimalType.Herbivore, "Elephant", Animal.Sex.Female, "Grass", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            _repository.Add(animal1);
            _repository.Add(animal2);

            var allAnimals = _repository.GetAll();

            Xunit.Assert.Contains(animal1, allAnimals);  // Проверяем, что животное 1 присутствует
            Xunit.Assert.Contains(animal2, allAnimals);  // Проверяем, что животное 2 присутствует
        }
    }
}
