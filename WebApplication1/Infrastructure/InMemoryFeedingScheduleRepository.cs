namespace WebApplication1;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly List<FeedingSchedule> _schedules = new();

    public void Add(FeedingSchedule schedule)
    {
        _schedules.Add(schedule);
    }

    public void Remove(Guid id)
    {
        var schedule = _schedules.FirstOrDefault(s => s.Animal.AnimalId == id);
        if (schedule != null)
        {
            _schedules.Remove(schedule);
        }
    }

    public FeedingSchedule GetById(Guid id)
    {
        return _schedules.FirstOrDefault(s => s.Animal.AnimalId == id);
    }

    public IEnumerable<FeedingSchedule> GetAll()
    {
        return _schedules;
    }
}
