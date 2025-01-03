using FitnessApp.Data;
using FitnessApp.Manager;
using FitnessApp.Model;
using System.Windows;

namespace FitnessWPFClient
{
    public partial class ReserveerWindow : Window
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly int _memberId;

        public ReserveerWindow(int memberId)
        {
            InitializeComponent();
            _memberId = memberId;
            _reservationRepository = new ReservationRepository(new FitnessContext());
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = ReservationDatePicker.SelectedDate ?? DateTime.MinValue;

            if (selectedDate == DateTime.MinValue)
            {
                MessageBox.Show("Selecteer een datum.");
                return;
            }

                var reservation = new Reservation
                {
                    MemberId = _memberId,
                    Date = selectedDate
                };

            _reservationRepository.AddReservation(reservation);

            var timeslotWindow = new ReservatieTimeslotWindow(reservation.Id);
            timeslotWindow.Show();
            this.Close();
        }
    }
}
