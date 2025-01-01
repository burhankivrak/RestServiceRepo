using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly FitnessContext _context;

        public ReservationRepository(FitnessContext context)
        {
            _context = context;
        }
        public void AddReservation(Reservation res)
        {
            if (res.Date <= DateTime.Now)
                throw new Exception("Reservation date must be in the future.");

            if (res.Date > DateTime.Now.AddDays(7))
                throw new Exception("Reservation date cannot be more than 7 days in advance.");

            if (ExistsReservation(res.Id))
                throw new Exception("Reservation already exists");

            _context.Reservation.Add(res);
            _context.SaveChanges();
        }

        public bool ExistsReservation(int id)
        {
            return _context.Reservation.Any(r => r.Id == id);
        }

        public Reservation GetReservation(int id)
        {
            var reservation = _context.Reservation
                .Include(r => r.Member)
                .FirstOrDefault(r => r.Id == id);


            if (reservation == null)
                throw new Exception("Reservattion doesn't exist");

            return reservation;
        }

        public void RemoveReservation(Reservation res)
        {
            var existingReservation = _context.Reservation
                                            .FirstOrDefault(r => r.Id == res.Id);

            if (existingReservation == null)
                throw new Exception("Reservation doesn't exist");

            _context.Reservation.Remove(existingReservation);
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation res)
        {
            var existingReservation = _context.Reservation
                                            .FirstOrDefault(r => r.Id == res.Id);

            if (existingReservation == null)
                throw new Exception("Reservation doesn't exist");

            _context.Entry(existingReservation).CurrentValues.SetValues(res);
            _context.SaveChanges();
        }
    }
}
