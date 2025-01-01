using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsessionController : ControllerBase
    {
        private ITrainingsessionRepository repo;

        public TrainingsessionController(ITrainingsessionRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("running/details/{runningSessionId}")]
        public ActionResult<List<RunningSessionDetail>> GetRunningSessionDetails(int runningSessionId)
        {
            var details = repo.GetRunningSessionDetails(runningSessionId);
            if (details == null || !details.Any())
                return NotFound();

            return Ok(details);
        }

        [HttpGet("sessions")]
        public ActionResult<List<object>> GetSessionsForMonthAndYear([FromQuery] string? type, [FromQuery] int memberId, [FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var sessions = repo.GetSessionsForMonthAndYear(type, memberId, month, year);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sessions/stats")]
        public ActionResult GetSessionStatsForMember([FromQuery] string? type, [FromQuery] int memberId)
        {
            try
            {
                var stats = repo.GetSessionStatsForMember(type, memberId);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sessions/monthly-count")]
        public ActionResult GetSessionCountPerMonthForYear([FromQuery] string? type,[FromQuery] int memberId, [FromQuery] int year)
        {
            try
            {
                var monthlyCounts = repo.GetSessionCountPerMonthForYear(type, memberId, year);
                return Ok(monthlyCounts); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sessions/stats-with-types")]
        public ActionResult<List<object>> GetSessionCountPerMonthForYearWithType([FromQuery] int memberId, [FromQuery] int year)
        {
            try
            {
                var result = repo.GetSessionCountPerMonthForYearWithType(memberId, year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
