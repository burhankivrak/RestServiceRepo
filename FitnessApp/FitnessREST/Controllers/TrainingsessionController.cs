using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsessionController : ControllerBase
    {
        private readonly ITrainingsessionRepository _repo;

        public TrainingsessionController(ITrainingsessionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("running/details/{runningSessionId}")]
        public ActionResult<IEnumerable<RunningSessionDetail>> GetRunningSessionDetails(int runningSessionId)
        {
            var details = _repo.GetRunningSessionDetails(runningSessionId);
            if (details == null || !details.Any())
                return NotFound();

            return Ok(details);
        }

        [HttpGet("sessions")]
        public ActionResult<IEnumerable<object>> GetSessionsForMonthAndYear([FromQuery] string? type, [FromQuery] int memberId, [FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var sessions = _repo.GetSessionsForMonthAndYear(type, memberId, month, year);
                return Ok(sessions);
            }
            catch (TrainingsessionException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sessions/stats")]
        public ActionResult GetSessionStatsForMember([FromQuery] string? type, [FromQuery] int memberId)
        {
            try
            {
                var stats = _repo.GetSessionStatsForMember(type, memberId);
                return Ok(stats);
            }
            catch (TrainingsessionException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sessions/monthly-count")]
        public ActionResult GetSessionCountPerMonthForYear([FromQuery] string? type,[FromQuery] int memberId, [FromQuery] int year)
        {
            try
            {
                var monthlyCounts = _repo.GetSessionCountPerMonthForYear(type, memberId, year);
                return Ok(monthlyCounts); 
            }
            catch (TrainingsessionException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sessions/stats-with-types")]
        public ActionResult<IEnumerable<object>> GetSessionCountPerMonthForYearWithType([FromQuery] int memberId, [FromQuery] int year)
        {
            try
            {
                var result = _repo.GetSessionCountPerMonthForYearWithType(memberId, year);
                return Ok(result);
            }
            catch (TrainingsessionException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sessions/training-impact")]
        public ActionResult<IEnumerable<object>> GetTrainingImpactPerMonthForYear([FromQuery] int memberId, [FromQuery] int year)
        {
            try
            {
                var result = _repo.GetTrainingImpactPerMonthForYear(memberId, year);
                return Ok(result);
            }
            catch (TrainingsessionException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
