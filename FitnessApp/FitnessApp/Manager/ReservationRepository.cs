using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class ReservationRepository : IReservationRepository
    {
        private FitnessContext context;

        public ReservationRepository()
        {
            this.context = new FitnessContext();
        }
        public void AddReservation(Reservation res)
        {

            if (ExistsReservation(res.Id))
                throw new Exception("Reservation already exists");

            context.Reservation.Add(res);  
            context.SaveChanges();
        }

        public bool ExistsReservation(int id)
        {
            return context.Reservation.Any(r => r.Id == id);
        }

        public Reservation GetReservation(int id)
        {
            var reservation = context.Reservation.Include(r => r.Member).FirstOrDefault(r => r.Id == id);


            if (reservation == null)
                throw new Exception("Reservattion doesn't exist");

            return reservation;
        }

        public void RemoveReservation(Reservation res)
        {
            var existingReservation = context.Reservation
                                            .FirstOrDefault(r => r.Id == res.Id);

            if (existingReservation == null)
                throw new Exception("Reservation doesn't exist");

            context.Reservation.Remove(existingReservation);  
            context.SaveChanges();
        }

        public void UpdateReservation(Reservation res)
        {
            var existingReservation = context.Reservation
                                            .FirstOrDefault(r => r.Id == res.Id);

            if (existingReservation == null)
                throw new Exception("Reservation doesn't exist");

            context.Entry(existingReservation).CurrentValues.SetValues(res);
            context.SaveChanges();
        }
    }
}
