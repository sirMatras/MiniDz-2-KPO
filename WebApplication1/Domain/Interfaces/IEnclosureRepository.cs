namespace WebApplication1;

public interface IEnclosureRepository
{
    void Add(Enclosure enclosure);
    void Remove(Guid id);
    Enclosure GetById(Guid id);
    IEnumerable<Enclosure> GetAll();
}
