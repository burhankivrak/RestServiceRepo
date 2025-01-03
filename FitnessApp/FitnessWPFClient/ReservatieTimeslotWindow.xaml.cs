using FitnessApp.Data;
using FitnessApp.Manager;
using FitnessApp.Model;
using FitnessBL.Managers;
using FitnessDL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessWPFClient
{
    public partial class ReservatieTimeslotWindow : Window
    {
        private readonly ReservationTimeslotRepository _reservationTimeslotRepository;
        private readonly EquipmentRepository _equipmentRepository;
        private readonly TimeslotRepository _timeslotRepository;
        private readonly ReservationRepository _reservation;
        private readonly int _reservationId;

        public ReservatieTimeslotWindow(int reservationId)
        {
            InitializeComponent();
            _reservationId = reservationId;
            _reservationTimeslotRepository = new ReservationTimeslotRepository(new FitnessContext());
            _equipmentRepository = new EquipmentRepository(new FitnessContext());
            _timeslotRepository = new TimeslotRepository(new FitnessContext());
            _reservation = new ReservationRepository(new FitnessContext());
            LoadTimeslots();
            LoadEquipment();
        }

        private void LoadTimeslots()
        {

            var timeslots = _timeslotRepository.GetAll().OrderBy(t => t.StartTime).ToList();
            var morningSlots = timeslots.Where(t => t.PartOfDay == "morning").ToList();
            var afternoonSlots = timeslots.Where(t => t.PartOfDay == "afternoon").ToList();
            var eveningSlots = timeslots.Where(t => t.PartOfDay == "evening").ToList();

            TimeslotComboBox.ItemsSource = morningSlots.Concat(afternoonSlots).Concat(eveningSlots)
                                                        .Select(t => $"{t.StartTime}:00 - {t.EndTime}:00")
                                                        .ToList();
        }

        private void LoadEquipment()
        {
            var equipment = _equipmentRepository.GetAll();
            EquipmentComboBox.ItemsSource = equipment.Select(e => e.Name).ToList();
        }

        private void AddTimeslotButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEquipmentName = EquipmentComboBox.SelectedItem as string;
            var selectedTimeslot = TimeslotComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedEquipmentName) || string.IsNullOrEmpty(selectedTimeslot))
            {
                MessageBox.Show("Selecteer zowel een toestel als een tijdslot.");
                return;
            }

            var equipment = _equipmentRepository.GetAll().FirstOrDefault(e => e.Name == selectedEquipmentName);

            if (equipment == null)
            {
                MessageBox.Show("Dit toestel bestaat niet.");
                return;
            }

            var timeslotParts = selectedTimeslot.Split(" - ");
            var startTime = int.Parse(timeslotParts[0].Replace(":00", ""));
            var endTime = int.Parse(timeslotParts[1].Replace(":00", ""));

            var timeslot = _timeslotRepository.Get(startTime, endTime);
            if (timeslot == null)
            {
                MessageBox.Show("Het geselecteerde tijdslot bestaat niet.");
                return;
            }

            var reservation = _reservation.GetReservation(_reservationId);
            if (reservation == null)
            {
                MessageBox.Show("Reservering niet gevonden.");
                return;
            }



            // Controle: Zelfde toestel mag niet op dezelfde dag voor hetzelfde tijdslot worden gereserveerd
            var reservationsForEquipment = _reservationTimeslotRepository.GetTimeslotsForEquipment(equipment.Id);
            var sameDayReservations = reservationsForEquipment
                .Where(r => r.Reservation.Date.Date == reservation.Date.Date)
                .ToList();

            // Controleer of het toestel op hetzelfde tijdslot al is gereserveerd voor dezelfde dag
            var existingReservationForTimeslot = sameDayReservations
                .Any(r => r.TimeslotId == timeslot.Id && r.EquipmentId == equipment.Id);

            if (existingReservationForTimeslot)
            {
                MessageBox.Show("Dit toestel is al gereserveerd op dezelfde dag voor dit tijdslot. Kies een ander tijdslot.");
                return;
            }

            // Controle: Maximaal 4 tijdsloten per dag per reservering
            var reservationsForMember = _reservationTimeslotRepository.GetTimeslotsForReservation(_reservationId);
            var totalTimeslotsForDay = reservationsForMember.Count();

            if (totalTimeslotsForDay >= 4)
            {
                MessageBox.Show("Je kunt niet meer dan 4 tijdsloten per dag reserveren.");
                return;
            }

            // Controle: Maximaal 2 opeenvolgende tijdsloten voor hetzelfde toestel
            var consecutiveReservations = sameDayReservations
                .OrderBy(r => r.Timeslot.StartTime)
                .ToList();

            // Controleer of het geselecteerde tijdslot aansluit op een bestaande reservering
            var lastReservation = consecutiveReservations.LastOrDefault();
            if (lastReservation != null)
            {
                // Als het geselecteerde tijdslot aansluit op de laatste reservering, mag dit
                if (lastReservation.Timeslot.EndTime == startTime)
                {
                    // Controleer of er al 2 opeenvolgende tijdsloten zijn
                    var consecutiveSlots = consecutiveReservations
                        .Where(r => r.Timeslot.StartTime == lastReservation.Timeslot.EndTime)
                        .ToList();

                    // Als er al 2 opeenvolgende tijdsloten zijn voor hetzelfde toestel, mag je niet nog een tijdslot reserveren
                    if (consecutiveSlots.Count() >= 2)
                    {
                        MessageBox.Show("Je kunt niet meer dan 2 opeenvolgende tijdsloten voor dit toestel reserveren.");
                        return;
                    }
                }
                else
                {
                    // Als het tijdslot niet aansluit op de laatste reservering, is het een nieuw tijdslot
                    // Check of dit tijdslot overlapt met een andere reservering
                    var overlappingTimeslots = consecutiveReservations
                        .Where(r => r.Timeslot.StartTime < endTime && r.Timeslot.EndTime > startTime)
                        .ToList();

                    if (overlappingTimeslots.Any())
                    {
                        MessageBox.Show("Dit toestel is al gereserveerd voor dit tijdslot.");
                        return;
                    }
                }
            }

            _reservationTimeslotRepository.AddReservationTimeslot(new ReservationTimeslot
            {
                ReservationId = _reservationId,
                EquipmentId = equipment.Id,
                TimeslotId = timeslot.Id
            });

            MessageBox.Show("Timeslot toegevoegd.");
        }



        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var overzichtWindow = new OverzichtWindow(_reservationId);
            overzichtWindow.Show();
            this.Close();
        }
    }
}
