using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private IMemberRepository repo;

        public MemberController(IMemberRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Members> Get(int id)
        {
            try
            {
                return Ok(repo.GetMember(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Members> Post([FromBody] Members member)
        {
            try
            { 
            repo.AddMember(member);
            return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Members member)
        {
            if (member == null)
            {
                return BadRequest();
            }

            member.Id = id;

            if (!repo.ExistsMember(id))
            {
                repo.AddMember(member);
                return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
            }
            repo.UpdateMember(member);
            return NoContent();
        }

        [HttpGet("{id}/reservations")]
        public ActionResult<IEnumerable<Reservation>> GetReservationsForMember(int id)
        {
            try
            {
                var reservations = repo.GetReservationsForMember(id);

                if (!reservations.Any())
                {
                    return NotFound($"No reservations found for member with ID {id}.");
                }

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/program")]
        public ActionResult<IEnumerable<FitnessProgram>> GetProgramMembersForMember(int id)
        {
            try
            {
                var programMembers = repo.GetProgramMembersForMember(id);

                if (!programMembers.Any())
                {
                    return NotFound($"No programs found for member with ID {id}.");
                }

                return Ok(programMembers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("trainingsessions")]
        public ActionResult<IEnumerable<object>> GetTrainingsessionsForMember([FromQuery] string? type, [FromQuery] int memberId)
        {
            try
            {
                var trainingsessions = repo.GetTrainingsessionsForMember(type, memberId);
                return Ok(trainingsessions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
