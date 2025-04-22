namespace WebApplication1;

public class AnimalMovedEvent
{
    public Animal Animal { get; }
    public Enclosure OldEnclosure { get; }
    public Enclosure NewEnclosure { get; }
    public DateTime Timestamp { get; }

    public AnimalMovedEvent(Animal animal, Enclosure oldEnclosure, Enclosure newEnclosure)
    {
        Animal = animal;
        OldEnclosure = oldEnclosure;
        NewEnclosure = newEnclosure;
        Timestamp = DateTime.UtcNow;
    }
    
    public override string ToString()
    {
        return $"Животное {Animal.Name} перемещено в вольер {NewEnclosure} типа ({NewEnclosure.Type})";
    }
}