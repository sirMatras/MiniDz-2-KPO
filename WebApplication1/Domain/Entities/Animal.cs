using Newtonsoft.Json;

namespace WebApplication1;
public class Animal
{
    public enum Health
    {
        Healthy,
        Sick
    }

    public enum Sex
    {
        Male,
        Female
    }

    public enum AnimalType
    {
        Predator,
        Herbivore,
        Bird,
        Fish
    }
    
    public AnimalType Type { get; set; }
    public string Name { get; set; }
    public Guid AnimalId { get; set; }
    public DateTime BirthDate { get; set; }
    public Sex AnimalSex { get; set; }
    public string FavFood { get; set; }
    public Health HealthStatus { get; set; }
    public Guid EnclosureId { get; set; }
    
    [JsonConstructor]
    public Animal()
    {
        BirthDate = DateTime.Now;
        AnimalId = Guid.NewGuid();
    }

    public Animal(AnimalType type, string name, Sex sex, string favFood,
        Health healthStatus, Guid enclosureId, Guid animalId)
        : this()
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Имя животного не может быть пустым."); 

        Type = type;
        Name = name;
        AnimalSex = sex;
        FavFood = favFood;
        HealthStatus = healthStatus;
        EnclosureId = enclosureId;
        AnimalId = animalId;
    }

    public void Heal()
    {
        if (HealthStatus == Health.Healthy)
            return;

        HealthStatus = Health.Healthy;
    }

    public void Feed()
    {
        if (string.IsNullOrEmpty(FavFood))
            return;
    }

    public void EnclosureChange(Guid enclosureId)
    {
        if (enclosureId == Guid.Empty)
            return;

        EnclosureId = enclosureId;
    }
}