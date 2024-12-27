using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationRepository repo;

        public ReservationController(IReservationRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            try
            {
                var reservation = repo.GetReservation(id);
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
                repo.AddReservation(reservation);
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

            if (!repo.ExistsReservation(id))
            {
                repo.AddReservation(reservation);
                return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
            }

            repo.UpdateReservation(reservation);
            return NoContent();  
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var reservation = repo.GetReservation(id);
                if (reservation == null)
                {
                    return NotFound();  
                }

                repo.RemoveReservation(reservation);
                return NoContent();  // Return NoContent status (204) on successful deletion
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
