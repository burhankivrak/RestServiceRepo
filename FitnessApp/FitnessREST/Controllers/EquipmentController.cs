using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using FitnessDL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {

        private readonly IEquipmentRepository _repo;

        public EquipmentController(IEquipmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Equipment> Get(int id)
        {
            try
            {
                return Ok(_repo.GetEquipment(id));
            }
            catch (EquipmentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Equipment> Post([FromBody] Equipment e)
        {
            _repo.AddEquipment(e);
            return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromQuery] Status status)
        {
            try
            {
                _repo.UpdateEquipmentStatus(id, status);
                return NoContent();
            }
            catch (EquipmentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                if (!_repo.ExistsEquipment(id))
                {
                    return NotFound();
                }
                _repo.RemoveEquipment(id);
                return NoContent();
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
