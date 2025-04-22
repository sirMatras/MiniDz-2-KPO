using WebApplication1;
using Xunit;
using System;

namespace WebApplication1.Tests
{
    public class FeedingScheduleTests
    {
        [Fact]
        public void FeedingSchedule_Constructor_ShouldInitializeWithValidData()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedTime = DateTime.Now.AddHours(1);
            var foodType = "Meat";

            var feedingSchedule = new FeedingSchedule(animal, feedTime, foodType);

            Xunit.Assert.Equal(animal, feedingSchedule.Animal);  // Проверяем, что животное правильно установлено
            Xunit.Assert.Equal(feedTime, feedingSchedule.FeedTime);  // Проверяем, что время кормления правильно установлено
            Xunit.Assert.Equal(foodType, feedingSchedule.FoodType);  // Проверяем, что тип еды правильно установлен
            Xunit.Assert.False(feedingSchedule.Executed);  // Проверяем, что по умолчанию кормление не выполнено
        }

        [Fact]
        public void ChangeSchedule_ShouldUpdateFeedTimeAndFoodType()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedTime = DateTime.Now.AddHours(1);
            var foodType = "Meat";

            var feedingSchedule = new FeedingSchedule(animal, feedTime, foodType);

            var newFeedTime = DateTime.Now.AddHours(2);
            var newFoodType = "Fish";
            feedingSchedule.ChangeSchedule(newFeedTime, newFoodType);

            Xunit.Assert.Equal(newFeedTime, feedingSchedule.FeedTime);  // Проверяем, что время кормления изменилось
            Xunit.Assert.Equal(newFoodType, feedingSchedule.FoodType);  // Проверяем, что тип еды изменился
        }

        [Fact]
        public void Execute_ShouldSetExecutedToTrue_WhenNotExecutedBefore()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedTime = DateTime.Now.AddHours(1);
            var foodType = "Meat";

            var feedingSchedule = new FeedingSchedule(animal, feedTime, foodType);

            feedingSchedule.Execute();

            Xunit.Assert.True(feedingSchedule.Executed);  // Проверяем, что кормление было выполнено
        }

        [Fact]
        public void Execute_ShouldNotChangeExecuted_WhenAlreadyExecuted()
        {
            var animal = new Animal(Animal.AnimalType.Predator, "Lion", Animal.Sex.Male, "Meat", Animal.Health.Healthy, Guid.NewGuid(), Guid.NewGuid());
            var feedTime = DateTime.Now.AddHours(1);
            var foodType = "Meat";

            var feedingSchedule = new FeedingSchedule(animal, feedTime, foodType);

            feedingSchedule.Execute();
            var initialExecutedStatus = feedingSchedule.Executed;

            feedingSchedule.Execute();  // Повторный вызов метода Execute

            Xunit.Assert.Equal(initialExecutedStatus, feedingSchedule.Executed);  // Проверяем, что статус не изменился
        }
    }
}
