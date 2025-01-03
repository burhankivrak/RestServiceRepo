using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
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
            var member = _context.Members.FirstOrDefault(m => m.Id == res.MemberId);
            if (member == null)
            {
                throw new ReservationException("The member does not exist.");
            }
            res.Member = member;

            if (res.Date <= DateTime.Now)
                throw new ReservationException("Reservation date must be in the future.");

            if (res.Date > DateTime.Now.AddDays(7))
                throw new ReservationException("Reservation date cannot be more than 7 days in advance.");

            if (ExistsReservation(res.Id))
                throw new ReservationException("Reservation already exists");
            

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
                throw new ReservationException("Reservation doesn't exist");

            return reservation;
        }

        public void RemoveReservation(int id)
        {
            var existingReservation = _context.Reservation
                                            .FirstOrDefault(r => r.Id == id);

            if (existingReservation == null)
                throw new ReservationException("Reservation doesn't exist");

            _context.Reservation.Remove(existingReservation);
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation res)
        {
            var existingReservation = _context.Reservation
                                            .FirstOrDefault(r => r.Id == res.Id);

            if (existingReservation == null)
                throw new ReservationException("Reservation doesn't exist");

            _context.Entry(existingReservation).CurrentValues.SetValues(res);
            _context.SaveChanges();
        }
    }
}
