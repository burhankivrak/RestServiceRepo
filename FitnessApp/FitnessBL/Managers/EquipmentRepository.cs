using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using FitnessDL.Enums;

namespace FitnessApp.Manager
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly FitnessContext _context;

        public EquipmentRepository(FitnessContext context)
        {
            _context = context;
        }
        public IEnumerable<Equipment> GetAll()
        {
            return _context.Equipment.Where(e => e.Status == Status.available).ToList();
        }

        public void AddEquipment(Equipment e)
        {
            if (ExistsEquipment(e.Id))
                throw new EquipmentException("Equipment already exists");

            _context.Equipment.Add(e);
            _context.SaveChanges();
        }

        public Equipment GetEquipment(int id)
        {
            var e = _context.Equipment.FirstOrDefault(e => e.Id == id);

            if (e == null)
                throw new EquipmentException("Equipment doesn't exist");

            return e;
        }

        public bool ExistsEquipment(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }

        public void UpdateEquipmentStatus(int id, Status status)
        {
            var equipment = _context.Equipment.FirstOrDefault(e => e.Id == id);
            if (equipment == null)
                throw new EquipmentException("Equipment doesn't exist");

            equipment.Status = status;
            _context.SaveChanges();

            if (status == Status.maintanance)
            {
                var reservationTimeslots = _context.ReservationTimeslot
                                                  .Where(rt => rt.EquipmentId == id)
                                                  .ToList();

                if (reservationTimeslots.Any())
                {
                    foreach (var reservationTimeslot in reservationTimeslots)
                    {
                        var reservation = _context.Reservation.FirstOrDefault(r => r.Id == reservationTimeslot.ReservationId);
                        if (reservation != null)
                        {
                            _context.Reservation.Remove(reservation); 
                        }
                    }

                    _context.ReservationTimeslot.RemoveRange(reservationTimeslots);
                    _context.SaveChanges();
                }
                else
                {
                    throw new EquipmentException("No reservations found for equipment");
                }
            }
        }

        public void RemoveEquipment(int id)
        {
            var equipment = _context.Equipment.FirstOrDefault(e => e.Id == id);
            
            if (equipment == null)
                throw new EquipmentException("Equipment doesn't exist");

            var futureReservations = _context.ReservationTimeslot
                                     .Where(rt => rt.EquipmentId == id && rt.Reservation.Date > DateTime.Now)
                                     .ToList();

            if (futureReservations.Any())
            {
                throw new EquipmentException("Equipment cannot be deleted because it has active future reservations");
            }

            equipment.Status = Status.deleted;
            _context.SaveChanges();
        }

    }
}
