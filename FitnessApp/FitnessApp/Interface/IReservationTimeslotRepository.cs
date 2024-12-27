using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface IReservationTimeslotRepository
    {
        void AddReservationTimeslot(ReservationTimeslot res);
        bool ExistsReservationTimeslot(int id);
        ReservationTimeslot GetReservationTimeslot(int id);
    }
}
