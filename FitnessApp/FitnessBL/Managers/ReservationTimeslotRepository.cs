using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class ReservationTimeslotRepository : IReservationTimeslotRepository
    {
        private FitnessContext context;

        public ReservationTimeslotRepository()
        {
            this.context = new FitnessContext();
        }

        public void AddReservationTimeslot(ReservationTimeslot res)
        {
            if (ExistsReservationTimeslot(res.ReservationTimeslotId))
                throw new Exception("Reservation already exists");

            context.ReservationTimeslot.Add(res);
            context.SaveChanges();
        }

        public ReservationTimeslot GetReservationTimeslot(int id)
        {
            var reservation = context.ReservationTimeslot.Include(r => r.Reservation).ThenInclude(r => r.Member).Include(r => r.Equipment).Include(r => r.Timeslot).FirstOrDefault(r => r.ReservationTimeslotId == id);


            if (reservation == null)
                throw new Exception("Reservation doesn't exist");

            return reservation;
        }

        public bool ExistsReservationTimeslot(int id)
        {
            return context.ReservationTimeslot.Any(r => r.ReservationTimeslotId == id);
        }
    }
}
