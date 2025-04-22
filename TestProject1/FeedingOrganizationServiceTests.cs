using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class FeedingOrganizationServiceTests
    {
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly FeedingOrganizationService _service;

        public FeedingOrganizationServiceTests()
        {
            _feedingScheduleRepository = new InMemoryFeedingScheduleRepository();
            _animalRepository = new InMemoryAnimalRepository();
            _service = new FeedingOrganizationService(_feedingScheduleRepository, _animalRepository);
        }

        [Fact]
        public void ScheduleFeeding_ShouldThrowException_WhenAnimalNotFound()
        {
            var animalId = Guid.NewGuid();
            var feedTime = DateTime.Now;
            var foodType = "Meat";

            var exception = Xunit.Assert.Throws<InvalidOperationException>(() =>
                _service.ScheduleFeeding(animalId, feedTime, foodType));

            Xunit.Assert.Equal($"Животное с ID {animalId} не найдено.", exception.Message);  // Проверка на правильное исключение
        }

        [Fact]
        public void ExecuteFeeding_ShouldThrowException_WhenFeedingScheduleNotFound()
        {
            var animalId = Guid.NewGuid();

            var exception = Xunit.Assert.Throws<InvalidOperationException>(() =>
                _service.ExecuteFeeding(animalId));

            Xunit.Assert.Equal($"Нет расписания кормления для животного с ID {animalId}.", exception.Message);  // Проверка на правильное исключение
        }
    }
}