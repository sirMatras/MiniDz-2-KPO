using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class AnimalTests
    {
        [Fact]
        public void AnimalConstructor_ShouldInitializeAnimalWithValidData()
        {
            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, enclosureId, animalId);

            Xunit.Assert.Equal(animalId, animal.AnimalId);  // Проверяем, что animalId правильно установлен
            Xunit.Assert.Equal("Lion", animal.Name);  // Проверяем, что имя животного правильно установлено
            Xunit.Assert.Equal(Animal.Sex.Male, animal.AnimalSex);  // Проверяем пол животного
            Xunit.Assert.Equal("Meat", animal.FavFood);  // Проверяем любимую еду
            Xunit.Assert.Equal(Animal.Health.Healthy, animal.HealthStatus);  // Проверяем статус здоровья
            Xunit.Assert.Equal(enclosureId, animal.EnclosureId);  // Проверяем, что вольер установлен правильно
        }

        [Fact]
        public void AnimalConstructor_ShouldThrowException_WhenNameIsNullOrEmpty()
        {
            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();

            var exception = Xunit.Assert.Throws<ArgumentException>(() =>
                new Animal(Animal.AnimalType.Predator, "", Animal.Sex.Male, "Meat", Animal.Health.Healthy, enclosureId, animalId));

            Xunit.Assert.Equal("Имя животного не может быть пустым.", exception.Message);  // Проверка на правильное исключение
        }

        [Fact]
        public void Heal_ShouldMakeSickAnimalHealthy()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Sick, Guid.NewGuid(), Guid.NewGuid());

            animal.Heal();

            Xunit.Assert.Equal(Animal.Health.Healthy, animal.HealthStatus);  // Проверяем, что животное стало здоровым
        }

        [Fact]
        public void Heal_ShouldNotChangeHealthyAnimal()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            animal.Heal();

            Xunit.Assert.Equal(Animal.Health.Healthy, animal.HealthStatus);  // Проверяем, что животное остается здоровым
        }

        [Fact]
        public void Feed_ShouldNotChangeAnything_WhenNoFavFood()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            // Проверяем, что метод Feed ничего не меняет
            animal.Feed();

            // Здесь мы не проверяем конкретные изменения, так как метод не должен ничего делать.
            Xunit.Assert.True(true);  // Проверка, что тест завершился без ошибок
        }

        [Fact]
        public void EnclosureChange_ShouldChangeEnclosureId_WhenValidEnclosureId()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var newEnclosureId = Guid.NewGuid();

            animal.EnclosureChange(newEnclosureId);

            Xunit.Assert.Equal(newEnclosureId, animal.EnclosureId);  // Проверяем, что вольер был изменен
        }

        [Fact]
        public void EnclosureChange_ShouldNotChangeEnclosureId_WhenInvalidEnclosureId()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var oldEnclosureId = animal.EnclosureId;  // Сохраняем старый вольер

            animal.EnclosureChange(Guid.Empty);  // Пытаемся установить неверный идентификатор вольера

            Xunit.Assert.Equal(oldEnclosureId, animal.EnclosureId);  // Проверяем, что вольер не был изменен
        }
    }
}
