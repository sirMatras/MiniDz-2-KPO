using Newtonsoft.Json;
using System;

namespace WebApplication1
{
    public class FeedingSchedule
    {
        public Animal Animal { get; set; }
        public DateTime FeedTime { get; set; }
        public string FoodType { get; set; }
        public bool Executed { get; set; }
        
        [JsonConstructor]
        public FeedingSchedule()
        {
            Executed = false;
        }
        
        public FeedingSchedule(Animal animal, DateTime feedTime, string foodType)
            : this() 
        {
            Animal = animal ?? throw new ArgumentNullException(nameof(animal), "Животное не может быть null.");
            FeedTime = feedTime;
            FoodType = foodType;
        }

        public void ChangeSchedule(DateTime newTime, string newFood)
        {
            FeedTime = newTime;
            FoodType = newFood;
        }

        public void Execute()
        {
            if (Executed)
            {
                return;
            }

            Executed = true;
        }
    }
}