using FitnessApp.Interface;
using FitnessApp.Model;
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
                if (res.ReservationId > 0)
                {
                    res.Reservation = null; // Ensure EF uses the existing ReservationId
                }

                if (res.TimeslotId > 0)
                {
                    res.Timeslot = null; // Ensure EF uses the existing TimeslotId
                }

                if (res.EquipmentId > 0)
                {
                    res.Equipment = null; // Ensure EF uses the existing EquipmentId
                }
                _repo.AddReservationTimeslot(res);
                return CreatedAtAction(nameof(Get), new { id = res.ReservationTimeslotId }, res);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
