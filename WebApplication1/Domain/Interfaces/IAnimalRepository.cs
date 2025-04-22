namespace WebApplication1
{
    public interface IAnimalRepository
    {
        void Add(Animal animal);
        void Remove(Guid id);
        Animal GetById(Guid id);
        IEnumerable<Animal> GetAll();
    }
}