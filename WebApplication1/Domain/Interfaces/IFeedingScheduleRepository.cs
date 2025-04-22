namespace WebApplication1;

public interface IFeedingScheduleRepository
{
    void Add(FeedingSchedule feedingSchedule);
    void Remove(Guid id);
    FeedingSchedule GetById(Guid id);
    IEnumerable<FeedingSchedule> GetAll();
}