using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            catch (ReservationException ex)
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
            catch (ReservationException ex)
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
                if (!_repo.ExistsReservation(id))
                {
                    return NotFound();  
                }

                _repo.RemoveReservation(id);
                return NoContent(); 
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
