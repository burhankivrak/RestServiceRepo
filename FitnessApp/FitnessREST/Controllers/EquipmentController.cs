using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {

        private IEquipmentRepository repo;

        public EquipmentController(IEquipmentRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Equipment> Get(int id)
        {
            try
            {
                return Ok(repo.GetEquipment(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Equipment> Post([FromBody] Equipment e)
        {
            repo.AddEquipment(e);
            return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromQuery] string status)
        {
            try
            {
                repo.UpdateEquipmentStatus(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
