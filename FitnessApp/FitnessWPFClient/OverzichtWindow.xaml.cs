using FitnessApp.Data;
using FitnessApp.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessWPFClient
{
    public partial class OverzichtWindow : Window
    {
        private readonly ReservationTimeslotRepository _reservationTimeslotRepository;

        public OverzichtWindow(int reservationId)
        {
            InitializeComponent();
            _reservationTimeslotRepository = new ReservationTimeslotRepository(new FitnessContext());
            LoadReservationData(reservationId);
        }

        private void LoadReservationData(int reservationId)
        {
            ReservationIdTextBlock.Text = reservationId.ToString();
            var timeslots = _reservationTimeslotRepository.GetTimeslotsForReservation(reservationId);
            TimeslotListBox.ItemsSource = timeslots.Select(t => $"{t.Timeslot.StartTime}:00 - {t.Timeslot.EndTime}:00, Equipment: ID {t.Equipment.Id} - {t.Equipment.Name}").ToList();
        }
    }
}
