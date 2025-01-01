using FitnessApp.Interface;
using FitnessApp.Model;
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                _repo.RemoveEquipment(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
