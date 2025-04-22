namespace WebApplication1;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly List<Enclosure> _enclosures = new();

    public void Add(Enclosure enclosure)
    {
        _enclosures.Add(enclosure);
    }

    public void Remove(Guid id)
    {
        var enclosure = _enclosures.FirstOrDefault(e => e.EnclosureId == id);
        if (enclosure != null)
        {
            _enclosures.Remove(enclosure);
        }
    }

    public Enclosure GetById(Guid id)
    {
        return _enclosures.FirstOrDefault(e => e.EnclosureId == id);
    }

    public IEnumerable<Enclosure> GetAll()
    {
        return _enclosures;
    }
}
