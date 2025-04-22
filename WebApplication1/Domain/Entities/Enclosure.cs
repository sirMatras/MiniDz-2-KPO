using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication1
{
    public class Enclosure
    {
        public enum EnclosureType
        {
            Predator,
            Herbivore,
            Bird,
            Aquarium
        }

        private List<Guid> _enclosureList = new();

        public EnclosureType Type { get; set; }
        public int CurrentSize { get; set; }
        public int Square { get; set; }
        public int MaxSize { get; set; }
        public Guid EnclosureId { get; set; }

        // Конструктор для десериализации
        [JsonConstructor]
        public Enclosure()
        {
            EnclosureId = Guid.NewGuid();
            CurrentSize = 0;
        }

        // Основной конструктор для ручного создания
        public Enclosure(EnclosureType type, int square, int maxSize)
            : this() // Вызов конструктора по умолчанию
        {
            if (maxSize <= 0)
                throw new Exception("Недопустимый размер вольера.");

            Type = type;
            Square = square;
            MaxSize = maxSize;
        }

        public void AddAnimal(Animal animal)
        {
            if (_enclosureList.Count >= MaxSize)
            {
                throw new InvalidOperationException("Вольер переполнен! Животное не может быть добавлено.");
            }

            if (_enclosureList.Contains(animal.AnimalId))
            {
                throw new InvalidOperationException("Данное животное уже находится в вольере.");
            }

            switch (Type)
            {
                case EnclosureType.Predator:
                    if (animal.Type != Animal.AnimalType.Predator) 
                        throw new InvalidOperationException("Неверный тип животного для добавления.");
                    break;
                case EnclosureType.Herbivore:
                    if (animal.Type != Animal.AnimalType.Herbivore) 
                        throw new InvalidOperationException("Неверный тип животного для добавления.");
                    break;
                case EnclosureType.Bird:
                    if (animal.Type != Animal.AnimalType.Bird) 
                        throw new InvalidOperationException("Неверный тип животного для добавления.");
                    break;
                case EnclosureType.Aquarium:
                    if (animal.Type != Animal.AnimalType.Fish) 
                        throw new InvalidOperationException("Неверный тип животного для добавления.");
                    break;
            }

            _enclosureList.Add(animal.AnimalId);
            CurrentSize++;
        }

        public void Remove(Guid id)
        {
            var animal = _enclosureList.FirstOrDefault(x => x == id);

            if (animal == Guid.Empty || !_enclosureList.Contains(id))
            {
                throw new InvalidOperationException("Животное с таким ID не найдено в вольере!");
            }

            _enclosureList.Remove(animal);
            CurrentSize--;
        }

        public void Clean()
        {
            Console.WriteLine("Уборка вольера была произведена успешно.");
        }
    }
}
