using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
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
                throw new ReservationException($"Equipment with ID {res.EquipmentId} doesn't exist.");
            
            if (equipment.Status == Status.deleted)
                throw new ReservationException("Cannot create a reservation for equipment that is deleted.");


            if (ExistsReservationTimeslot(res.ReservationTimeslotId))
                throw new ReservationException("Reservation already exists");

            _context.ReservationTimeslot.Add(res);
            _context.SaveChanges();
        }

        public IEnumerable<ReservationTimeslot> GetTimeslotsForEquipment(int equipmentId)
        {
            var timeslots = _context.ReservationTimeslot
                .Where(rt => rt.EquipmentId == equipmentId)
                .Include(rt => rt.Timeslot) 
                .Include(rt => rt.Reservation)
                .ToList();

            return timeslots;
        }

        public IEnumerable<ReservationTimeslot> GetTimeslotsForReservation(int reservationId)
        {
            return _context.ReservationTimeslot
                .Where(rt => rt.ReservationId == reservationId)
                .Include(rt => rt.Timeslot) 
                .Include(rt => rt.Equipment)
                .ToList();
        }

        public ReservationTimeslot GetReservationTimeslot(int id)
        {
            var reservation = _context.ReservationTimeslot.Include(r => r.Reservation).ThenInclude(r => r.Member).Include(r => r.Equipment).Include(r => r.Timeslot).FirstOrDefault(r => r.ReservationTimeslotId == id);


            if (reservation == null)
                throw new ReservationException("Reservation doesn't exist");

            return reservation;
        }

        public bool ExistsReservationTimeslot(int id)
        {
            return _context.ReservationTimeslot.Any(r => r.ReservationTimeslotId == id);
        }

        public void UpdateReservationTimeslot(ReservationTimeslot res)
        {
            var existingReservation = _context.ReservationTimeslot
                                            .FirstOrDefault(rt => rt.ReservationTimeslotId == res.ReservationTimeslotId);
            if (existingReservation == null)
                throw new ReservationException("Reservation doesn't exist");
            _context.Entry(existingReservation).CurrentValues.SetValues(res);
            _context.SaveChanges();
        }
    }
}
