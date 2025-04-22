namespace WebApplication1;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _animals = new();

    public void Add(Animal animal)
    {
        _animals.Add(animal);
    }

    public void Remove(Guid id)
    {
        var animal = _animals.FirstOrDefault(a => a.AnimalId == id);
        if (animal != null)
        {
            _animals.Remove(animal);
        }
    }

    public Animal GetById(Guid id)
    {
        return _animals.FirstOrDefault(a => a.AnimalId == id);
    }

    public IEnumerable<Animal> GetAll()
    {
        return _animals;
    }
}
