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
            repo.AddMember(member);
            return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Members member)
        {
            if (member == null || member.Id != id)
            {
                return BadRequest();
            }
            if (!repo.ExistsMember(id))
            {
                repo.AddMember(member);
                return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
            }
            repo.UpdateMember(member);
            return NoContent();
        }

    }
}
