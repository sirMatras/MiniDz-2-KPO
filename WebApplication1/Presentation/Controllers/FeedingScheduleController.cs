using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebApplication1
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingScheduleController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;

        public FeedingScheduleController(IFeedingScheduleRepository feedingScheduleRepository)
        {
            _feedingScheduleRepository = feedingScheduleRepository;
        }

        [HttpGet]
        public IActionResult GetAllFeedingSchedules()
        {
            var schedules = _feedingScheduleRepository.GetAll();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeedingScheduleById(Guid id)
        {
            var schedule = _feedingScheduleRepository.GetById(id);
            if (schedule == null)
                return NotFound($"Расписание кормления для животного с ID {id} не найдено.");
            return Ok(schedule);
        }

        [HttpPost]
        public IActionResult AddFeedingSchedule([FromBody] FeedingSchedule feedingSchedule)
        {
            _feedingScheduleRepository.Add(feedingSchedule);
            return CreatedAtAction(nameof(GetFeedingScheduleById), new { id = feedingSchedule.Animal.AnimalId }, feedingSchedule);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedingSchedule(Guid id)
        {
            var schedule = _feedingScheduleRepository.GetById(id);
            if (schedule == null)
                return NotFound($"Расписание кормления для животного с ID {id} не найдено.");

            _feedingScheduleRepository.Remove(id);
            return NoContent();
        }
    }
}