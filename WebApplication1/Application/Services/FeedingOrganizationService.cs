namespace WebApplication1
{
    public class FeedingOrganizationService : IFeedingOrganizationService
    {
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;  
        private readonly IAnimalRepository _animalRepository;  

        public FeedingOrganizationService(IFeedingScheduleRepository feedingScheduleRepository, IAnimalRepository animalRepository)
        {
            _feedingScheduleRepository = feedingScheduleRepository;
            _animalRepository = animalRepository;
        }

        public void ScheduleFeeding(Guid animalId, DateTime feedTime, string foodType)
        {
            var animal = _animalRepository.GetById(animalId);
            if (animal == null)
            {
                // Если животное не найдено, выбрасываем исключение
                throw new InvalidOperationException($"Животное с ID {animalId} не найдено.");
            }

            var feedingSchedule = new FeedingSchedule(animal, feedTime, foodType);
            _feedingScheduleRepository.Add(feedingSchedule);
        }

        public void ExecuteFeeding(Guid animalId)
        {
            var feedingSchedule = _feedingScheduleRepository.GetAll().FirstOrDefault(f => f.Animal.AnimalId == animalId && !f.Executed);
            if (feedingSchedule == null)
            {
                // Если нет расписания кормления для животного, выбрасываем исключение
                throw new InvalidOperationException($"Нет расписания кормления для животного с ID {animalId}.");
            }

            feedingSchedule.Execute();
        }
    }
}