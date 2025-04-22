using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class InMemoryFeedingScheduleRepositoryTests
    {
        private readonly InMemoryFeedingScheduleRepository _repository;

        public InMemoryFeedingScheduleRepositoryTests()
        {
            _repository = new InMemoryFeedingScheduleRepository();
        }

        [Fact]
        public void Add_ShouldAddFeedingScheduleToRepository()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedingSchedule = new FeedingSchedule(animal, DateTime.Now, "Meat");

            _repository.Add(feedingSchedule);

            var retrievedSchedule = _repository.GetById(feedingSchedule.Animal.AnimalId);

            Xunit.Assert.NotNull(retrievedSchedule);  // Проверяем, что расписание было добавлено
            Xunit.Assert.Equal(feedingSchedule.Animal.AnimalId, retrievedSchedule.Animal.AnimalId);  // Проверяем, что идентификатор животного совпадает
        }

        [Fact]
        public void Remove_ShouldRemoveFeedingScheduleFromRepository()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedingSchedule = new FeedingSchedule(animal, DateTime.Now, "Meat");

            _repository.Add(feedingSchedule);
            _repository.Remove(feedingSchedule.Animal.AnimalId);

            var retrievedSchedule = _repository.GetById(feedingSchedule.Animal.AnimalId);

            Xunit.Assert.Null(retrievedSchedule);  // Проверяем, что расписание удалено
        }

        [Fact]
        public void GetById_ShouldReturnCorrectFeedingSchedule_WhenScheduleExists()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedingSchedule = new FeedingSchedule(animal, DateTime.Now, "Meat");

            _repository.Add(feedingSchedule);

            var retrievedSchedule = _repository.GetById(feedingSchedule.Animal.AnimalId);

            Xunit.Assert.NotNull(retrievedSchedule);  // Проверяем, что расписание найдено
            Xunit.Assert.Equal(feedingSchedule.Animal.AnimalId, retrievedSchedule.Animal.AnimalId);  // Проверяем, что идентификатор животного совпадает
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenScheduleDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();  // Идентификатор, которого нет в репозитории

            var retrievedSchedule = _repository.GetById(nonExistentId);

            Xunit.Assert.Null(retrievedSchedule);  // Проверяем, что метод возвращает null, если расписание не найдено
        }

        [Fact]
        public void GetAll_ShouldReturnAllFeedingSchedulesInRepository()
        {
            var animal1 = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var animal2 = new Animal(Animal.AnimalType.Herbivore, "Elephant", Animal.Sex.Female, "Grass", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());

            var feedingSchedule1 = new FeedingSchedule(animal1, DateTime.Now, "Meat");
            var feedingSchedule2 = new FeedingSchedule(animal2, DateTime.Now, "Grass");

            _repository.Add(feedingSchedule1);
            _repository.Add(feedingSchedule2);

            var allSchedules = _repository.GetAll();

            Xunit.Assert.Contains(feedingSchedule1, allSchedules);  // Проверяем, что расписание для первого животного присутствует
            Xunit.Assert.Contains(feedingSchedule2, allSchedules);  // Проверяем, что расписание для второго животного присутствует
        }
    }
}
