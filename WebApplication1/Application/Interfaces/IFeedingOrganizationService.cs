namespace WebApplication1;

public interface IFeedingOrganizationService
{
    void ScheduleFeeding(Guid animalId, DateTime feedTime, string foodType);
    void ExecuteFeeding(Guid animalId);
}