using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationTimeslotController : ControllerBase
    {
        private readonly IReservationTimeslotRepository _repo;

        public ReservationTimeslotController(IReservationTimeslotRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public ActionResult<ReservationTimeslot> Post([FromBody] ReservationTimeslot res)
        {
            try
            {
                _repo.AddReservationTimeslot(res);
                return CreatedAtAction(nameof(Get), new { id = res.ReservationTimeslotId }, res);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ReservationTimeslot> Get(int id)
        {
            try
            {
                var reservation = _repo.GetReservationTimeslot(id);
                return Ok(reservation);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReservationTimeslot reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            reservation.ReservationTimeslotId = id;

            if (!_repo.ExistsReservationTimeslot(id))
            {
                _repo.AddReservationTimeslot(reservation);
                return CreatedAtAction(nameof(Get), new { id = reservation.ReservationTimeslotId }, reservation);
            }

            _repo.UpdateReservationTimeslot(reservation);
            return NoContent();
        }
    }
}
