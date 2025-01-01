using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessDL.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class ReservationTimeslotRepository : IReservationTimeslotRepository
    {
        private readonly FitnessContext _context;

        public ReservationTimeslotRepository(FitnessContext context)
        {
            _context = context;
        }

        public void AddReservationTimeslot(ReservationTimeslot res)
        {
            var equipment = _context.Equipment.FirstOrDefault(e => e.Id == res.EquipmentId);
            if (equipment == null)
                throw new Exception($"Equipment with ID {res.EquipmentId} doesn't exist.");
            
            if (equipment.Status == Status.deleted)
                throw new Exception("Cannot create a reservation for equipment that is deleted.");


            if (ExistsReservationTimeslot(res.ReservationTimeslotId))
                throw new Exception("Reservation already exists");

            _context.ReservationTimeslot.Add(res);
            _context.SaveChanges();
        }

        public ReservationTimeslot GetReservationTimeslot(int id)
        {
            var reservation = _context.ReservationTimeslot.Include(r => r.Reservation).ThenInclude(r => r.Member).Include(r => r.Equipment).Include(r => r.Timeslot).FirstOrDefault(r => r.ReservationTimeslotId == id);


            if (reservation == null)
                throw new Exception("Reservation doesn't exist");

            return reservation;
        }

        public bool ExistsReservationTimeslot(int id)
        {
            return _context.ReservationTimeslot.Any(r => r.ReservationTimeslotId == id);
        }
    }
}
