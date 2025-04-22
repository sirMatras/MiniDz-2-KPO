namespace WebApplication1;

public class FeedingTimeEvent
{
    public Animal Animal { get; }
    public DateTime FeedTime { get; }
    public string FoodType { get; }
    public DateTime Timestamp { get; }

    public FeedingTimeEvent(Animal animal, DateTime feedTime, string foodType)
    {
        Animal = animal;
        FeedTime = feedTime;
        FoodType = foodType;
        Timestamp = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"Животное {Animal} было успешно покормлено в {Timestamp}.";
    }
}