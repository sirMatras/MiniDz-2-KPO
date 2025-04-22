namespace WebApplication1
{
    public class ZooStatisticsService : IZooStatisticsService
    {
        private readonly IAnimalRepository _animalRepository;  // Используем интерфейс
        private readonly IEnclosureRepository _enclosureRepository;  // Используем интерфейс

        public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }

        public void PrintZooStatistics()
        {
            var totalAnimals = _animalRepository.GetAll().Count();
            if (totalAnimals == 0)
            {
                throw new InvalidOperationException("Нет животных в зоопарке.");
            }

            var animalCountByType = _animalRepository.GetAll().GroupBy(a => a.Type)
                .Select(group => new { AnimalType = group.Key, Count = group.Count() });

            var totalEnclosures = _enclosureRepository.GetAll().Count();
            if (totalEnclosures == 0)
            {
                throw new InvalidOperationException("Нет вольеров в зоопарке.");
            }

            var enclosureDetails = _enclosureRepository.GetAll().Select(e => new 
            {
                EnclosureType = e.Type,
                OccupiedSpace = e.CurrentSize,
                MaxSpace = e.MaxSize
            });

            // Вывод статистики
            Console.WriteLine("Статистика зоопарка:");
            Console.WriteLine($"Общее количество животных: {totalAnimals}");
            foreach (var animalGroup in animalCountByType)
            {
                Console.WriteLine($"Тип животного {animalGroup.AnimalType}: {animalGroup.Count}");
            }

            Console.WriteLine($"Общее количество вольеров: {totalEnclosures}");
            foreach (var enclosure in enclosureDetails)
            {
                Console.WriteLine($"Вольер типа {enclosure.EnclosureType}: занято {enclosure.OccupiedSpace} из {enclosure.MaxSpace} кв.м.");
            }
        }
    }
}
