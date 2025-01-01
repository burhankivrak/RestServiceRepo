using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _repo;

        public ReservationController(IReservationRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            try
            {
                var reservation = _repo.GetReservation(id);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation reservation)
        {
            try
            {
                _repo.AddReservation(reservation);
                return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            reservation.Id = id;

            if (!_repo.ExistsReservation(id))
            {
                _repo.AddReservation(reservation);
                return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
            }

            _repo.UpdateReservation(reservation);
            return NoContent();  
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var reservation = _repo.GetReservation(id);
                if (reservation == null)
                {
                    return NotFound();  
                }

                _repo.RemoveReservation(reservation);
                return NoContent();  // Return NoContent status (204) on successful deletion
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
