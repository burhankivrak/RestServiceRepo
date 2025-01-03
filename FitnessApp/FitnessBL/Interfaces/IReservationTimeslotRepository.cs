﻿using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface IReservationTimeslotRepository
    {
        void AddReservationTimeslot(ReservationTimeslot res);
        bool ExistsReservationTimeslot(int id);
        ReservationTimeslot GetReservationTimeslot(int id);
        void UpdateReservationTimeslot(ReservationTimeslot res);
        IEnumerable<ReservationTimeslot> GetTimeslotsForEquipment(int equipmentId);
        IEnumerable<ReservationTimeslot> GetTimeslotsForReservation(int reservationId);
    }
}
