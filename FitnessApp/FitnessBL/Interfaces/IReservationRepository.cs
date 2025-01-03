using FitnessApp.Model;
using System.Diagnostics.Metrics;

namespace FitnessApp.Interface
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation res);
        Reservation GetReservation(int id);
        void RemoveReservation(int id);
        void UpdateReservation(Reservation res);
        bool ExistsReservation(int id);
    }
}
