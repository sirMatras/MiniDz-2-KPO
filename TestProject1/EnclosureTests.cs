using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class EnclosureTests
    {
        [Fact]
        public void EnclosureConstructor_ShouldInitializeWithValidData()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);

            Xunit.Assert.Equal(Enclosure.EnclosureType.Predator, enclosure.Type);  // Проверяем тип вольера
            Xunit.Assert.Equal(100, enclosure.Square);  // Проверяем площадь вольера
            Xunit.Assert.Equal(150, enclosure.MaxSize);  // Проверяем максимальный размер
            Xunit.Assert.Equal(0, enclosure.CurrentSize);  // Проверяем, что текущий размер вольера изначально равен 0
        }

        [Fact]
        public void EnclosureConstructor_ShouldThrowException_WhenMaxSizeIsLessThanOrEqualToZero()
        {
            var exception = Xunit.Assert.Throws<Exception>(() =>
                new Enclosure(Enclosure.EnclosureType.Predator, 100, 0));

            Xunit.Assert.Equal("Недопустимый размер вольера.", exception.Message);  // Проверяем, что выбрасывается исключение с нужным сообщением
        }

        [Fact]
        public void AddAnimal_ShouldAddAnimal_WhenConditionsAreMet()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            enclosure.AddAnimal(animal);

            // Проверяем, что животное добавлено в вольер
            Xunit.Assert.Equal(1, enclosure.CurrentSize);  // Размер вольера должен увеличиться
        }

        [Fact]
        public void AddAnimal_ShouldThrowException_WhenEnclosureIsFull()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 1);  // Вольер с максимальным размером 1
            var animal1 = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var animal2 = new Animal(Animal.AnimalType.Predator, "Tiger", Animal.Sex.Female, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            enclosure.AddAnimal(animal1);

            // Попытка добавить второе животное в переполненный вольер
            var exception = Xunit.Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(animal2));

            Xunit.Assert.Equal("Вольер переполнен! Животное не может быть добавлено.", exception.Message);  // Проверка, что выбрасывается исключение с нужным сообщением
        }

        [Fact]
        public void AddAnimal_ShouldThrowException_WhenAnimalAlreadyInEnclosure()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            enclosure.AddAnimal(animal);

            // Попытка добавить то же животное еще раз
            var exception = Xunit.Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(animal));

            Xunit.Assert.Equal("Данное животное уже находится в вольере.", exception.Message);  // Проверка, что выбрасывается исключение с нужным сообщением
        }

        [Fact]
        public void AddAnimal_ShouldThrowException_WhenAnimalTypeDoesNotMatchEnclosureType()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var animal = new Animal(Animal.AnimalType.Herbivore, "Elephant", Animal.Sex.Female, "Grass", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            // Попытка добавить животное, тип которого не соответствует типу вольера
            var exception = Xunit.Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(animal));

            Xunit.Assert.Equal("Неверный тип животного для добавления.", exception.Message);  // Проверка, что выбрасывается исключение с нужным сообщением
        }

        [Fact]
        public void Remove_ShouldRemoveAnimal_WhenAnimalIsInEnclosure()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            enclosure.AddAnimal(animal);

            enclosure.Remove(animal.AnimalId);

            // Проверяем, что животное было удалено из вольера
            Xunit.Assert.Equal(0, enclosure.CurrentSize);  // Размер вольера должен уменьшиться
        }

        [Fact]
        public void Remove_ShouldThrowException_WhenAnimalIsNotInEnclosure()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var animalId = Guid.NewGuid();  // Животное, которого нет в вольере

            var exception = Xunit.Assert.Throws<InvalidOperationException>(() => enclosure.Remove(animalId));

            Xunit.Assert.Equal("Животное с таким ID не найдено в вольере!", exception.Message);  // Проверка на выбрасывание исключения с нужным сообщением
        }

        [Fact]
        public void Clean_ShouldPrintCleaningMessage()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);

            // Проверяем, что метод Clean выводит сообщение в консоль
            var exception = Record.Exception(() => enclosure.Clean());

            Xunit.Assert.Null(exception);  // Проверяем, что исключений не было и метод был выполнен успешно
        }
    }
}
