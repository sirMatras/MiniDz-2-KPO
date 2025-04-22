using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class InMemoryEnclosureRepositoryTests
    {
        private readonly InMemoryEnclosureRepository _repository;

        public InMemoryEnclosureRepositoryTests()
        {
            _repository = new InMemoryEnclosureRepository();
        }

        [Fact]
        public void Add_ShouldAddEnclosureToRepository()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);

            _repository.Add(enclosure);

            var retrievedEnclosure = _repository.GetById(enclosure.EnclosureId);

            Xunit.Assert.NotNull(retrievedEnclosure);  // Проверяем, что вольер был добавлен
            Xunit.Assert.Equal(enclosure.EnclosureId, retrievedEnclosure.EnclosureId);  // Проверяем, что идентификатор совпадает
        }

        [Fact]
        public void Remove_ShouldRemoveEnclosureFromRepository()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);

            _repository.Add(enclosure);
            _repository.Remove(enclosure.EnclosureId);

            var retrievedEnclosure = _repository.GetById(enclosure.EnclosureId);

            Xunit.Assert.Null(retrievedEnclosure);  // Проверяем, что вольер удален и не найден в репозитории
        }

        [Fact]
        public void GetById_ShouldReturnCorrectEnclosure_WhenEnclosureExists()
        {
            var enclosure = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);

            _repository.Add(enclosure);

            var retrievedEnclosure = _repository.GetById(enclosure.EnclosureId);

            Xunit.Assert.NotNull(retrievedEnclosure);  // Проверяем, что вольер найден
            Xunit.Assert.Equal(enclosure.EnclosureId, retrievedEnclosure.EnclosureId);  // Проверяем, что идентификатор совпадает
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenEnclosureDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();  // Идентификатор вольера, которого нет в репозитории

            var retrievedEnclosure = _repository.GetById(nonExistentId);

            Xunit.Assert.Null(retrievedEnclosure);  // Проверяем, что метод возвращает null, если вольер не найден
        }

        [Fact]
        public void GetAll_ShouldReturnAllEnclosuresInRepository()
        {
            var enclosure1 = new Enclosure(Enclosure.EnclosureType.Predator, 100, 150);
            var enclosure2 = new Enclosure(Enclosure.EnclosureType.Herbivore, 200, 250);

            _repository.Add(enclosure1);
            _repository.Add(enclosure2);

            var allEnclosures = _repository.GetAll();

            Xunit.Assert.Contains(enclosure1, allEnclosures);  // Проверяем, что вольер 1 присутствует
            Xunit.Assert.Contains(enclosure2, allEnclosures);  // Проверяем, что вольер 2 присутствует
        }
    }
}
